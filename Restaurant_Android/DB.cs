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

namespace Restaurant_Android
{
    public class DB
    {

        static public string path = "/storage/emulated/0/user.sql";
        static public string ipath = "/storage/emulated/0/Restaurant/";

        //static public string constring = "Data Source=192.168.8.100;database=RestaurantDB;User ID=sa;Password=#image123;";

        //static public string path = "D:/user.sql";
        //static public string path = Android.OS.Environment.RootDirectory.ParentFile + "/user.sql";
        //static public string path = Android.OS.Environment.ExternalStorageDirectory+ "/user.sql";

        public static void createFolder1()
        {
           
            try
            {
                if (!Directory.Exists(ipath))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(ipath);
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
        }


            public static string getIp() {

            string constring = "Data Source=192.168.8.110;database=RestaurantDB;User ID=sa;Password=#image123;";
            string dpPath = DB.path;
            var db = new SQLiteConnection(dpPath);
            try
            {
                
                var data = db.Table<IP_Address1>(); //Call Table  
                if (data != null)
                {
                    foreach (IP_Address1 item in data)
                    {
                        constring= "Data Source="+item.ip1+";database=RestaurantDB;User ID=sa;Password=#image123;";
                    }
                }

            }
            catch (Exception)
            {
                db.Close();
            }
            return constring;
        }


        public static bool checkConnection1()
        {

            bool b = false;

            SqlConnection myConnection = new SqlConnection(getIp());
            try
            {
                SqlDataReader myReader = null;
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("select st from config1", myConnection);
                myReader = myCommand.ExecuteReader();
                
                while (myReader.Read())
                {
                    b = true;

                }
            }
            catch (Exception ex)
            {
                b = false;
            }
            finally
            {
                myConnection.Close();
            }

            return b;
        }

    }
}