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
using Android.Graphics;

namespace Restaurant_Android
{
    [Activity(Label = "Order View",WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class orderact : Activity
    {

        TextView title;
        TextView price;
        TextView des;
        ImageView img1;
        Button order;
        ImageButton bhome5;
        private ImageButton bback5;
        EditText qty;
        private EditText cno1;
        EditText remark1;
        private ImageButton bcart5;
        private Spinner sr1;
        private string pre4;
        private string pre5;
        private string pre6;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            GC.Collect();
            // Create your application here
            SetContentView(Resource.Layout.order1);
            
            title= FindViewById<TextView>(Resource.Id.txttitle);
            des = FindViewById<TextView>(Resource.Id.txtdes);
            price = FindViewById<TextView>(Resource.Id.txtprice);
            img1 =FindViewById<ImageView>(Resource.Id.imgm1);
            order=FindViewById<Button>(Resource.Id.btnorder);
            qty= FindViewById<EditText>(Resource.Id.txtqty);

            cno1 = FindViewById<EditText>(Resource.Id.ccno1);

            remark1 = FindViewById<EditText>(Resource.Id.txtremark);
            bhome5 = FindViewById<ImageButton>(Resource.Id.btnhome5);
            bback5 = FindViewById<ImageButton>(Resource.Id.btnback5);
            bcart5 = FindViewById<ImageButton>(Resource.Id.btncart5);

            sr1 = FindViewById<Spinner>(Resource.Id.spsr1);

            cno1.Text = "1";

            var items1 = new List<string>();

            items1.Add("Well Done");
            items1.Add("Low Sugar");
            items1.Add("Without Alcohol");

            var adapter1 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, items1);

            sr1.Adapter = adapter1;

            //bhome5.Enabled = false;
            //bback5.Enabled = false;
            //bcart5.Enabled = false;

            string dpPath = DB.path;
            var db = new SQLiteConnection(dpPath);

            try
            {
                string text = Intent.GetStringExtra("MyData") ?? "0";
                pre4 = Intent.GetStringExtra("pre4") ?? "0";
                pre5 = Intent.GetStringExtra("pre5") ?? "0";
                pre6 = Intent.GetStringExtra("pre6") ?? "0";
                

                int id1 = 0;
                if (string.IsNullOrEmpty(text))
                {
                    id1 = 0;
                }
                else
                {
                    id1 = int.Parse(text);
                }

                
                var data = db.Table<level4>(); //Call Table  

                var data1 = data.Where(x => x.id4 == id1).ToList<level4>(); //Linq Query  
                if (data1 != null)
                {
                    foreach (level4 item in data1)
                    {
                        title.Text = item.id4+":"+item.name4;
                        price.Text = ""+item.price4;

                        if (item.des4==null)
                        {
                            des.Text = "";
                        }
                        else
                        {
                            des.Text = "" + item.des4;
                        }
                        

                        //img1.SetImageURI(Android.Net.Uri.Parse(item.img4));


                       

                            //byte[] b = item.img4;
                            //Bitmap bitmap = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                            //img1.SetImageBitmap(bitmap);

                            string filePath = DB.ipath + "L4" + item.id4 + ".jpeg";
                            Java.IO.File file = new Java.IO.File(filePath);
                            if (file.Exists())
                            {
                            try
                            {
                                img1.SetImageURI(Android.Net.Uri.Parse(filePath));
                            }
                            catch (Exception ex)
                            {
                                StartActivity(typeof(MainActivity));
                            }
                        }
                            

                            //imgid = item.img4;
                        

                        

                        //int id = (int)typeof(Resource.Drawable).GetField(item.img3).GetValue(null);
                        //img1.SetImageResource(id);

                        
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
                bhome5.Enabled = true;
                bback5.Enabled = true;
                bcart5.Enabled = true;
            }

            //Toast.MakeText(this,text, ToastLength.Long).Show();

            try
            {
                order.Click += Order_Click;
                bhome5.Click += Bhome5_Click;
                bback5.Click += Bback5_Click;
                bcart5.Click += Bcart5_Click;

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            
            
        }

       

        private void Bcart5_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(cart_view));
        }

        private void Bback5_Click(object sender, EventArgs e)
        {
            //StartActivity(typeof(lv1act));
            try
            {
                if (pre4 != "0" && pre5 != "0" && pre6 != "0" && pre4 !=null && pre5 != null && pre6 !=null)
                {
                    var activity2 = new Intent(this, typeof(lv4act));
                    activity2.PutExtra("MyData", pre4 + "");
                    activity2.PutExtra("pre2", pre5 + "");
                    activity2.PutExtra("pre3", pre6 + "");
                    StartActivity(activity2);
                }

                
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            
        }

        private void Bhome5_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void Order_Click(object sender, EventArgs e)
        {
            if ( String.IsNullOrEmpty(qty.Text) | String.IsNullOrWhiteSpace(qty.Text))
            {
                Toast.MakeText(this, "Qty or Description is Empty", ToastLength.Short).Show();
            }
            else
            {
                 addCart(1, title.Text.Split(':')[0], title.Text.Split(':')[1], price.Text, des.Text, int.Parse(qty.Text), sr1.SelectedItem.ToString(),remark1.Text,cno1.Text.ToString().Trim());
            }
        }

        public void addCart(int cid,string iid, string citem, string cprice, string cdes, int cqty, string csp1, string cremark, string ccno1)
        {
            string dpPath = DB.path;
            var db = new SQLiteConnection(dpPath);
            try
            {
                db.CreateTable<Cart1>();
                Cart1 tbl = new Cart1();
                tbl.cid = cid;
                tbl.iid = iid;
                tbl.citem = citem;
                tbl.cprice = cprice;
                tbl.cdes = cdes;
                tbl.cremark = cremark;
                tbl.cqty = cqty;
                tbl.csp1 = csp1;
                tbl.ccno1 = ccno1;
                tbl.i_type = pre6;
                db.Insert(tbl);
                Toast.MakeText(this, "Successful...!", ToastLength.Short).Show();
                StartActivity(typeof(lv1act));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }
            finally
            {
                db.Close(); 
            }


        }

        
    }
}