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
    [Activity(Label = "Remove Item From Sub Menu 1")]
    public class Remove_Level2_Item : Activity
    {
        private Spinner sp2;
        private ImageButton b2;
        private ImageButton back2;
        private ImageButton home2;
        string dpPath = DB.path;
        //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Remove_Level2_Item);
            // Create your application here

            sp2 = FindViewById<Spinner>(Resource.Id.spr2);

            b2 = FindViewById<ImageButton>(Resource.Id.btnribin2);
            back2 = FindViewById<ImageButton>(Resource.Id.btnri2);
            home2 = FindViewById<ImageButton>(Resource.Id.btnrihome2);

            var items = new List<string>();

            try
            {

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
            sp2.Adapter = adapter;

            b2.Click += B2_Click;

            back2.Click += Back2_Click;
            home2.Click += Home2_Click;

        }

        private void Home2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void Back2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(admin1));
        }

        private void B2_Click(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(dpPath);
            try
            {
                string ss = sp2.SelectedItem.ToString().Split(':')[0];
                int i = int.Parse(ss);
                var data = db.Table<level2>();
                var data1 = data.Where(x => x.id2 == i).ToList<level2>(); //Linq Query  

                if (data1 != null)
                {
                    foreach (level2 item in data1)
                    {
                        db.Delete<level2>(item.id2);
                        StartActivity(typeof(Remove_Level2_Item));
                        Toast.MakeText(this, item.name2 + " Delete Success..!", ToastLength.Short).Show();
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