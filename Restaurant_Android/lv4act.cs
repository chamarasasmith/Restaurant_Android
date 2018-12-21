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
    [Activity(Label = "Sub Menu 3")]
    public class lv4act : Activity
    {
        private List<level4> mitems1;
        //private ListView mlistView1;
        private GridView mlistView1;
        ImageButton bhome4;
        private ImageButton bback4;
        private ImageButton bcart4;
        private string pre2;
        private string pre3;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            GC.Collect();
            // Create your application here
            SetContentView(Resource.Layout.layout4);

            //mlistView1 = FindViewById<ListView>(Resource.Id.mylistView3);
            mlistView1 = FindViewById<GridView>(Resource.Id.gridView4);
            bhome4 = FindViewById<ImageButton>(Resource.Id.btnhome4);
            bback4 = FindViewById<ImageButton>(Resource.Id.btnback4);
            bcart4 = FindViewById<ImageButton>(Resource.Id.btncart4);

            bhome4.Enabled = false;
            bback4.Enabled = false;
            bcart4.Enabled = false;
            string dpPath = DB.path;
            var db = new SQLiteConnection(dpPath);

            try
            {
                mitems1 = new List<level4>();
                string text = Intent.GetStringExtra("MyData") ?? "0";
                pre2 = Intent.GetStringExtra("pre2") ?? "0";
                pre3 = Intent.GetStringExtra("pre3") ?? "0";
                //Toast.MakeText(this, text, ToastLength.Short).Show();

                int id3 = 0;
                if (string.IsNullOrEmpty(text))
                {
                    id3 = 0;
                }
                else
                {
                    id3 = int.Parse(text);
                }


                
                var data = db.Table<level4>(); //Call Table  
                //db.DropTable<level3>();
                var data1 = data.Where(x => x.id3 == id3).ToList<level4>(); //Linq Query  
                if (data1 != null)
                {
                    foreach (level4 item in data1)
                    {
                        mitems1.Add(new level4() { id3 = item.id3, id4 = item.id4, name4 = item.name4 });
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
                bhome4.Enabled = true;
                bback4.Enabled = true;
                bcart4.Enabled = true;
            }

            Adapter4 adapter1 = new Adapter4(this, mitems1);

            mlistView1.Adapter = adapter1;


            mlistView1.ItemClick += MlistView1_ItemClick;
                bhome4.Click += Bhome4_Click;
                bback4.Click += Bback4_Click;
                bcart4.Click += Bcart4_Click;
            

        }

        private void Bcart4_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(cart_view));
        }

        private void Bback4_Click(object sender, EventArgs e)
        {
            if (pre2 != "0" | pre3 != "0")
            {
                //StartActivity(typeof(lv1act));
                var activity2 = new Intent(this, typeof(lv3act));
                activity2.PutExtra("MyData", pre2 + "");
                activity2.PutExtra("pre1", pre3 + "");
                StartActivity(activity2);
            }
        }

        private void Bhome4_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void MlistView1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                if (mitems1[e.Position] != null)
                {
                    var activity2 = new Intent(this, typeof(orderact));
                    activity2.PutExtra("MyData", mitems1[e.Position].id4 + "");
                    activity2.PutExtra("pre4", mitems1[e.Position].id3 + "");
                    activity2.PutExtra("pre5", pre2 + "");
                    activity2.PutExtra("pre6", pre3 + "");
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