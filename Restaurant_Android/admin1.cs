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
using Android.Graphics;
using Java.IO;
using Java.Nio;
using System.Data;
using Android.Net;
using System.Threading.Tasks;
using System.Timers;
using System.Net;

namespace Restaurant_Android
{
    [Activity(Label = "Admin Panel", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class admin1 : Activity
    {
        Button b1;
        Button b2;
        Button b3;
        Button b4;
        private Button br1;
        private Button br2;
        private Button br3;
        private Button br4;
        private Button bre4;
        private Button bre3;
        private Button bre2;
        private Button bre1;
        private Button bcreate;
        private Button bsync;
        private Button bsync2;
        private Button bsync3;
        private Button setip;
        private Button check1;
        private Button discon;
        private EditText ipa;
        private ImageButton st1;
        private TextView st2;
        private ImageButton bhome1;
        Timer timer;
        string dpPath = DB.path;
        //string constring = DB.constring;
        string constring = DB.getIp();
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.admin1);
            

            b1 = FindViewById<Button>(Resource.Id.btnl1);
            b2 = FindViewById<Button>(Resource.Id.btnl2);
            b3 = FindViewById<Button>(Resource.Id.btnl3);
            b4 = FindViewById<Button>(Resource.Id.btnl4);

            br1 = FindViewById<Button>(Resource.Id.btnlr1);
            br2 = FindViewById<Button>(Resource.Id.btnlr2);
            br3 = FindViewById<Button>(Resource.Id.btnlr3);
            br4 = FindViewById<Button>(Resource.Id.btnlr4);

            bre4 = FindViewById<Button>(Resource.Id.btnlre4);
            bre3 = FindViewById<Button>(Resource.Id.btnlre3);
            bre2 = FindViewById<Button>(Resource.Id.btnlre2);
            bre1 = FindViewById<Button>(Resource.Id.btnlre1);



            bcreate = FindViewById<Button>(Resource.Id.btncreate);
            bsync = FindViewById<Button>(Resource.Id.btnsync1);
            bsync2 = FindViewById<Button>(Resource.Id.btnsync2);
            bsync3 = FindViewById<Button>(Resource.Id.btnsync3);

            setip = FindViewById<Button>(Resource.Id.btnsetip);
            check1 = FindViewById<Button>(Resource.Id.btncheck);
            discon = FindViewById<Button>(Resource.Id.btndiscon);
            ipa = FindViewById<EditText>(Resource.Id.txtip);

            
            bhome1 = FindViewById<ImageButton>(Resource.Id.btnahome1);

            b1.Click += B1_Click;
            b2.Click += B2_Click;
            b3.Click += B3_Click;
            b4.Click += B4_Click;
            bhome1.Click += Bhome1_Click;

            br1.Click += Br1_Click;
            br2.Click += Br2_Click;
            br3.Click += Br3_Click;
            br4.Click += Br4_Click;

            bre4.Click += Bre4_Click;
            bre3.Click += Bre3_Click;
            bre2.Click += Bre2_Click;
            bre1.Click += Bre1_Click;

            bcreate.Click += Bcreate_Click;
            bsync.Click += Bsync_ClickAsync;
            bsync2.Click += Bsync2_Click;
            bsync3.Click += Bsync3_Click;

            discon.Click += Discon_Click;
            setip.Click += Setip_Click;
            check1.Click += Check1_Click;

            //if (DB.checkConnection1())
            //{

            //    st1.SetImageResource(Resource.Drawable.conn);
            //    st2.Text = "Connected";

            //}
            //else
            //{
            //    st1.SetImageResource(Resource.Drawable.dis);
            //    st2.Text = "Disconnected";
            //}

            //timer = new Timer();
            //timer.Interval = 1000;
            //timer.Elapsed += Timer_Elapsed;
            //timer.Start();
           
           

        }

        private void Check1_Click(object sender, EventArgs e)
        {
            if (DB.checkConnection1())
            {
                Toast.MakeText(this, "Connected", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Disconnected", ToastLength.Long).Show();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            Toast.MakeText(this, "Sync", ToastLength.Long).Show();

                UpdateLevel1ToSqliteDB();
                updateLevel2ToSqliteDB();
                updateLevel3ToSqliteDB();
                updateLevel4ToSqliteDB();

                syncLevel1ToSqliteDB();
                syncLevel2ToSqliteDB();
                syncLevel3ToSqliteDB();
                syncLevel4ToSqliteDB();

                DeleteFromSqliteDB1();
                DeleteFromSqliteDB2();
                DeleteFromSqliteDB3();
                DeleteFromSqliteDB4();
            
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            Toast.MakeText(this, "!", ToastLength.Short).Show();
        }


        private void Discon_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
            alertDiag.SetTitle("Confirm Disconnect");
            alertDiag.SetMessage("Desconnect Connection");
            alertDiag.SetPositiveButton("Desconnect", (senderAlert, args) => {
                SQLiteConnection db = new SQLiteConnection(dpPath);
                try
                {
                    //db.DeleteAll<IP_Address1>();
                    db.DropTable<IP_Address1>();
                    StartActivity(typeof(admin1));
                    Toast.MakeText(this, "Success..!", ToastLength.Short).Show();
                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
                finally
                {
                    db.Close();
                }
            });
            alertDiag.SetNegativeButton("Cancel", (senderAlert, args) => {
                alertDiag.Dispose();
            });
            Dialog diag = alertDiag.Create();
            diag.Show();
        }

        private void Bsync3_Click(object sender, EventArgs e)
        {

            //RunOnUiThread(() => UpdateLevel1ToSqliteDB());
            //RunOnUiThread(() => updateLevel2ToSqliteDB());
            //RunOnUiThread(() => updateLevel3ToSqliteDB());
            //RunOnUiThread(() => updateLevel4ToSqliteDB());

            UpdateLevel1ToSqliteDB();
            updateLevel2ToSqliteDB();
            updateLevel3ToSqliteDB();
            updateLevel4ToSqliteDB();

            //while (true) { 

            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => UpdateLevel1ToSqliteDB()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));
            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => updateLevel2ToSqliteDB()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));
            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => updateLevel3ToSqliteDB()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));
            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => updateLevel4ToSqliteDB()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));

            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => DeleteFromSqliteDB1()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));
            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => DeleteFromSqliteDB2()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));
            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => DeleteFromSqliteDB3()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));
            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => DeleteFromSqliteDB4()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));


            //}


            //RunOnUiThread(() => DeleteFromSqliteDB1());
            //RunOnUiThread(() => DeleteFromSqliteDB2());
            //RunOnUiThread(() => DeleteFromSqliteDB3());
            //RunOnUiThread(() => DeleteFromSqliteDB4());

            DeleteFromSqliteDB1();
            DeleteFromSqliteDB2();
            DeleteFromSqliteDB3();
            DeleteFromSqliteDB4();


        }

        private void Setip_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = DB.path;
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<IP_Address1>();
                IP_Address1 tbl = new IP_Address1();
                tbl.id1 = 1;
                tbl.ip1 = ipa.Text.Trim();

                db.Insert(tbl);
                Toast.MakeText(this, "Save Successfully...", ToastLength.Short).Show();
                StartActivity(typeof(admin1));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }
        }



        private void Bsync2_Click(object sender, EventArgs e)
        {

            //RunOnUiThread(() => syncLevel1ToSqliteDB());
            //RunOnUiThread(() => syncLevel2ToSqliteDB());
            //RunOnUiThread(() => syncLevel3ToSqliteDB());
            //RunOnUiThread(() => syncLevel4ToSqliteDB());

            syncLevel1ToSqliteDB();
            syncLevel2ToSqliteDB();
            syncLevel3ToSqliteDB();
            syncLevel4ToSqliteDB();


            //for (int i = 0; i < 300; i++)
            //{
            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => syncLevel1ToSqliteDB()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));
            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => syncLevel2ToSqliteDB()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));
            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => syncLevel3ToSqliteDB()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));
            //    RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show());
            //    await Task.Run(() => syncLevel4ToSqliteDB()).ContinueWith(result => RunOnUiThread(() => Toast.MakeText(this, "Sync...!", ToastLength.Short).Show()));
            //}



        }
        private void Bsync_ClickAsync(object sender, EventArgs e)
        {
            //RunOnUiThread(() => Save1Images(0));
            //RunOnUiThread(() => Save2Images(0));
            //RunOnUiThread(() => Save3Images(0));
            //RunOnUiThread(() => Save4Images(0));

            Save1Images(0);
            Save2Images(0);
            Save3Images(0);
            Save4Images(0);


            //syncLevel1toServer();
            //syncLevel2toServer();
            //syncLevel3toServer();
            //syncLevel4toServer();

        }

        private void clearDb1(int id) {

            SqlConnection myConnection = new SqlConnection(constring);
            try
            {

                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("DELETE FROM Deleted_Items WHERE id1='" + id + "'", myConnection);

                int i = myCommand.ExecuteNonQuery();

            }
            catch (Exception)
            {
            }
            finally
            {
                myConnection.Close();
            }

        }

        private void DeleteFromSqliteDB1()
        {

            try
            {
                SqlConnection myConnection = new SqlConnection(constring);
                SQLiteConnection db = new SQLiteConnection(dpPath);
                try
                {
                    SqlDataReader myReader = null;
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand("select * from Deleted_Items where level_name='level1'", myConnection);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                        int id1 = int.Parse(myReader["id1"].ToString().Trim());
                        int del_id = int.Parse(myReader["del_id"].ToString().Trim());


                        var data = db.Table<level1>();
                        var data1 = data.Where(x => x.id1 == del_id).FirstOrDefault<level1>(); //Linq Query  

                        string file1 =DB.ipath + "L1" + del_id + ".jpeg";

                        if (data1 != null)
                        {
                            db.Delete<level1>(del_id);

                            if (System.IO.File.Exists(file1))
                            {
                                System.IO.File.Delete(file1);
                            }

                            clearDb1(id1);
                            Toast.MakeText(this, data1.name1 + " Deleted", ToastLength.Short).Show();
                        }
                        else
                        {


                        }

                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
                finally
                {
                    db.Close();
                    myConnection.Close();
                }
            }
            catch (Exception ex1)
            {

                Toast.MakeText(this, ex1.Message, ToastLength.Long).Show();
            }


        }



        private void DeleteFromSqliteDB2()
        {

            try
            {
                SqlConnection myConnection = new SqlConnection(constring);
                SQLiteConnection db = new SQLiteConnection(dpPath);
                try
                {
                    SqlDataReader myReader = null;
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand("select * from Deleted_Items where level_name='level2'", myConnection);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                        int id1 = int.Parse(myReader["id1"].ToString().Trim());
                        int del_id = int.Parse(myReader["del_id"].ToString().Trim());


                        var data = db.Table<level2>();
                        var data1 = data.Where(x => x.id2 == del_id).FirstOrDefault<level2>(); //Linq Query  

                        string file1 = DB.ipath + "L2" + del_id + ".jpeg";

                        if (data1 != null)
                        {
                            db.Delete<level2>(del_id);

                            if (System.IO.File.Exists(file1))
                            {
                                System.IO.File.Delete(file1);
                            }

                            clearDb1(id1);
                            Toast.MakeText(this, data1.name2 + " Deleted", ToastLength.Short).Show();
                        }
                        else
                        {


                        }

                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
                finally
                {
                    db.Close();
                    myConnection.Close();
                }
            }
            catch (Exception ex1)
            {

                Toast.MakeText(this, ex1.Message, ToastLength.Long).Show();
            }


        }


        private void DeleteFromSqliteDB3()
        {

            try
            {
                SqlConnection myConnection = new SqlConnection(constring);
                SQLiteConnection db = new SQLiteConnection(dpPath);
                try
                {
                    SqlDataReader myReader = null;
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand("select * from Deleted_Items where level_name='level3'", myConnection);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                        int id1 = int.Parse(myReader["id1"].ToString().Trim());
                        int del_id = int.Parse(myReader["del_id"].ToString().Trim());


                        var data = db.Table<level3>();
                        var data1 = data.Where(x => x.id3 == del_id).FirstOrDefault<level3>(); //Linq Query  

                        string file1 = DB.ipath + "L3" + del_id + ".jpeg";

                        if (data1 != null)
                        {
                            db.Delete<level3>(del_id);

                            if (System.IO.File.Exists(file1))
                            {
                                System.IO.File.Delete(file1);
                            }

                            clearDb1(id1);
                            Toast.MakeText(this, data1.name3 + " Deleted", ToastLength.Short).Show();
                        }
                        else
                        {


                        }

                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
                finally
                {
                    db.Close();
                    myConnection.Close();
                }
            }
            catch (Exception ex1)
            {

                Toast.MakeText(this, ex1.Message, ToastLength.Long).Show();
            }


        }



        private void DeleteFromSqliteDB4()
        {

            try
            {
                SqlConnection myConnection = new SqlConnection(constring);
                SQLiteConnection db = new SQLiteConnection(dpPath);
                try
                {
                    SqlDataReader myReader = null;
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand("select * from Deleted_Items where level_name='level4'", myConnection);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                        int id1 = int.Parse(myReader["id1"].ToString().Trim());
                        int del_id = int.Parse(myReader["del_id"].ToString().Trim());


                        var data = db.Table<level4>();
                        var data1 = data.Where(x => x.id4 == del_id).FirstOrDefault<level4>(); //Linq Query  

                        string file1 = DB.ipath + "L4" + del_id + ".jpeg";

                        if (data1 != null)
                        {
                            db.Delete<level4>(del_id);

                            if (System.IO.File.Exists(file1))
                            {
                                System.IO.File.Delete(file1);
                            }

                            clearDb1(id1);
                            Toast.MakeText(this, data1.name4 + " Deleted", ToastLength.Short).Show();
                        }
                        else
                        {


                        }

                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
                finally
                {
                    db.Close();
                    myConnection.Close();
                }
            }
            catch (Exception ex1)
            {

                Toast.MakeText(this, ex1.Message, ToastLength.Long).Show();
            }


        }


        private void UpdateLevel1ToSqliteDB()
        {

            try
            {
                SqlConnection myConnection = new SqlConnection(constring);
                SQLiteConnection db = new SQLiteConnection(dpPath);
                try
                {
                    SqlDataReader myReader = null;
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand("select * from level1", myConnection);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {



                        int id = int.Parse(myReader["id1"].ToString().Trim());
                        string name = myReader["name1"].ToString().Trim();
                        DateTime date1 = (DateTime)myReader["date1"];
                        //byte[] img1 = (byte[])myReader["img1"];


                        var data = db.Table<level1>();
                        var data1 = data.Where(x => x.id1 == id).FirstOrDefault<level1>(); //Linq Query  
                        
                        if (data1 != null)
                        {

                            if (data1.date1.ToString()!=date1.ToString())
                            {

                                //db.Delete<level1>(id);
                                //db.CreateTable<level1>();
                                //level1 tbl = new level1();
                                //tbl.id1 = id;
                                //tbl.name1 = name;
                                //tbl.date1 = date1;

                                //db.Insert(tbl);
                                //Save1Images(id);
                                //updateServerSt("level1", "id1", id);

                                data1.id1 = id;
                                data1.name1 = name;
                                data1.date1 = date1;
                                db.Update(data1);
                                Save1Images(id);
                                Toast.MakeText(this, name + " Updated", ToastLength.Short).Show();


                            }
                            
                        }
                        else
                        {


                        }

                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
                finally
                {
                    db.Close();
                    myConnection.Close();
                }
            }
            catch (Exception ex1)
            {

                Toast.MakeText(this, ex1.Message, ToastLength.Long).Show();
            }


        }



        private void updateLevel2ToSqliteDB()
        {

            SqlConnection myConnection = new SqlConnection(constring);
            SQLiteConnection db = new SQLiteConnection(dpPath);
            try
            {
                SqlDataReader myReader = null;
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("select * from level2", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    int id = int.Parse(myReader["id2"].ToString().Trim());
                    int id1 = int.Parse(myReader["id1"].ToString().Trim());
                    string name = myReader["name2"].ToString().Trim();
                    DateTime date1 = (DateTime)myReader["date2"];
                    //byte[] img1 = (byte[])myReader["img2"];

                    var data = db.Table<level2>();
                    var data1 = data.Where(x => x.id2 == id).FirstOrDefault<level2>(); //Linq Query  

                    if (data1 != null)
                    {

                        if (data1.date2.ToString() != date1.ToString())
                        {
                            //db.Delete<level2>(id);
                            //db.CreateTable<level2>();
                            //level2 tbl = new level2();
                            //tbl.id2 = id;
                            //tbl.name2 = name;
                            ////tbl.img2 = img1;
                            //tbl.id1 = id1;
                            //db.Insert(tbl);
                            //Save2Images(id);
                            ////updateServerSt("level2", "id2", id);

                            data1.id2 = id;
                            data1.name2 = name;
                            data1.id1 = id1;
                            data1.date2 = date1;
                            db.Update(data1);
                            Save2Images(id);
                            Toast.MakeText(this, name + " Updated", ToastLength.Short).Show();
                        }
                    }
                    else
                    {


                    }

                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            finally
            {
                db.Close();
                myConnection.Close();
            }
        }




        private void updateLevel3ToSqliteDB()
        {

            SqlConnection myConnection = new SqlConnection(constring);
            SQLiteConnection db = new SQLiteConnection(dpPath);
            try
            {
                SqlDataReader myReader = null;
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("select * from level3", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    int id = int.Parse(myReader["id3"].ToString().Trim());
                    int id2 = int.Parse(myReader["id2"].ToString().Trim());
                    string name = myReader["name3"].ToString().Trim();
                    DateTime date1 = (DateTime)myReader["date3"];
                    //byte[] img1 = (byte[])myReader["img3"];

                    var data = db.Table<level3>();
                    var data1 = data.Where(x => x.id3 == id).FirstOrDefault<level3>(); //Linq Query  

                    if (data1 != null)
                    {

                        if (data1.date3.ToString() != date1.ToString())
                        {
                            //db.Delete<level3>(id);
                            //db.CreateTable<level3>();
                            //level3 tbl = new level3();
                            //tbl.id3 = id;
                            //tbl.name3 = name;
                            ////tbl.img3 = img1;
                            //tbl.id2 = id2;
                            //db.Insert(tbl);
                            //Save3Images(id);
                            ////updateServerSt("level3", "id3", id);

                            data1.id3 = id;
                            data1.name3 = name;
                            data1.id2 = id2;
                            data1.date3 = date1;
                            db.Update(data1);
                            Save3Images(id);
                            Toast.MakeText(this, name + " Updated", ToastLength.Short).Show();
                        }
                    }
                    else
                    {


                    }

                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            finally
            {
                db.Close();
                myConnection.Close();
            }
        }


        private void updateLevel4ToSqliteDB()
        {

            SqlConnection myConnection = new SqlConnection(constring);
            SQLiteConnection db = new SQLiteConnection(dpPath);
            try
            {
                SqlDataReader myReader = null;
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("select * from level4", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    int id = int.Parse(myReader["id4"].ToString().Trim());
                    int id3 = int.Parse(myReader["id3"].ToString().Trim());
                    string name = myReader["name4"].ToString().Trim();
                    string price4 = myReader["price4"].ToString().Trim();
                    string des4 = myReader["des4"].ToString().Trim();
                    //byte[] img1 = (byte[])myReader["img4"];
                    DateTime date1 = (DateTime)myReader["date4"];

                    var data = db.Table<level4>();
                    var data1 = data.Where(x => x.id4 == id).FirstOrDefault<level4>(); //Linq Query  

                    if (data1 != null)
                    {
                        if (data1.date4.ToString() != date1.ToString())
                        {
                            //db.Delete<level4>(id);
                            //db.CreateTable<level4>();
                            //level4 tbl = new level4();
                            //tbl.id4 = id;
                            //tbl.name4 = name;
                            ////tbl.img4 = img1;
                            //tbl.id3 = id3;
                            //tbl.price4 = price4;
                            //tbl.des4 = des4;
                            //db.Insert(tbl);
                            //Save4Images(id);
                            ////updateServerSt("level4", "id4", id);

                            data1.id4 = id;
                            data1.name4 = name;
                            data1.id3 = id3;
                            data1.price4 = price4;
                            data1.des4 = des4;
                            data1.date4 = date1;
                            db.Update(data1);
                            Save4Images(id);
                            Toast.MakeText(this, name + " Updated", ToastLength.Short).Show();
                        }
                    }
                    else
                    {


                    }

                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            finally
            {
                db.Close();
                myConnection.Close();
            }
        }


        public void insertImages(string tname, string cid, string cname, string cimg, string i1, string pname)
        {

            try
            {
                SqlConnection myConnection = new SqlConnection(constring);
                try
                {
                    SqlDataReader myReader = null;
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand("select * from " + tname + " where " + cid + "='" + i1 + "'", myConnection);


                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                        int id = int.Parse(myReader[cid].ToString().Trim());
                        string name = myReader[cname].ToString().Trim();
                        byte[] b = (byte[])myReader[cimg];
                        Bitmap bitmap1 = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                        var fileName = DB.ipath + pname + id + ".jpeg";
                        DB.createFolder1();
                        string msg1 = " Image is Saved";
                        if (System.IO.File.Exists(fileName))
                        {
                            System.IO.File.Delete(fileName);
                            msg1 = " Image is Replaced";
                        }
                        using (var os = new FileStream(fileName, FileMode.CreateNew))
                        {
                            bitmap1.Compress(Bitmap.CompressFormat.Jpeg, 100, os);
                            Toast.MakeText(this, name + msg1, ToastLength.Short).Show();
                        }

                       

                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
                finally
                {
                    myConnection.Close();
                }
            }
            catch (Exception ex1)
            {

                Toast.MakeText(this, ex1.Message, ToastLength.Long).Show();
            }

        }




        public void Save1Images(int i1)
        {

            try
            {
                SqlConnection myConnection = new SqlConnection(constring);
                try
                {
                    SqlDataReader myReader = null;
                    myConnection.Open();
                    SqlCommand myCommand;
                    if (i1 > 0)
                    {
                        myCommand = new SqlCommand("select * from level1 where id1='"+i1+"'", myConnection);
                    }
                    else
                    {
                        myCommand = new SqlCommand("select * from level1", myConnection);
                    }

                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                        int id = int.Parse(myReader["id1"].ToString().Trim());
                        string name = myReader["name1"].ToString().Trim();
                        byte[] b = (byte[])myReader["img1"];
                        Bitmap bitmap1 = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                        var fileName = DB.ipath + "L1" + id + ".jpeg";
                        DB.createFolder1();
                        string msg1 = " Image is Saved";

                        if (System.IO.File.Exists(fileName))
                        {
                            System.IO.File.Delete(fileName);
                            msg1 = " Image is Replaced";
                        }
                        using (var os = new FileStream(fileName, FileMode.CreateNew))
                        {   
                            bitmap1.Compress(Bitmap.CompressFormat.Jpeg, 100, os);
                            Toast.MakeText(this, name + msg1, ToastLength.Short).Show();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
                finally
                {
                    myConnection.Close();
                }
            }
            catch (Exception ex1)
            {

                Toast.MakeText(this, ex1.Message, ToastLength.Long).Show();
            }




        }



        public void Save2Images(int i1)
        {

            try
            {
                SqlConnection myConnection = new SqlConnection(constring);
                try
                {
                    SqlDataReader myReader = null;
                    myConnection.Open();
                    SqlCommand myCommand;
                    if (i1 > 0)
                    {
                        myCommand = new SqlCommand("select * from level2 where id2='"+i1+"'", myConnection);
                    }
                    else
                    {
                        myCommand = new SqlCommand("select * from level2", myConnection);
                    }


                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                        int id = int.Parse(myReader["id2"].ToString().Trim());
                        string name = myReader["name2"].ToString().Trim();
                        byte[] b = (byte[])myReader["img2"];
                        Bitmap bitmap1 = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                        var fileName = DB.ipath + "L2" + id + ".jpeg";
                        DB.createFolder1();
                        string msg1 = " Image is Saved";
                        if (System.IO.File.Exists(fileName))
                        {
                            System.IO.File.Delete(fileName);
                            msg1 = " Image is Replaced";
                        }
                        using (var os = new FileStream(fileName, FileMode.CreateNew))
                        {
                            bitmap1.Compress(Bitmap.CompressFormat.Jpeg, 100, os);
                            Toast.MakeText(this, name + msg1, ToastLength.Short).Show();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
                finally
                {
                    myConnection.Close();
                }
            }
            catch (Exception ex1)
            {

                Toast.MakeText(this, ex1.Message, ToastLength.Long).Show();
            }

        }


        public void Save3Images(int i1)
        {

            try
            {
                SqlConnection myConnection = new SqlConnection(constring);
                try
                {
                    SqlDataReader myReader = null;
                    myConnection.Open();
                    SqlCommand myCommand;
                    if (i1 >0)
                    {
                        myCommand = new SqlCommand("select * from level3 where id3='"+i1+"'", myConnection);
                    }
                    else
                    {
                        myCommand = new SqlCommand("select * from level3", myConnection);
                    }


                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                        int id = int.Parse(myReader["id3"].ToString().Trim());
                        string name = myReader["name3"].ToString().Trim();
                        byte[] b = (byte[])myReader["img3"];
                        Bitmap bitmap1 = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                        var fileName = DB.ipath + "L3" + id + ".jpeg";
                        DB.createFolder1();
                        string msg1 = " Image is Saved";
                        if (System.IO.File.Exists(fileName))
                        {
                            System.IO.File.Delete(fileName);
                            msg1 = " Image is Replaced";
                        }
                        using (var os = new FileStream(fileName, FileMode.CreateNew))
                        {
                            bitmap1.Compress(Bitmap.CompressFormat.Jpeg, 100, os);
                            Toast.MakeText(this, name + msg1, ToastLength.Short).Show();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
                finally
                {
                    myConnection.Close();
                }
            }
            catch (Exception ex1)
            {

                Toast.MakeText(this, ex1.Message, ToastLength.Long).Show();
            }

        }


        public void Save4Images(int i1)
        {

            try
            {
                SqlConnection myConnection = new SqlConnection(constring);
                try
                {
                    SqlDataReader myReader = null;
                    myConnection.Open();
                    SqlCommand myCommand;
                    if (i1 > 0)
                    {
                        myCommand = new SqlCommand("select * from level4 where id4='"+i1+"'", myConnection);
                    }
                    else
                    {
                        myCommand = new SqlCommand("select * from level4", myConnection);
                    }


                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                        int id = int.Parse(myReader["id4"].ToString().Trim());
                        string name = myReader["name4"].ToString().Trim();
                        byte[] b = (byte[])myReader["img4"];
                        Bitmap bitmap1 = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                        var fileName = DB.ipath + "L4" + id + ".jpeg";
                        DB.createFolder1();
                        string msg1 = " Image is Saved";
                        if (System.IO.File.Exists(fileName))
                        {
                            System.IO.File.Delete(fileName);
                            msg1 = " Image is Replaced";
                        }
                        using (var os = new FileStream(fileName, FileMode.CreateNew))
                        {
                            bitmap1.Compress(Bitmap.CompressFormat.Jpeg, 100, os);
                            Toast.MakeText(this, name + msg1, ToastLength.Short).Show();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
                finally
                {
                    myConnection.Close();
                }
            }
            catch (Exception ex1)
            {

                Toast.MakeText(this, ex1.Message, ToastLength.Long).Show();
            }

        }



        private void syncLevel1ToSqliteDB()
        {

            try
            {
                SqlConnection myConnection = new SqlConnection(constring);
                SQLiteConnection db = new SQLiteConnection(dpPath);
                try
                {
                    SqlDataReader myReader = null;
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand("select * from level1", myConnection);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {

                        int id = int.Parse(myReader["id1"].ToString().Trim());
                        string name = myReader["name1"].ToString().Trim();
                        DateTime date1 = (DateTime)myReader["date1"];
                        //byte[] img1 = (byte[])myReader["img1"];

                        var data = db.Table<level1>();
                        var data1 = data.Where(x => x.id1 == id).FirstOrDefault<level1>(); //Linq Query  

                        if (data1 != null)
                        {

                        }
                        else
                        {

                            db.CreateTable<level1>();
                            level1 tbl = new level1();
                            tbl.id1 = id;
                            tbl.name1 = name;
                            tbl.date1 = date1;
                            //tbl.img1 = img1;
                            db.Insert(tbl);
                            insertImages("level1", "id1", "name1", "img1", id + "", "L1");
                            Toast.MakeText(this, name + " Inserted", ToastLength.Short).Show();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
                finally
                {
                    db.Close();
                    myConnection.Close();
                }
            }
            catch (Exception ex1)
            {

                Toast.MakeText(this, ex1.Message, ToastLength.Long).Show();
            }


        }



        private void syncLevel2ToSqliteDB()
        {

            SqlConnection myConnection = new SqlConnection(constring);
            SQLiteConnection db = new SQLiteConnection(dpPath);
            try
            {
                SqlDataReader myReader = null;
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("select * from level2", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    int id = int.Parse(myReader["id2"].ToString().Trim());
                    int id1 = int.Parse(myReader["id1"].ToString().Trim());
                    string name = myReader["name2"].ToString().Trim();
                    //byte[] img1 = (byte[])myReader["img2"];
                    DateTime date1 = (DateTime)myReader["date2"];

                    var data = db.Table<level2>();
                    var data1 = data.Where(x => x.id2 == id).FirstOrDefault<level2>(); //Linq Query  

                    if (data1 != null)
                    {

                    }
                    else
                    {

                        db.CreateTable<level2>();
                        level2 tbl = new level2();
                        tbl.id2 = id;
                        tbl.name2 = name;
                        //tbl.img2 = img1;
                        tbl.date2 = date1;
                        tbl.id1 = id1;
                        db.Insert(tbl);
                        insertImages("level2", "id2", "name2", "img2", id + "", "L2");
                        Toast.MakeText(this, name + " Inserted", ToastLength.Short).Show();
                    }

                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            finally
            {
                db.Close();
                myConnection.Close();
            }
        }



        private void syncLevel3ToSqliteDB()
        {

            SqlConnection myConnection = new SqlConnection(constring);
            SQLiteConnection db = new SQLiteConnection(dpPath);
            try
            {
                SqlDataReader myReader = null;
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("select * from level3", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    int id = int.Parse(myReader["id3"].ToString().Trim());
                    int id2 = int.Parse(myReader["id2"].ToString().Trim());
                    string name = myReader["name3"].ToString().Trim();
                    //byte[] img1 = (byte[])myReader["img3"];
                    DateTime date1 = (DateTime)myReader["date3"];

                    var data = db.Table<level3>();
                    var data1 = data.Where(x => x.id3 == id).FirstOrDefault<level3>(); //Linq Query  

                    if (data1 != null)
                    {

                    }
                    else
                    {

                        db.CreateTable<level3>();
                        level3 tbl = new level3();
                        tbl.id3 = id;
                        tbl.name3 = name;
                        //tbl.img3 = img1;
                        tbl.id2 = id2;
                        tbl.date3 = date1;
                        db.Insert(tbl);
                        insertImages("level3", "id3", "name3", "img3", id + "", "L3");
                        Toast.MakeText(this, name + " Inserted", ToastLength.Short).Show();
                    }

                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            finally
            {
                db.Close();
                myConnection.Close();
            }
        }


        private void syncLevel4ToSqliteDB()
        {

            SqlConnection myConnection = new SqlConnection(constring);
            SQLiteConnection db = new SQLiteConnection(dpPath);
            try
            {
                SqlDataReader myReader = null;
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("select * from level4", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    int id = int.Parse(myReader["id4"].ToString().Trim());
                    int id3 = int.Parse(myReader["id3"].ToString().Trim());
                    string name = myReader["name4"].ToString().Trim();
                    string price4 = myReader["price4"].ToString().Trim();
                    string des4 = myReader["des4"].ToString().Trim();
                    //byte[] img1 = (byte[])myReader["img4"];
                    DateTime date1 = (DateTime)myReader["date4"];

                    var data = db.Table<level4>();
                    var data1 = data.Where(x => x.id4 == id).FirstOrDefault<level4>(); //Linq Query  

                    if (data1 != null)
                    {

                    }
                    else
                    {

                        db.CreateTable<level4>();
                        level4 tbl = new level4();
                        tbl.id4 = id;
                        tbl.name4 = name;
                        //tbl.img4 = img1;
                        tbl.date4 = date1;
                        tbl.id3 = id3;
                        tbl.price4 = price4;
                        tbl.des4 = des4;
                        db.Insert(tbl);
                        insertImages("level4", "id4", "name4", "img4", id + "", "L4");
                        Toast.MakeText(this, name + " Inserted", ToastLength.Short).Show();
                    }

                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            finally
            {
                db.Close();
                myConnection.Close();
            }
        }



        private void syncLevel1toServer()
        {

            SqlConnection myConnection = new SqlConnection(constring);
            string dpPath = DB.path;
            var db = new SQLiteConnection(dpPath);
            try
            {
                myConnection.Open();


                var data = db.Table<level1>(); //Call Table  

                if (data != null)
                {
                    foreach (level1 item in data)
                    {

                        SqlCommand myCommand = new SqlCommand("BEGIN IF NOT EXISTS(SELECT * FROM level1 WHERE id1 = '" + item.id1 + "') BEGIN INSERT INTO level1(id1, name1, img1,u_st) VALUES('" + item.id1 + "', '" + item.name1 + "', @image1,'0') END END", myConnection);
                        System.Data.IDataParameter par = myCommand.CreateParameter();
                        par.ParameterName = "image1";
                        par.DbType = DbType.Binary;
                        //par.Value = item.img1;
                        myCommand.Parameters.Add(par);
                        int i = myCommand.ExecuteNonQuery();
                        Toast.MakeText(this, item.name1 + " Inserted", ToastLength.Short).Show();
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

        private void syncLevel2toServer()
        {

            SqlConnection myConnection = new SqlConnection(constring);
            string dpPath = DB.path;
            var db = new SQLiteConnection(dpPath);
            try
            {
                myConnection.Open();


                var data = db.Table<level2>(); //Call Table  

                if (data != null)
                {
                    foreach (level2 item in data)
                    {
                        SqlCommand myCommand = new SqlCommand("BEGIN IF NOT EXISTS(SELECT * FROM level2 WHERE id2 = '" + item.id2 + "') BEGIN INSERT INTO level2(id2, name2, img2,id1,u_st) VALUES('" + item.id2 + "', '" + item.name2 + "',@image1 , '" + item.id1 + "','0') END END", myConnection);

                        System.Data.IDataParameter par = myCommand.CreateParameter();
                        par.ParameterName = "image1";
                        par.DbType = DbType.Binary;
                        //par.Value = item.img2;
                        myCommand.Parameters.Add(par);
                        int i = myCommand.ExecuteNonQuery();
                        Toast.MakeText(this, item.name2 + " Inserted", ToastLength.Short).Show();
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


        private void syncLevel3toServer()
        {

            SqlConnection myConnection = new SqlConnection(constring);
            string dpPath = DB.path;
            var db = new SQLiteConnection(dpPath);
            try
            {
                myConnection.Open();


                var data = db.Table<level3>(); //Call Table  

                if (data != null)
                {
                    foreach (level3 item in data)
                    {
                        SqlCommand myCommand = new SqlCommand("BEGIN IF NOT EXISTS(SELECT * FROM level3 WHERE id3 = '" + item.id3 + "') BEGIN INSERT INTO level3(id3, name3, img3,id2,u_st) VALUES('" + item.id3 + "', '" + item.name3 + "', @image1, '" + item.id2 + "','0') END END", myConnection);

                        System.Data.IDataParameter par = myCommand.CreateParameter();
                        par.ParameterName = "image1";
                        par.DbType = DbType.Binary;
                        //par.Value = item.img3;
                        myCommand.Parameters.Add(par);
                        int i = myCommand.ExecuteNonQuery();
                        Toast.MakeText(this, item.name3 + " Inserted", ToastLength.Short).Show();
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


        private void syncLevel4toServer()
        {

            SqlConnection myConnection = new SqlConnection(constring);
            string dpPath = DB.path;
            var db = new SQLiteConnection(dpPath);
            try
            {
                myConnection.Open();


                var data = db.Table<level4>(); //Call Table  

                if (data != null)
                {
                    foreach (level4 item in data)
                    {
                        SqlCommand myCommand = new SqlCommand("BEGIN IF NOT EXISTS(SELECT * FROM level4 WHERE id4 = '" + item.id4 + "') BEGIN INSERT INTO level4(id4, name4, img4,des4,price4,id3,u_st) VALUES('" + item.id4 + "', '" + item.name4 + "',  @image1,'" + item.des4 + "','" + item.price4 + "', '" + item.id3 + "','0') END END", myConnection);

                        System.Data.IDataParameter par = myCommand.CreateParameter();
                        par.ParameterName = "image1";
                        par.DbType = DbType.Binary;
                        //par.Value = item.img4;
                        myCommand.Parameters.Add(par);
                        int i = myCommand.ExecuteNonQuery();
                        Toast.MakeText(this, item.name4 + " Inserted", ToastLength.Short).Show();
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



        private void Bcreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }

        private void Bre1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Remove_Level1_Item));
        }

        private void Bre2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Remove_Level2_Item));
        }

        private void Bre3_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Remove_Level3_Item));
        }

        private void Bre4_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Remove_Level4_Item));
        }

        private void Br4_Click(object sender, EventArgs e)
        {


            AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
            alertDiag.SetTitle("Confirm Delete");
            alertDiag.SetMessage("Delete All Items From Sub Menu 3");
            alertDiag.SetPositiveButton("Delete", (senderAlert, args) => {
                SQLiteConnection db = new SQLiteConnection(dpPath);
                try
                {
                    db.DeleteAll<level4>();
                    //db.DropTable<level4>();
                    Toast.MakeText(this, "Success..!", ToastLength.Short).Show();
                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
                finally
                {
                    db.Close();
                }
            });
            alertDiag.SetNegativeButton("Cancel", (senderAlert, args) => {
                alertDiag.Dispose();
            });
            Dialog diag = alertDiag.Create();
            diag.Show();

        }

        private void Br3_Click(object sender, EventArgs e)
        {

            AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
            alertDiag.SetTitle("Confirm Delete");
            alertDiag.SetMessage("Delete All Items From Sub Menu 2");
            alertDiag.SetPositiveButton("Delete", (senderAlert, args) => {
                SQLiteConnection db = new SQLiteConnection(dpPath);
                try
                {
                    db.DeleteAll<level3>();
                    //db.DropTable<level3>();
                    Toast.MakeText(this, "Success..!", ToastLength.Short).Show();
                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
                finally
                {
                    db.Close();
                }
            });
            alertDiag.SetNegativeButton("Cancel", (senderAlert, args) => {
                alertDiag.Dispose();
            });
            Dialog diag = alertDiag.Create();
            diag.Show();



        }

        private void Br2_Click(object sender, EventArgs e)
        {

            AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
            alertDiag.SetTitle("Confirm Delete");
            alertDiag.SetMessage("Delete All Items From Sub Menu 1");
            alertDiag.SetPositiveButton("Delete", (senderAlert, args) => {
                SQLiteConnection db = new SQLiteConnection(dpPath);
                try
                {
                    db.DeleteAll<level2>();
                    //db.DropTable<level2>();
                    Toast.MakeText(this, "Success..!", ToastLength.Short).Show();
                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
                finally
                {
                    db.Close();
                }
            });
            alertDiag.SetNegativeButton("Cancel", (senderAlert, args) => {
                alertDiag.Dispose();
            });
            Dialog diag = alertDiag.Create();
            diag.Show();


        }

        private void Br1_Click(object sender, EventArgs e)
        {

            AlertDialog.Builder alertDiag = new AlertDialog.Builder(this);
            alertDiag.SetTitle("Confirm Delete");
            alertDiag.SetMessage("Delete All Items From Main Menu");
            alertDiag.SetPositiveButton("Delete", (senderAlert, args) => {
                SQLiteConnection db = new SQLiteConnection(dpPath);
                try
                {
                    db.DeleteAll<level1>();
                    //db.DropTable<level1>();
                    Toast.MakeText(this, "Success..!", ToastLength.Short).Show();
                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
                finally
                {
                    db.Close();
                }
            });
            alertDiag.SetNegativeButton("Cancel", (senderAlert, args) => {
                alertDiag.Dispose();
            });
            Dialog diag = alertDiag.Create();
            diag.Show();


        }




        private void updateServerSt(string tname, string cname, int id)
        {

            SqlConnection myConnection = new SqlConnection(constring);

            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("UPDATE " + tname + " SET u_st='0' WHERE " + cname + "='" + id + "'", myConnection);

                int i = myCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                myConnection.Close();
            }

        }




        private void Bhome1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void B4_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Add_Level4));
        }

        private void B3_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Add_Level3));
        }

        private void B2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Add_Level2));
        }

        private void B1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Add_Level1));
        }

        

    }
}