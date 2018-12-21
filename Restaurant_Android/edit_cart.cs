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
    [Activity(Label = "Edit Cart Items", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class edit_cart : Activity
    {
        string dpPath = DB.path;
        //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
        //private ListView mlistView;
        private EditText count1;
        private EditText des1;
        private TextView cid1;
        private EditText cno2;
        private ImageButton set1;
        private ImageButton remove1;
        private ImageButton hoome1;
        private Spinner sr2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.edit_cart);
            // Create your application here


            count1 = FindViewById<EditText>(Resource.Id.txtccount);
            des1 = FindViewById<EditText>(Resource.Id.txtcdes1);
            cid1 = FindViewById<TextView>(Resource.Id.txtcid1);
            cno2 = FindViewById<EditText>(Resource.Id.cno2);
            set1 = FindViewById<ImageButton>(Resource.Id.btnsave7);
            remove1 = FindViewById<ImageButton>(Resource.Id.btnbin7);
            hoome1 = FindViewById<ImageButton>(Resource.Id.btnhome7);

            sr2 = FindViewById<Spinner>(Resource.Id.spsr2);
            
            var items1 = new List<string>();

            items1.Add("Well Done");
            items1.Add("Low Sugar");
            items1.Add("Without Alcohol");

            var adapter1 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, items1);

            sr2.Adapter = adapter1;

            string text = Intent.GetStringExtra("MyData") ?? "0";
            //Toast.MakeText(this, text, ToastLength.Short).Show();
            int id1 = int.Parse(text);

            
            var db = new SQLiteConnection(dpPath);

            try
            {


                var data = db.Table<Cart1>(); //Call Table  



                var data1 = data.Where(x => x.cid == id1).ToList<Cart1>(); //Linq Query  

                //Toast.MakeText(this, data1.Count, ToastLength.Short).Show();

                if (data1 != null)
                {
                    foreach (Cart1 item in data1)
                    {

                        cid1.Text = item.cid.ToString()+" : "+item.citem;
                        des1.Text = item.cremark.ToString();
                        count1.Text = item.cqty.ToString();
                        cno2.Text = item.ccno1.ToString();
                        sr2.SetSelection(adapter1.GetPosition(item.csp1));

                    }
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {

                db.Close();
            }

            set1.Click += Set1_Click;
            remove1.Click += Remove1_Click;
            hoome1.Click += Hoome1_Click;
        }

        private void Hoome1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void Remove1_Click(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(dpPath);

            try
            {


                var data = db.Table<Cart1>(); //Call Table  

                var t1 = int.Parse(cid1.Text.Split(':')[0]);

                var data1 = data.Where(x => x.cid ==t1 ).ToList<Cart1>(); //Linq Query  

                //Toast.MakeText(this, data1.Count, ToastLength.Short).Show();

                if (data1 != null)
                {
                    foreach (Cart1 item in data1)
                    {
                        db.Delete<Cart1>(item.cid);
                        StartActivity(typeof(cart_view));

                    }
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {

                db.Close();
            }
        }

        private void Set1_Click(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(dpPath);

            try
            {


                var data = db.Table<Cart1>(); //Call Table  

                var t1 = int.Parse(cid1.Text.Split(':')[0]);

                var data1 = data.Where(x => x.cid == t1).ToList<Cart1>(); //Linq Query  

                //Toast.MakeText(this, data1.Count, ToastLength.Short).Show();

                if (data1 != null)
                {
                    foreach (Cart1 item in data1)
                    {
                        item.cqty = int.Parse(count1.Text);
                        item.cremark = des1.Text;
                        item.csp1 = sr2.SelectedItem.ToString();
                        item.ccno1 = cno2.Text;
                        db.Update(item);
                        StartActivity(typeof(cart_view));

                    }
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {

                db.Close();
            }
        }
    }
}