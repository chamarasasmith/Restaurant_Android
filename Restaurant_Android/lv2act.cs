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
    [Activity(Label = "Sub Menu 1")]
    public class lv2act : Activity
    {
        ImageButton bhome2;
        ImageButton bback2;
        ImageButton bcart2;
        private List<level2> mitems;
        //private ListView mlistView;
        private GridView mlistView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            GC.Collect();
            SetContentView(Resource.Layout.layout2);
            // Create your application here

            //mlistView = FindViewById<ListView>(Resource.Id.mylistView1);
            mlistView = FindViewById<GridView>(Resource.Id.gridView2);
            bhome2= FindViewById<ImageButton>(Resource.Id.btnhome2);
            bback2 = FindViewById<ImageButton>(Resource.Id.btnback2);
            bcart2 = FindViewById<ImageButton>(Resource.Id.btncart2);

            bhome2.Enabled = false;
            bback2.Enabled = false;
            bcart2.Enabled = false;

            string dpPath = DB.path;
            var db = new SQLiteConnection(dpPath);


            try
            {
                mitems = new List<level2>();

                string text = Intent.GetStringExtra("MyData") ?? "0";
                //Toast.MakeText(this, text, ToastLength.Short).Show();

                int id1 = 0;
                if (string.IsNullOrEmpty(text))
                {
                    id1 = 0;
                }
                else
                {
                    id1 = int.Parse(text);
                }




                var data = db.Table<level2>(); //Call Table  

                var data1 = data.Where(x => x.id1 == id1).ToList<level2>(); //Linq Query  

                //Toast.MakeText(this, data1.Count, ToastLength.Short).Show();

                if (data1 != null)
                {
                    foreach (level2 item in data1)
                    {
                        mitems.Add(new level2() { id1 = item.id1, id2 = item.id2, name2 = item.name2 });
                    }
                }
                
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                db.Close();
                bhome2.Enabled = true;
                bback2.Enabled = true;
                bcart2.Enabled = true;
            }

            Adapter2 adapter1 = new Adapter2(this, mitems);

            mlistView.Adapter = adapter1;

            mlistView.ItemClick += MlistView_ItemClick;
                bhome2.Click += Bhome2_Click;
                bback2.Click += Bback2_Click;
                bcart2.Click += Bcart2_Click;

           

            
        }

        private void Bcart2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(cart_view));
        }

        private void Bback2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(lv1act));
        }

        private void Bhome2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void MlistView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                if (mitems[e.Position] != null)
                {
                    var activity2 = new Intent(this, typeof(lv3act));
                    activity2.PutExtra("MyData", mitems[e.Position].id2 + "");
                    activity2.PutExtra("pre1", mitems[e.Position].id1 + "");
                    StartActivity(activity2);
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }

            
        }
    }
}