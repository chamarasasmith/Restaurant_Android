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
    [Activity(Label = "Main Menu")]
    public class lv1act : Activity
    {
        List<level1> mitems1;
        //private ListView mlistView1;
        private GridView mlistView1;
        ImageButton bhome1;
        ImageButton bback1;
        ImageButton bcart1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            GC.Collect();
            // Create your application here
            SetContentView(Resource.Layout.layout1);

            mlistView1 = FindViewById<GridView>(Resource.Id.gridView1);
            bhome1 = FindViewById<ImageButton>(Resource.Id.btnhome1);
            bback1 = FindViewById<ImageButton>(Resource.Id.btnback1);
            bcart1 = FindViewById<ImageButton>(Resource.Id.btncart1);

            bhome1.Enabled = false;
            bback1.Enabled = false;
            bcart1.Enabled = false;

            //mlistView1 = FindViewById<GridView>(Resource.Id.mylistView1);

            string dpPath = DB.path;
            var db = new SQLiteConnection(dpPath);

            try
            {
                mitems1 = new List<level1>();

                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
                
                var data = db.Table<level1>(); //Call Table  
                
                //db.DropTable<level1>();
                //db.DropTable<level2>();
                //db.DropTable<level3>();
                //db.DropTable<level4>();
                //db.DropTable<Cart1>();


                //var data1 = data.Where(x => x.username == txtusername.Text && x.password == txtPassword.Text).FirstOrDefault(); //Linq Query  

                if (data != null)
                {
                    foreach (level1 item in data)
                    {
                        //Toast.MakeText(this, item.date1.ToString(), ToastLength.Short).Show();
                        mitems1.Add(new level1() { id1 = item.id1, name1= item.name1 });
                    }
                }
                
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }
            finally
            {
                db.Close();
                bhome1.Enabled = true;
                bback1.Enabled = true;
                bcart1.Enabled = true;
            }


            Adapter1 adapter1 = new Adapter1(this, mitems1);
            
            mlistView1.Adapter = adapter1;
           
            mlistView1.ItemClick += MlistView1_ItemClick;
            bhome1.Click += Bhome1_Click;
            bback1.Click += Bback1_Click;
            bcart1.Click += Bcart1_Click;
        }

        private void Bcart1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(cart_view));
        }

        private void Bback1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void Bhome1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void MlistView1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            
            try
            {
                if (mitems1[e.Position]!=null)
                {
                    var activity2 = new Intent(this, typeof(lv2act));
                    activity2.PutExtra("MyData", mitems1[e.Position].id1 + "");

                    StartActivity(activity2);
                }
                
            }
            catch (Exception ex)
            {

                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            
            //Toast.MakeText(this, mitems1[e.Position].id1 + "", ToastLength.Long).Show();
        }

       



    }
}