using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using GridView_MySQL_MultiColumn.m_Code.m_MySQL;

namespace GridView_MySQL_MultiColumn
{
    [Activity(Label = "GridView MySQL MultiColumn", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private string urlAddress = "http://10.0.2.2/android/spacecraft_select_images.php";
        private GridView gv;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            gv = FindViewById<GridView>(Resource.Id.gv);
            Button downloadBtn = FindViewById<Button>(Resource.Id.downloadBtn);

            downloadBtn.Click += downloadBtn_Click;

        }

        void downloadBtn_Click(object sender, EventArgs e)
        {
            new Downloader(this, urlAddress, gv).Execute();
        }
    }
}

