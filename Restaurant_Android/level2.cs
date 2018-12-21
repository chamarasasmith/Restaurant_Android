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
    class level2
    {
        [PrimaryKey]
        public int id2 { get; set; }
        public string name2 { get; set; }
        //public byte[] img2 { get; set; }
        public DateTime date2 { get; set; }
        public int id1 { get; set; }

    }
}