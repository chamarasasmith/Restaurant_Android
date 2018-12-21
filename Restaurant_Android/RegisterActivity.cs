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
    [Activity(Label = "Create New User")]
    public class RegisterActivity : Activity
    {
        EditText txtusername;
        EditText txtPassword;
        Button btncreate;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Newuser);
            // Create your application here

            btncreate = FindViewById<Button>(Resource.Id.btnregister);
            txtusername = FindViewById<EditText>(Resource.Id.editText3);
            txtPassword = FindViewById<EditText>(Resource.Id.editText4);
            
            btncreate.Click += Btncreate_Click;
            
        }
        private void Btncreate_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "In", ToastLength.Short).Show();
            try
            {
                string dpPath = DB.path;
                //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "user.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<LoginTable>();
                LoginTable tbl = new LoginTable();
                tbl.username = txtusername.Text;
                tbl.password = txtPassword.Text;
                db.Insert(tbl);
                txtusername.Text = "";
                txtPassword.Text = "";
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {

                txtusername.Text = "";
                txtPassword.Text = "";
                Toast.MakeText(this,"ex"+ ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}