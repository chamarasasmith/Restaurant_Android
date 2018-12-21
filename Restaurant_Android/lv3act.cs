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
    [Activity(Label = "Sub Menu 2")]
    public class lv3act : Activity
    {
        private List<level3> mitems1;
        private string text;

        //private ListView mlistView1;
        private GridView mlistView1;
        ImageButton bhome3;
        private ImageButton bback3;
        private ImageButton bcart3;
        private string pre1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            GC.Collect();
            // Create your application here
            SetContentView(Resource.Layout.layout3);

            //mlistView1 = FindViewById<ListView>(Resource.Id.mylistView3);
            mlistView1 = FindViewById<GridView>(Resource.Id.gridView3);
            bhome3 = FindViewById<ImageButton>(Resource.Id.btnhome3);
            bback3 = FindViewById<ImageButton>(Resource.Id.btnback3);
            bcart3 = FindViewById<ImageButton>(Resource.Id.btncart3);

            bhome3.Enabled = false;
            bback3.Enabled = false;
            bcart3.Enabled = false;

            string dpPath = DB.path;
            var db = new SQLiteConnection(dpPath);

            try
            {
                mitems1 = new List<level3>();
                text = Intent.GetStringExtra("MyData") ?? "0";
                pre1 = Intent.GetStringExtra("pre1") ?? "0";
                //Toast.MakeText(this, pre1, ToastLength.Short).Show();


                int id2 = 0;
                if (string.IsNullOrEmpty(text))
                {
                    id2 = 0;
                }
                else
                {
                    id2 = int.Parse(text);
                }

                
                var data = db.Table<level3>(); //Call Table  
                //db.DropTable<level3>();
                var data1 = data.Where(x => x.id2 == id2).ToList<level3>(); //Linq Query  
                if (data1 != null)
                {
                    foreach (level3 item in data1)
                    {
                        mitems1.Add(new level3() { id2 = item.id2, id3 = item.id3, name3 = item.name3 });
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
                bhome3.Enabled = true;
                bback3.Enabled = true;
                bcart3.Enabled = true;
            }

            Adapter3 adapter1 = new Adapter3(this, mitems1);

            mlistView1.Adapter = adapter1;


                mlistView1.ItemClick += MlistView1_ItemClick;
                bhome3.Click += Bhome3_Click;
                bback3.Click += Bback3_Click;
                bcart3.Click += Bcart3_Click;
          
        }

        private void Bcart3_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(cart_view));
        }

        private void Bback3_Click(object sender, EventArgs e)
        {
            if (pre1 != "0")
            {
                //StartActivity(typeof(lv1act));
                var activity2 = new Intent(this, typeof(lv2act));
                activity2.PutExtra("MyData", pre1 + "");
                StartActivity(activity2);
            }

        }

        private void Bhome3_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void MlistView1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                if (mitems1[e.Position] != null)
                {
                    var activity2 = new Intent(this, typeof(lv4act));
                    activity2.PutExtra("MyData", mitems1[e.Position].id3 + "");
                    activity2.PutExtra("pre2", mitems1[e.Position].id2 + "");
                    activity2.PutExtra("pre3", pre1 + "");
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