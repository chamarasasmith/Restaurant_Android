using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Restaurant_Android
{
    class Adapter1 : BaseAdapter
    {

        List<level1> mitems;
        Activity mcontext;

        public Adapter1(Activity c, List<level1> item)
        {

            mitems = item;
            mcontext = c;
        }

        public override int Count
        {
            get
            {
                return mitems.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? mcontext.LayoutInflater.Inflate(Resource.Layout.listview_row1, parent, false);

            TextView text = view.FindViewById<TextView>(Resource.Id.txtname1);
            text.Text = mitems[position].name1;
            ImageView img = view.FindViewById<ImageView>(Resource.Id.imgBtn11);

            string filePath = DB.ipath + "L1" + mitems[position].id1 + ".jpeg";

            using (Java.IO.File file = new Java.IO.File(filePath))
            {
                if (file.Exists())
                {
                    img.SetImageURI(Android.Net.Uri.Parse(filePath));
                }
            }

            
            

            return view;
        }
    }
}