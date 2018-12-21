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
    [Activity(Label = "Add Sub Menu 2 Items", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class Add_Level3 : Activity
    {
        Spinner sp3;
        ImageView img3;
        Button load3;
        ImageButton save3;
        private ImageButton home3;
        private ImageButton back3;
        EditText ename3;
        string imgpath;
        int PickImageId = 1000;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.add_level3_item);
            // Create your application here

            img3 = FindViewById<ImageView>(Resource.Id.limg3);
            load3 = FindViewById<Button>(Resource.Id.limgbtn3);
            save3 = FindViewById<ImageButton>(Resource.Id.btncsave3);
            home3 = FindViewById<ImageButton>(Resource.Id.btnchome3);
            back3 = FindViewById<ImageButton>(Resource.Id.btncback3);

            ename3 = FindViewById<EditText>(Resource.Id.lname3);
            sp3 = FindViewById<Spinner>(Resource.Id.spinner3);

            var items = new List<string>();

            try
            {
                string dpPath = DB.path;
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<level2>(); //Call Table  
                if (data != null)
                {
                    foreach (level2 item in data)
                    {
                        items.Add(item.id2 + ":" + item.name2);
                    }
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }




            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, items);
            sp3.Adapter = adapter;

            load3.Click += Load3_Click;
            save3.Click += Save3_Click;
            back3.Click += Back3_Click;
            home3.Click += Home3_Click;
        }

        private void Home3_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void Back3_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(admin1));
        }

        private void Save3_Click(object sender, EventArgs e)
        {
            string ss = sp3.SelectedItem.ToString().Split(':')[0];
            //Toast.MakeText(this, ss, ToastLength.Long).Show();
            int i = int.Parse(ss);
            createL3Table(ename3.Text, imgpath, i);
        }

        private void Load3_Click(object sender, EventArgs e)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                imgpath = GetRealPathFromURI(uri);
                //imgpath = uri + "";
                //Toast.MakeText(this, imgpath, ToastLength.Long).Show();
                img3.SetImageURI(uri);

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

        public void createL3Table(string name3, string img3, int id2)
        {

            try
            {
                string dpPath = DB.path;
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<level3>();
                level3 i = db.Table<level3>().LastOrDefault<level3>();
                level3 tbl = new level3();
                tbl.id3 = i.id3 + 1;
                tbl.name3 = name3;

                byte[] img = File.ReadAllBytes(imgpath);

                //tbl.img3 = img;

                tbl.id2 = id2;
                db.Insert(tbl);
                Toast.MakeText(this, "Save Successfully...,", ToastLength.Short).Show();
                StartActivity(typeof(Add_Level3));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "ex" + ex.ToString(), ToastLength.Short).Show();
            }

        }

    }
}