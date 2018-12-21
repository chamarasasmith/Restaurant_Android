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
    class Ref_Info
    {
        [PrimaryKey]
        public int id1 { get; set; }
        public string st { get; set; }
    }
}