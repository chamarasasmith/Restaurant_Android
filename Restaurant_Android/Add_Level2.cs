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
    [Activity(Label = "Add Sub Menu 1 Items", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class Add_Level2 : Activity
    {
        Spinner sp1;
        ImageView img2;
        Button load2;
        ImageButton save2;
        private ImageButton home2;
        private ImageButton back2;
        EditText ename2;
        string imgpath;
        int PickImageId = 1000;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.add_level2_item);
            // Create your application here
            img2 = FindViewById<ImageView>(Resource.Id.limg2);
            load2 = FindViewById<Button>(Resource.Id.limgbtn2);
            save2 = FindViewById<ImageButton>(Resource.Id.btncsave2);
            home2 = FindViewById<ImageButton>(Resource.Id.btnchome2);
            back2 = FindViewById<ImageButton>(Resource.Id.btncback2);

            ename2 = FindViewById<EditText>(Resource.Id.lname2);
            sp1 =FindViewById<Spinner>(Resource.Id.spinner1);

            var items = new List<string>();

            try
            {
                string dpPath = DB.path; 
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<level1>(); //Call Table  
                if (data != null)
                {
                    foreach (level1 item in data)
                    {
                        items.Add(item.id1+":"+item.name1);
                    }
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }




            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, items);
            sp1.Adapter = adapter;

            load2.Click += Load2_Click;
            save2.Click += Save2_Click;
            home2.Click += Home2_Click;
            back2.Click += Back2_Click;
        }

        private void Back2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(admin1));
        }

        private void Home2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void Save2_Click(object sender, EventArgs e)
        {
            string ss = sp1.SelectedItem.ToString().Split(':')[0];
            //Toast.MakeText(this, ss, ToastLength.Long).Show();
            int i=int.Parse(ss);
            createL2Table(ename2.Text, imgpath,i);
        }

        private void Load2_Click(object sender, EventArgs e)
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
                img2.SetImageURI(uri);

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


        public void createL2Table(string name2, string img2, int id1)
        {

            try
            {
                string dpPath = DB.path;
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<level2>();
                level2 i = db.Table<level2>().LastOrDefault<level2>();
                level2 tbl = new level2();
                tbl.id2 = i.id2 + 1;
                tbl.name2 = name2;

                byte[] img = File.ReadAllBytes(imgpath);

                //tbl.img2 = img;

                tbl.id1 = id1;
                db.Insert(tbl);
                Toast.MakeText(this, "Save Successfully...,", ToastLength.Short).Show();
                StartActivity(typeof(Add_Level2));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "ex" + ex.ToString(), ToastLength.Short).Show();
            }

        }
    }
}