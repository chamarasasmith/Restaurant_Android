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
    class Adapter3 : BaseAdapter
    {
        List<level3> mitems;
        Activity mcontext;

        public Adapter3(Activity c, List<level3> item)
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
            var view = convertView ?? mcontext.LayoutInflater.Inflate(Resource.Layout.listview_row3, parent, false);

            TextView text = view.FindViewById<TextView>(Resource.Id.txtname3);
            text.Text = mitems[position].name3;
            ImageView img = view.FindViewById<ImageView>(Resource.Id.imgBtn3);

            string filePath = DB.ipath + "L3" + mitems[position].id3 + ".jpeg";

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