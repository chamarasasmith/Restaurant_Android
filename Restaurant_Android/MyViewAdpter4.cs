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
    class MyViewAdpter4:BaseAdapter<level4>
    {
        public List<level4> mitems;
        private Context mcontext;

        public MyViewAdpter4(Context c, List<level4> item)
        {

            mitems = item;
            mcontext = c;
        }

        public override level4 this[int position]
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
                    row = LayoutInflater.From(mcontext).Inflate(Resource.Layout.listview_row5, null, false);
                }
                TextView text = row.FindViewById<TextView>(Resource.Id.txtname5);
                text.Text = mitems[position].name4;
                //string s = mitems[position].img4;
                ImageView img = row.FindViewById<ImageView>(Resource.Id.imgBtn5);


                //img.SetImageURI(Android.Net.Uri.Parse(s));

                //byte[] b = mitems[position].img4;
                
                    //Bitmap bitmap = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                    //img.SetImageBitmap(bitmap);

                    string filePath = DB.ipath + "L4" + mitems[position].id4 + ".jpeg";
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
        public void Save1(string directory, Bitmap bitmap)
        {
            var fileName = directory;
            using (var os = new FileStream(fileName, FileMode.CreateNew))
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, os);
            }
        }
    }
}