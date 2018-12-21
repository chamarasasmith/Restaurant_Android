using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;

namespace Restaurant_Android
{
    [Activity(Label = "Add Sub Menu 3 Items", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class Add_Level4 : Activity
    {
        Spinner sp4;
        ImageView img4;
        Button load4;
        ImageButton save4;
        private ImageButton home4;
        private ImageButton back4;
        EditText ename4;
        EditText edes4;
        EditText eprice4;
        string imgpath;
        int PickImageId = 1000;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.add_level4_item);
            // Create your application here

            img4 = FindViewById<ImageView>(Resource.Id.limg4);
            load4 = FindViewById<Button>(Resource.Id.limgbtn4);
            save4 = FindViewById<ImageButton>(Resource.Id.btncsave4);
            home4 = FindViewById<ImageButton>(Resource.Id.btnchome4);
            back4 = FindViewById<ImageButton>(Resource.Id.btncback4);

            ename4 = FindViewById<EditText>(Resource.Id.lname4);
            edes4 = FindViewById<EditText>(Resource.Id.ldes4);
            eprice4 = FindViewById<EditText>(Resource.Id.lp4);

            sp4 = FindViewById<Spinner>(Resource.Id.spinner4);

            var items = new List<string>();

            try
            {
                string dpPath = DB.path;
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<level3>(); //Call Table  
                if (data != null)
                {
                    foreach (level3 item in data)
                    {
                        items.Add(item.id3 + ":" + item.name3);
                    }
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, items);
            sp4.Adapter = adapter;

            load4.Click += Load4_Click ;
            save4.Click += Save4_Click ;
            home4.Click += Home4_Click;
            back4.Click += Back4_Click;
        }

        private void Back4_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(admin1));
        }

        private void Home4_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void Load4_Click(object sender, EventArgs e)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }

        private void Save4_Click(object sender, EventArgs e)
        {
            string ss = sp4.SelectedItem.ToString().Split(':')[0];
            //Toast.MakeText(this, ss, ToastLength.Long).Show();
            int i = int.Parse(ss);
            createL4Table(ename4.Text, imgpath, edes4.Text, eprice4.Text, i);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                imgpath = GetRealPathFromURI(uri);
                //imgpath = uri + "";
                //Toast.MakeText(this, imgpath, ToastLength.Long).Show();
                img4.SetImageURI(uri);

            }
        }

        public string GetRealPathFromURI(Android.Net.Uri contentUri)
        {
            var mediaStoreImagesMediaData = "_data";
            string[] projection = { mediaStoreImagesMediaData };
#pragma warning disable CS0618 // Type or member is obsolete
            Android.Database.ICursor cursor = this.ManagedQuery(contentUri, projection, null, null, null);
#pragma warning restore CS0618 // Type or member is obsolete
            int columnIndex = cursor.GetColumnIndexOrThrow(mediaStoreImagesMediaData);
            cursor.MoveToFirst();
            return cursor.GetString(columnIndex);
        }

        public void createL4Table(string name4, string img4, string des4, string price4, int id3)
        {

            try
            {
                string dpPath = DB.path;
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<level4>();
                level4 i = db.Table<level4>().LastOrDefault<level4>();
                level4 tbl = new level4();
                tbl.id4 = i.id4 + 1;
                tbl.name4 = name4;

                byte[] img = File.ReadAllBytes(imgpath);

                //tbl.img4 = img;

                tbl.id3 = id3;
                tbl.des4 = des4;
                tbl.price4 = price4;
                db.Insert(tbl);
                Toast.MakeText(this, "Save Successfully...,", ToastLength.Short).Show();
                StartActivity(typeof(Add_Level4));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "ex" + ex.ToString(), ToastLength.Short).Show();
            }

        }
    }
}