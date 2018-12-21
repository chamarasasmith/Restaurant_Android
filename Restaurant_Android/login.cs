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
    [Activity(Label = "Login", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class login : Activity
    {
        EditText un;
        EditText pw;
        Button login1;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.login);

            un=FindViewById<EditText>(Resource.Id.txtun1);
            pw = FindViewById<EditText>(Resource.Id.txtpwd1);
            login1 = FindViewById<Button>(Resource.Id.btnlog1);
            
            login1.Click += Login1_Click;
           
        }

        
            private void Login1_Click(object sender, EventArgs e)
        {
            try
            {

                if (un.Text=="mcs" && pw.Text=="#image123")
                {
                    un.Text = "";
                    pw.Text = "";
                    StartActivity(typeof(admin1));
                }
                else
                {
                    string dpPath = DB.path;
                    var db = new SQLiteConnection(dpPath);

                    try
                    {
                        var data = db.Table<LoginTable>(); //Call Table  
                        var data1 = data.Where(x => x.username == un.Text && x.password == pw.Text).FirstOrDefault(); //Linq Query  
                        if (data1 != null)
                        {
                            un.Text = "";
                            pw.Text = "";
                            Toast.MakeText(this, "Login Success", ToastLength.Short).Show();
                            StartActivity(typeof(admin1));
                        }
                        else
                        {
                            un.Text = "";
                            pw.Text = "";
                            Toast.MakeText(this, "Username or Password is invalid", ToastLength.Short).Show();
                        }
                    }
                    catch (Exception ex1)
                    {
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
                Toast.MakeText(this, ex.Message.ToString(), ToastLength.Short).Show();
            }
        }
        
    }
}