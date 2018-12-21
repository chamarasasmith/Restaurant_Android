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
    [Activity(Label = "Remove Item From Sub Menu 2")]
    public class Remove_Level3_Item : Activity
    {
        private Spinner sp3;
        private ImageButton b3;
        private ImageButton back3;
        //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
        private ImageButton home3;
        string dpPath = DB.path;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Remove_Level3_Item);
            // Create your application here

            sp3 = FindViewById<Spinner>(Resource.Id.spr3);

            b3 = FindViewById<ImageButton>(Resource.Id.btnribin3);
            back3 = FindViewById<ImageButton>(Resource.Id.btnri3);
            home3 = FindViewById<ImageButton>(Resource.Id.btnrihome3);

            var items = new List<string>();

            try
            {

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
            sp3.Adapter = adapter;
            b3.Click += B3_Click;

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

        private void B3_Click(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(dpPath);
            try
            {
                string ss = sp3.SelectedItem.ToString().Split(':')[0];
                int i = int.Parse(ss);
                var data = db.Table<level3>();
                var data1 = data.Where(x => x.id3 == i).ToList<level3>(); //Linq Query  

                if (data1 != null)
                {
                    foreach (level3 item in data1)
                    {
                        db.Delete<level3>(item.id3);
                        StartActivity(typeof(Remove_Level3_Item));
                        Toast.MakeText(this, item.name3 + " Delete Success..!", ToastLength.Short).Show();
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