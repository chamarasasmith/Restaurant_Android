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
{   [Table("level1")]
    class level1
    {   
        [PrimaryKey]
        public int id1 { get; set; }
        public string name1 { get; set; }
        //public byte[] img1 { get; set; }
        public DateTime date1 { get; set; }
    }
}