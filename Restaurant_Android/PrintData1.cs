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
using SQLite;

namespace Restaurant_Android
{
    class PrintData1
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string i_name { get; set; }
        public string i_type { get; set; }
        public string s_req1 { get; set; }
        public string s_req { get; set; }
        public int qty { get; set; }
        public string price1 { get; set; }
        public string t_no { get; set; }
        public string t_pos { get; set; }
        public string c_no { get; set; }
        public string ref_id { get; set; }
        public string un { get; set; }

    }
}