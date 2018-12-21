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
using System.IO;
using Android.Media;
using Android.Graphics;
using Java.IO;

namespace Restaurant_Android
{
    class MyViewAdpter1:BaseAdapter<level1>
    {
        public List<level1> mitems;
        private Context mcontext;

        public MyViewAdpter1(Context c, List<level1> item)
        {

            mitems = item;
            mcontext = c;
        }

        public override level1 this[int position]
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

                if (mitems != null)
                {
                    if (row == null)
                    {
                        row = LayoutInflater.From(mcontext).Inflate(Resource.Layout.listview_row1, parent, false);
                    }
                    TextView text = row.FindViewById<TextView>(Resource.Id.txtname1);
                    text.Text = mitems[position].name1;
                    //string s = mitems[position].img1;
                    ImageView img = row.FindViewById<ImageView>(Resource.Id.imgBtn11);


                    //img.SetImageURI(Android.Net.Uri.Parse(s));

                    //byte[] b = mitems[position].img1;
                    

                        //Bitmap bitmap = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                        //img.SetImageBitmap(bitmap);
                        string filePath = DB.ipath + "L1" + mitems[position].id1 + ".jpeg";
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