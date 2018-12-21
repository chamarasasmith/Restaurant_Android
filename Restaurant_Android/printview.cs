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
using Android.Print.Pdf;
using Android.Print;
using System.Data.SqlClient;

namespace Restaurant_Android
{
    [Activity(Label = "printview")]
    public class printview : Activity
    {
        private Button pr1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.printview);
            // Create your application here

            pr1=FindViewById<Button>(Resource.Id.print1);

            pr1.Click += Pr1_Click;
        }

        private void Pr1_Click(object sender, EventArgs e)
        {

            string constring = "Data Source=192.168.8.101;database=RestaurantDB;User ID=sa;Password=#image123;";

            SqlConnection myConnection = new SqlConnection(constring);
            try
            {
                SqlDataReader myReader = null;
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("select * from order1", myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    string iname = myReader["item_name"].ToString().Trim();
                    string s_req = myReader["s_request"].ToString().Trim();
                    string qty = myReader["qty"].ToString().Trim();

                    string[] a = s_req.Split(',');

                    Toast.MakeText(this, iname, ToastLength.Long).Show();

                    foreach (var item in a)
                    {
                        string[] row = new string[] { iname, item, qty };
                        //dataGridView1.Rows.Add(row);
                        iname = "";
                        qty = "";
                    }

                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            finally
            {
                myConnection.Close();
            }


            //var printManager = (PrintManager)GetSystemService(Context.PrintService);
            //var content = FindViewById<LinearLayout>(Resource.Id.llv1);
            //var printAdapter = new GenericPrintAdapter(this, content);

            //printManager.Print("MyPrintJob", printAdapter, null);
        }
    }
    public class GenericPrintAdapter : PrintDocumentAdapter
    {
        View view;
        Context context;
        PrintedPdfDocument document;
        float scale;

        public GenericPrintAdapter(Context context, View view)
        {
            this.view = view;
            this.context = context;
        }

        public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes,CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
        {
            document = new PrintedPdfDocument(context, newAttributes);

            CalculateScale(newAttributes);

            var printInfo = new PrintDocumentInfo
                .Builder("MyPrint.pdf")
                .SetContentType(PrintContentType.Document)
                .SetPageCount(1)
                .Build();

            callback.OnLayoutFinished(printInfo, true);
        }

        void CalculateScale(PrintAttributes newAttributes)
        {
            int dpi = Math.Max(newAttributes.GetResolution().HorizontalDpi, newAttributes.GetResolution().VerticalDpi);

            int leftMargin = (int)(dpi * (float)newAttributes.MinMargins.LeftMils / 1000);
            int rightMargin = (int)(dpi * (float)newAttributes.MinMargins.RightMils / 1000);
            int topMargin = (int)(dpi * (float)newAttributes.MinMargins.TopMils / 1000);
            int bottomMargin = (int)(dpi * (float)newAttributes.MinMargins.BottomMils / 1000);

            int w = (int)(dpi * (float)newAttributes.GetMediaSize().WidthMils / 1000) - leftMargin - rightMargin;
            int h = (int)(dpi * (float)newAttributes.GetMediaSize().HeightMils / 1000) - topMargin - bottomMargin;
            scale = 0.3f;
            //scale = Math.Min((float)document.PageContentRect.Width() / w, (float)document.PageContentRect.Height() / h);
            Toast.MakeText(context,scale.ToString(),ToastLength.Long).Show();
        }

        public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination,CancellationSignal cancellationSignal, WriteResultCallback callback)
        {
            PrintedPdfDocument.Page page = document.StartPage(0);
            

            page.Canvas.Scale(scale, scale);

            view.Draw(page.Canvas);

            document.FinishPage(page);
            
            WritePrintedPdfDoc(destination);

            document.Close();

            document.Dispose();

            callback.OnWriteFinished(pages);
        }

        void WritePrintedPdfDoc(ParcelFileDescriptor destination)
        {
            var javaStream = new Java.IO.FileOutputStream(destination.FileDescriptor);
            var osi = new OutputStreamInvoker(javaStream);
            using (var mem = new MemoryStream())
            {
                document.WriteTo(mem);
                var bytes = mem.ToArray();
                osi.Write(bytes, 0, bytes.Length);
            }
        }
    }
}