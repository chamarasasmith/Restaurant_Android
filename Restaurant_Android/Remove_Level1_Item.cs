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
using SQLite;
using System.IO;

namespace Restaurant_Android
{
    [Activity(Label = "Remove Item From Main Menu")]
    public class Remove_Level1_Item : Activity
    {
        private Spinner sp1;
        private ImageButton b1;
        private ImageButton back1;
        private ImageButton home1;
        string dpPath = DB.path;
        //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Remove_Level1_Item);
            // Create your application here

            sp1=FindViewById<Spinner>(Resource.Id.spr1);

            b1 = FindViewById<ImageButton>(Resource.Id.btnribin1);
            back1 = FindViewById<ImageButton>(Resource.Id.btnri1);
            home1 = FindViewById<ImageButton>(Resource.Id.btnrihome1);

            var items = new List<string>();

            try
            {

                var db = new SQLiteConnection(dpPath);
                var data = db.Table<level1>(); //Call Table  
                if (data != null)
                {
                    foreach (level1 item in data)
                    {
                        items.Add(item.id1 + ":" + item.name1);
                    }
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, items);
            sp1.Adapter = adapter;

            b1.Click += B1_Click;
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

        private void Bin1_Click(object sender, EventArgs e)
        {
            
        }

        private void B1_Click(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(dpPath);
            try
            {
                string ss = sp1.SelectedItem.ToString().Split(':')[0];
                int i = int.Parse(ss);
                var data = db.Table<level1>();
                var data1 = data.Where(x => x.id1 == i).ToList<level1>(); //Linq Query  

                if (data1 != null)
                {
                    foreach (level1 item in data1)
                    {
                        db.Delete<level1>(item.id1);
                        StartActivity(typeof(Remove_Level1_Item));
                        Toast.MakeText(this, item.name1+" Delete Success..!", ToastLength.Short).Show();
                    }
                }
            }
            catch (Exception ex)
            {

                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
            finally
            {
                db.Close();
            }
        }
    }
}