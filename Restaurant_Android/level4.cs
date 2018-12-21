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
    class level4
    {
        [PrimaryKey]
        public int id4 { get; set; }
        public string name4 { get; set; }
        //public byte[] img4 { get; set; }
        public string des4 { get; set; }
        public string price4 { get; set; }
        public DateTime date4 { get; set; }
        public int id3 { get; set; }
    }
}