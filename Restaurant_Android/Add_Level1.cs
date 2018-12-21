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
using static System.Net.Mime.MediaTypeNames;

namespace Restaurant_Android
{
    [Activity(Label = "Add Main Menu Items", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class Add_Level1 : Activity
    {
        ImageView img1;
        Button load1;
        ImageButton save1;
        private ImageButton back1;
        private ImageButton home1;
        EditText ename1;
        string imgpath;
        int PickImageId = 1000;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.add_level1_item);

            img1=FindViewById<ImageView>(Resource.Id.limg1);
            load1=FindViewById<Button>(Resource.Id.limgbtn1);
            save1=FindViewById<ImageButton>(Resource.Id.btncsave1);
            back1 = FindViewById<ImageButton>(Resource.Id.btncback1);
            home1 = FindViewById<ImageButton>(Resource.Id.btnchome1);

            ename1 =FindViewById<EditText>(Resource.Id.lname1);

            load1.Click += Load1_Click;
            save1.Click += Save1_Click;
            back1.Click += Back1_Click;
            home1.Click += Home1_Click;
        }

        private void Home1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void Back1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(admin1));
        }

        private void Save1_Click(object sender, EventArgs e)
        {
            createL1Table(ename1.Text, imgpath);
        }

        private void Load1_Click(object sender, EventArgs e)
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
                //imgpath = uri+ "";
                Toast.MakeText(this, imgpath, ToastLength.Long).Show();

                img1.SetImageURI(uri);
               
            }
        }


        public string GetRealPathFromURI(Android.Net.Uri contentUri)
        {
            var mediaStoreImagesMediaData = "_data";
            string[] projection = { mediaStoreImagesMediaData };
#pragma warning disable CS0618 // Type or member is obsolete
            Android.Database.ICursor cursor = this.ManagedQuery(contentUri, projection,null, null, null);
#pragma warning restore CS0618 // Type or member is obsolete
            int columnIndex = cursor.GetColumnIndexOrThrow(mediaStoreImagesMediaData);
            cursor.MoveToFirst();
            return cursor.GetString(columnIndex);
        }


        public void createL1Table( string name1, string img1)
        {

            try
            {
                string dpPath = DB.path;
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<level1>();
                level1 i = db.Table<level1>().LastOrDefault<level1>();
                level1 tbl = new level1();
                tbl.id1 = i.id1 + 1;
                tbl.name1 = name1;

                byte[] img = File.ReadAllBytes(imgpath);

                //tbl.img1 = img;
                db.Insert(tbl);
                Toast.MakeText(this, "Save Successfully...,", ToastLength.Short).Show();
                StartActivity(typeof(Add_Level1));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "ex" + ex.ToString(), ToastLength.Short).Show();
            }

        }


        


    }
}