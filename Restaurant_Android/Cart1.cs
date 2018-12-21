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
    class Cart1
    {
        [PrimaryKey, AutoIncrement]
        public int cid { get; set; }

        public string iid { get; set; }
        public string citem { get; set; }
        public string cprice { get; set; }
        public string cdes { get; set; }
        public string ccno1 { get; set; }
        public int cqty { get; set; }
        //public byte[] cimg { get; set; }
        public string csp1 { get; set; }
        public string cremark { get; set; }
        public string i_type { get; set; }

    }
}