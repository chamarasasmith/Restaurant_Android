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

namespace Restaurant_Android
{
    class CartViewAdapter:BaseAdapter<Cart1>
    {
        public List<Cart1> mitems;
        private Context mcontext;

        public CartViewAdapter(Context c, List<Cart1> item)
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

            if (row == null)
            {
                row = LayoutInflater.From(mcontext).Inflate(Resource.Layout.cartviewadapter, null, false);
            }
            TextView text1 = row.FindViewById<TextView>(Resource.Id.txtname6);
            TextView text2 = row.FindViewById<TextView>(Resource.Id.txtname7);
            TextView text3 = row.FindViewById<TextView>(Resource.Id.txtname8);
            text1.Text = mitems[position].citem;
            double d = mitems[position].cqty * double.Parse(mitems[position].cprice);
            text2.Text = "Qty : " + mitems[position].cqty;
            text3.Text = "Price : " + d;
            //string s = mitems[position].img4;
            ImageView img = row.FindViewById<ImageView>(Resource.Id.imgBtn6);


            //img.SetImageURI(Android.Net.Uri.Parse(s));

            //byte[] b = mitems[position].cimg;

            
                //Bitmap bitmap = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                //img.SetImageBitmap(bitmap);

                string filePath = DB.ipath + "L4" + mitems[position].iid + ".jpeg";
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

            return row;
        }
    }
}