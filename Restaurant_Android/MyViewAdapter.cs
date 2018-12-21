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
    class MyViewAdapter : BaseAdapter<level2>
    {

        public List<level2> mitems;
        private Context mcontext;

        public MyViewAdapter(Context c, List<level2> item) {

            mitems = item;
            mcontext = c;
        }

        public override level2 this[int position] {
            get {return mitems[position]; }
        }

        public override int Count {
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
                        row = LayoutInflater.From(mcontext).Inflate(Resource.Layout.listview_row, parent, false);
                    }
                    TextView text = row.FindViewById<TextView>(Resource.Id.txtname);
                    text.Text = mitems[position].name2;
                    //string s= mitems[position].img2;
                    ImageView img = row.FindViewById<ImageView>(Resource.Id.imgBtn1);

                    //img.SetImageURI(Android.Net.Uri.Parse(s));

                    //byte[] b = mitems[position].img2;

                        //Bitmap bitmap = BitmapFactory.DecodeByteArray(b, 0, b.Length);
                        ////    //img.SetImageBitmap(bitmap);

                        string filePath = DB.ipath + "L2" + mitems[position].id2 + ".jpeg";
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

     
    }
}