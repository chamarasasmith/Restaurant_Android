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
    [Activity(Label = "Remove Item From Sub Menu 3")]
    public class Remove_Level4_Item : Activity
    {
        private Spinner sp4;
        private ImageButton b4;
        private ImageButton back4;
        private ImageButton home4;
        //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
        string dpPath = DB.path;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Remove_Level4_Item);
            // Create your application here

            sp4 = FindViewById<Spinner>(Resource.Id.spr4);

            b4 = FindViewById<ImageButton>(Resource.Id.btnribin4);
            back4 = FindViewById<ImageButton>(Resource.Id.btnri4);
            home4 = FindViewById<ImageButton>(Resource.Id.btnrihome4);

            var items = new List<string>();

            try
            {

                var db = new SQLiteConnection(dpPath);
                var data = db.Table<level4>(); //Call Table  
                if (data != null)
                {
                    foreach (level4 item in data)
                    {
                        items.Add(item.id4 + ":" + item.name4);
                    }
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, items);
            sp4.Adapter = adapter;
            b4.Click += B4_Click;
            back4.Click += Back4_Click;
            home4.Click += Home4_Click;
        }

        private void Home4_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void Back4_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(admin1));
        }

        private void B4_Click(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(dpPath);
            try
            {
                string ss = sp4.SelectedItem.ToString().Split(':')[0];
                int i = int.Parse(ss);
                var data = db.Table<level4>();
                var data1 = data.Where(x => x.id4 == i).ToList<level4>(); //Linq Query  

                if (data1 != null)
                {
                    foreach (level4 item in data1)
                    {
                        db.Delete<level4>(item.id4);
                        StartActivity(typeof(Remove_Level4_Item));
                        Toast.MakeText(this, item.name4 + " Delete Success..!", ToastLength.Short).Show();
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