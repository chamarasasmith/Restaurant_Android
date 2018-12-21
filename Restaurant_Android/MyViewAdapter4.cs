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
using Android.Graphics;
using System.IO;

namespace Restaurant_Android
{
    class MyViewAdapter4: BaseAdapter<Cart1>
    {
        public List<Cart1> mitems;
        private Context mcontext;

        public MyViewAdapter4(Context c, List<Cart1> item)
        {

            mitems = item;
            mcontext = c;
        }

        public override Cart1 this[int position]
        {
            get { return mitems[position]; }
        }

        public override int Count
        {
            get { return mitems.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            try
            {
                if (row == null)
                {
                    row = LayoutInflater.From(mcontext).Inflate(Resource.Layout.listview_row4, null, false);
                }
                TextView text = row.FindViewById<TextView>(Resource.Id.txtname4);
                text.Text = mitems[position].citem;
                //string s = mitems[position].cimg;
                ImageView img = row.FindViewById<ImageView>(Resource.Id.imgBtn4);

                //img.SetImageURI(Android.Net.Uri.Parse(s));
                //byte[] b = mitems[position].cimg;
                
                //Bitmap bitmap = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                    //img.SetImageBitmap(bitmap);

                    string filePath = DB.ipath + "C1" + mitems[position].cid + ".jpeg";
                    Java.IO.File file = new Java.IO.File(filePath);
                    if (!file.Exists())
                    {
                    //Save1(filePath, bitmap);
                    //img.SetImageResource(Resource.Drawable.silk_route);
                }
                    else
                    {
                        img.SetImageURI(Android.Net.Uri.Parse(filePath));
                    }
    
            

            //int id = (int)typeof(Resource.Drawable).GetField(s).GetValue(null);
            //img.SetImageResource(id);

        }
            catch (Exception)
            {
                
            }

            
            return row;
        }

       

    }
}