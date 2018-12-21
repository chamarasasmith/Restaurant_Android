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
using System.Data.SqlClient;
using Android.Net;

namespace Restaurant_Android
{
    [Activity(Label = "Cart View", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class cart_view : Activity
    {
        private List<Cart1> mitems;
        //private ListView mlistView;
        private GridView mlistView;
        private ImageButton home1;
        private ImageButton done1;
        private ImageButton clear1;
        private TextView tol;
        private EditText t_no;
        //string constring = DB.constring;
        string constring = DB.getIp();
        
        private EditText emp_id;
        private EditText pw;
        private Spinner sptpos;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.cartview1);
            // Create your application here
            mlistView = FindViewById<GridView>(Resource.Id.cartgrid1);

            home1 = FindViewById<ImageButton>(Resource.Id.btnhome6);
            done1 = FindViewById<ImageButton>(Resource.Id.btnprint6);
            clear1 = FindViewById<ImageButton>(Resource.Id.btnbin6);

            tol = FindViewById<TextView>(Resource.Id.txttotal);

            t_no = FindViewById<EditText>(Resource.Id.tabno1);

            emp_id = FindViewById<EditText>(Resource.Id.empid);
            pw = FindViewById<EditText>(Resource.Id.emppw);
            
            sptpos = FindViewById<Spinner>(Resource.Id.sptpos1);


            string dpPath = DB.path;
            //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);

            //var items1 = new List<string>();

            //items1.Add("1");
            //items1.Add("2");
            //items1.Add("3");
            //items1.Add("4");

            var items2 = new List<string>();

            items2.Add("A");
            items2.Add("B");
            items2.Add("C");
            items2.Add("D");

            //var adapter1 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, items1);
            var adapter2 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, items2);
            //spcno.Adapter = adapter1;
            sptpos.Adapter = adapter2;

            try
            {

                mitems = new List<Cart1>();
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

                var data = db.Table<Cart1>(); //Call Table  



                /*var data1 = data.Where(x => x.cid == id1).ToList<level2>();*/ //Linq Query  

                //Toast.MakeText(this, data1.Count, ToastLength.Short).Show();

                double dd = 0.0;

                if (data != null)
                {
                    foreach (Cart1 item in data)
                    {
                        double d=0.0;
                        if (item.cprice!=null)
                        {
                            d = item.cqty * double.Parse(item.cprice);
                        }
                        
                        dd += d;
                        mitems.Add(new Cart1() { cid = item.cid, iid=item.iid,cqty=item.cqty ,cprice=item.cprice,citem = item.citem });
                    }
                }
                //tol.Text = "Total Price : " + dd;
                tol.Text = "";
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
            }
            finally {

                db.Close();
            }

            CartViewAdapter adapter = new CartViewAdapter(this, mitems);
            mlistView.Adapter = adapter;

            mlistView.ItemClick += MlistView_ItemClick; ;
            clear1.Click += Clear1_Click;
            home1.Click += Home1_Click;
            done1.Click += Done1_Click;
            

        }


        private string getRef(string t_no, string t_pos)
        {
            string s = "";
            SqlConnection myConnection = new SqlConnection(constring);
            try
            {
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand("SELECT * FROM Ref_Num WHERE st='1' AND t_no='"+t_no+ "' AND t_pos='" + t_pos + "'", myConnection);

                SqlDataReader reader = myCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Toast.MakeText(this, "get", ToastLength.Short).Show();
                        s = reader["ref_id"].ToString().Trim();

                    }
                }
                else
                {
                    s = setRef(t_no, t_pos);
                }
                
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
            finally
            {
                myConnection.Close();
            }
            return s;
        }


        private string setRef(string t_no, string t_pos)
        {
            string s ="";
            SqlConnection myConnection = new SqlConnection(constring);
            try
            {
                myConnection.Open();
                string d1 = DateTime.Now.ToString("yyyy-MM-dd");
                SqlCommand myCommand = new SqlCommand("INSERT INTO Ref_Num (t_no,t_pos,start_time,end_time,st) VALUES ('"+t_no+"','"+t_pos+"','"+d1.ToString()+"','"+d1.ToString() + "','1')", myConnection);

                int reader = myCommand.ExecuteNonQuery();
                if (reader==1)
                {
                    s=getRef(t_no, t_pos);
                }
                
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
            finally
            {
                myConnection.Close();
            }
            return s;
        }

        private void Done1_Click(object sender, EventArgs e)
        {

            if (DB.checkConnection1())
            {

                bool b1= checkValid1();

            if (b1)
            {
                string dpPath = DB.path;
                var db = new SQLiteConnection(dpPath);

                try
                {
                    

                    var data = db.Table<Cart1>(); //Call Table  

                    double dd = 0.0;

                    if (data != null)
                    {
                        foreach (Cart1 item in data)
                        {
                            double d = item.cqty * double.Parse(item.cprice);
                            dd += d;
                            db.CreateTable<PrintData1>();
                            PrintData1 tbl = new PrintData1();

                            tbl.i_name = item.citem;
                            tbl.price1 = item.cprice;
                            tbl.s_req = item.cremark;
                            tbl.s_req1 = item.csp1;
                            tbl.i_type = item.i_type;
                            tbl.qty = item.cqty;
                            tbl.t_no = t_no.Text;
                            tbl.ref_id= getRef(t_no.Text, sptpos.SelectedItem.ToString());
                            tbl.t_pos = sptpos.SelectedItem.ToString();
                            tbl.c_no = item.ccno1;
                            tbl.un = emp_id.Text;
                            db.Insert(tbl);
                            db.Delete<Cart1>(item.cid);
                                
                        }
                    }
                       bool b= syncPrint();
                        if (b)
                        {
                            Toast.MakeText(this, "Print Success..!", ToastLength.Short).Show();
                            StartActivity(typeof(cart_view));
                        }
                        else
                        {
                            Toast.MakeText(this, "Print Unsuccess..!", ToastLength.Short).Show();
                            StartActivity(typeof(cart_view));

                        }



                    }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
                }finally
                {
                    db.Close();
                }
            }


            else
            {
                Toast.MakeText(this, "Employee Not Exist", ToastLength.Short).Show();
            }

            }
            else
            {
                Toast.MakeText(this, "Go to Admin Page and Set Your Server IP", ToastLength.Long).Show();
            }




            //StartActivity(typeof(printview));
        }

        

        bool syncPrint()
        {
            bool b = false;
                try
                {
                    SqlConnection myConnection = new SqlConnection(constring);
                    string dpPath = DB.path;
                    var db = new SQLiteConnection(dpPath);
                    try
                    {
                        myConnection.Open();
                     
                        var data = db.Table<PrintData1>(); //Call Table  

                        if (data != null)
                        {
                            int i = 0;
                            foreach (PrintData1 item in data)
                            {
                            //Toast.MakeText(this, item.t_no+":"+item.t_pos+":"+item.c_no, ToastLength.Long).Show();
                            SqlCommand myCommand = new SqlCommand("INSERT INTO order1 (item_name,qty,s_request,s_request1,vetor_id,status1,price1,t_no,t_po,i_type,order_st,bill_st,ref_num,c_no) VALUES ('" + item.i_name.ToString() + "','" + item.qty.ToString() + "','" + item.s_req.ToString() + "','" + item.s_req1.ToString() + "','" + item.un.ToString() + "','1','" + item.price1.ToString() + "','" + item.t_no.ToString() + "','" + item.t_pos.ToString() + "','" + item.i_type.ToString() + "','1','1','" + item.ref_id.ToString() + "','" + item.c_no.ToString() + "')", myConnection);
                            i = myCommand.ExecuteNonQuery();
                            db.Delete<PrintData1>(item.id);
                            }
                            if (i == 1)
                            {
                            b = true;
                            //Toast.MakeText(this, "1111111111Success...!!!", ToastLength.Long).Show();
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
                        myConnection.Close();
                    }
                }
                catch (Exception ex1)
                {
                    Toast.MakeText(this, ex1.Message.ToString(), ToastLength.Long).Show();

                }


            return b;

            
        }

        private bool checkValid1() {

            bool b = false;
            try
            {

                if (emp_id.Text == "mcs" && pw.Text == "#image123")
                {
                    b = true;
                }
                else
                {
                    string dpPath = DB.path;
                    var db = new SQLiteConnection(dpPath);

                    try
                    {
                        var data = db.Table<LoginTable>(); //Call Table  
                        var data1 = data.Where(x => x.username == emp_id.Text && x.password == pw.Text).FirstOrDefault(); //Linq Query  
                        if (data1 != null)
                        {
                            b = true;
                        }
                        else
                        {
                            b = false;
                        }
                    }
                    catch (Exception ex1)
                    {
                        b = false;
                        Toast.MakeText(this, ex1.Message.ToString(), ToastLength.Short).Show();
                    }
                    finally
                    {
                        db.Close();
                    }



                }


            }
            catch (Exception ex)
            {
                b = false;
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }
            return b;
        }


        private void Home1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void Clear1_Click(object sender, EventArgs e)
        {

            AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
            alertDiag.SetTitle("Clear Cart");
            alertDiag.SetMessage("Clear All Items From Cart");
            alertDiag.SetPositiveButton("Clear", (senderAlert, args) =>
            {

                try
                {
                  
                    string dpPath = DB.path;
                    var db = new SQLiteConnection(dpPath);
                    try
                    {
                        var data = db.Table<Cart1>(); //Call Table  

                        if (data!=null)
                        {
                            db.DeleteAll<Cart1>();
                        }
                        StartActivity(typeof(cart_view));
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
                    }
                    finally
                    {
                        db.Close();
                    }
                }
                catch (Exception ex1)
                {
                    Toast.MakeText(this, ex1.Message.ToString(), ToastLength.Long).Show();

                }




            });
            alertDiag.SetNegativeButton("Cancel", (senderAlert, args) =>
            {
                alertDiag.Dispose();
            });
            Dialog diag = alertDiag.Create();
            diag.Show();

        }

        private void MlistView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var activity2 = new Intent(this, typeof(edit_cart));
            activity2.PutExtra("MyData", mitems[e.Position].cid + "");
            StartActivity(activity2);
        }
    }
}