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
    
    class MyViewAdpter3:BaseAdapter<level3>
    {

        public List<level3> mitems;
        private Context mcontext;
        public MyViewAdpter3(Context c, List<level3> item)
        {

            mitems = item;
            mcontext = c;
        }

        public override level3 this[int position]
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
                    row = LayoutInflater.From(mcontext).Inflate(Resource.Layout.listview_row3, null, false);
                }
                TextView text = row.FindViewById<TextView>(Resource.Id.txtname3);
                text.Text = mitems[position].name3;
                //string s = mitems[position].img3;
                ImageView img = row.FindViewById<ImageView>(Resource.Id.imgBtn3);

                //img.SetImageURI(Android.Net.Uri.Parse(s));

                //byte[] b = mitems[position].img3;
                
                //Bitmap bitmap = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                    //img.SetImageBitmap(bitmap);

                    string filePath = DB.ipath + "L3" + mitems[position].id3 + ".jpeg";
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