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
    class level3
    {
        [PrimaryKey]
        public int id3 { get; set; }
        public string name3 { get; set; }
        //public byte[] img3 { get; set; }
        public DateTime date3 { get; set; }
        public int id2 { get; set; }
    }
}