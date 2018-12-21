using Android.App;
using Android.Widget;
using Android.OS;
using SQLite;
using System.IO;
using System;

namespace Restaurant_Android
{
    [Activity(Label = "Silk Route", MainLauncher = true)]
    public class MainActivity : Activity
    {
        
        Button btnsign;
        Button btnview;
        Button btncartview;
        private Button btntest1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            btnsign = FindViewById<Button>(Resource.Id.btnlogin);
            
            btnview = FindViewById<Button>(Resource.Id.btnview);

            btncartview = FindViewById<Button>(Resource.Id.btncart1);

            btntest1 = FindViewById<Button>(Resource.Id.bnttest1);

            btnsign.Click += Btnsign_Click;
            btncartview.Click += Btncartview_Click;
            btnview.Click += Btnview_Click;

            btntest1.Click += Btntest1_Click;

            CreateDB();



        }

        //public override void OnBackPressed()
        //{
        //    //base.OnBackPressed();
        //}

        

        private void Btntest1_Click(object sender, EventArgs e)
        {
           

            //createL1Table("Food Items", "f1");
            //createL1Table("Beverages", "b1");

            //createL2Table("Category 1", "f1",1);
            //createL2Table("Category 2", "b1",1);

            //createL3Table("Rice", "r1", 1);
            //createL3Table("Noodles", "n1", 1);
            //createL3Table("Hoppers", "h1", 2);
            //createL3Table("Kottu", "k1", 2);

            //string des1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
            //string des2 = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";



            //createL4Table("Chicken Biriyani", "bb1", des2, "100", 1);
            //createL4Table("Nasi Goreng", "bb2", des1, "200", 1);

            //createL4Table("Plain Noodles", "pn1", des2, "100", 2);
            //createL4Table("Chicken Noodles", "cn1", des2, "200", 2);
            //createL4Table("Vegitable Noodles", "vn1", des1, "150", 2);

            //createL4Table("Plain Hoppers", "ph1", des2, "10", 3);
            //createL4Table("Egg Hoppers", "eh1", des2, "30", 3);
            //createL4Table("Honey Hoppers", "hh1", des1, "20", 3);

            //createL4Table("Chicken Kottu", "chk1", des1, "300", 4);
            //createL4Table("Cheese Kottu", "ck1", des2, "350", 4);
            //createL4Table("Egg Kottu", "ek1", des1, "250", 4);


        }

        private void Btncartview_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(cart_view));
        }

        private void Btnview_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(lv1act));
        }

        
        private void Btnsign_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(login));
            
        }
        public string CreateDB()
        {
            var output = "";
            SQLiteConnection db=null;
            try
            {
                output += "Creating Databse if it doesnt exists";
                string dpPath = DB.path;
                db = new SQLiteConnection(dpPath);
                db.CreateTable<level1>();
                db.CreateTable<level2>();
                db.CreateTable<level3>();
                db.CreateTable<level4>();
                output += "\n Database Created....";
            }
            catch (Exception)
            {

            }
            finally {
                db.Close();
            }
            
            
            return output;
        }

        public void createL1Table( string name1, string img1)
        {

            try
            {
                string dpPath = DB.path;
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");

                var db = new SQLiteConnection(dpPath);
                db.CreateTable<level1>();
                level1 tbl = new level1();
                
                tbl.name1 = name1;
                //tbl.img1 = img1;
                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "ex" + ex.ToString(), ToastLength.Short).Show();
            }

        }

        public void createL2Table(string name2,string img2, int id1) {

            try
            {
                string dpPath = DB.path;
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<level2>();
                level2 tbl = new level2();
                //tbl.id2 = id2;
                tbl.name2 = name2;
                //tbl.img2 = img2;
                tbl.id1 = id1;
                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "ex" + ex.ToString(), ToastLength.Short).Show();
            }
           
        }
        public void createL3Table(string name3, string img3,int id2)
        {

            try
            {
                string dpPath = DB.path;
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<level3>();
                level3 tbl = new level3();
                //tbl.id3 = id3;
                tbl.name3 = name3;
                //tbl.img3 = img3;
                //tbl.des3 = des3;
                //tbl.price3 = price3;
                tbl.id2 = id2;
                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "ex" + ex.ToString(), ToastLength.Short).Show();
            }

        }


        public void createL4Table(string name4, string img4, string des4, string price4, int id3)
        {

            try
            {
                string dpPath = DB.path;
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<level4>();
                level4 tbl = new level4();
                //tbl.id4 = id4;
                tbl.name4 = name4;
                //tbl.img4 = img4;
                tbl.des4 = des4;
                tbl.price4 = price4;
                tbl.id3 = id3;
                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "ex" + ex.ToString(), ToastLength.Short).Show();
            }

        }
    }
}

