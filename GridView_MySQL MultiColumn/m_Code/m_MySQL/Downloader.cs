using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Lang;
using Java.Net;
using Console = System.Console;
using Exception = System.Exception;
using Object = Java.Lang.Object;

namespace GridView_MySQL_MultiColumn.m_Code.m_MySQL
{
    internal class Downloader : AsyncTask
    {
        private Context c;
        private string urlAddress;
        private GridView gv;

        private ProgressDialog pd;

        public Downloader(Context c, string urlAddress, GridView gv)
        {
            this.c = c;
            this.urlAddress = urlAddress;
            this.gv = gv;
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            pd = new ProgressDialog(c);
            pd.SetTitle("Fetch Data");
            pd.SetMessage("Fetching...Please wait");
            pd.Show();
        }

        protected override Object DoInBackground(params Object[] @params)
        {
            return DownloadData();
        }

        protected override void OnPostExecute(Object result)
        {
            base.OnPostExecute(result);
            pd.Dismiss();

            if (result.ToString().StartsWith("Error"))
            {
                Toast.MakeText(c,result.ToString(),ToastLength.Short).Show();
            }
            else
            {
                ///PARSE
                new DataParser(c, result.ToString(), gv).Execute();
            }
        }

        private Object DownloadData()
        {
            Object connection = Connector.connect(urlAddress);
            if (connection.ToString().StartsWith("Error"))
            {
                return connection.ToString();
            }

            try
            {
                HttpURLConnection con = (HttpURLConnection) connection;
                if (con.ResponseCode == HttpStatus.Ok)
                {
                    Stream s = new BufferedStream(con.InputStream);
                    BufferedReader br = new BufferedReader(new InputStreamReader(s));

                    string line;
                    StringBuffer jsonData = new StringBuffer();

                    while ((line = br.ReadLine()) != null)
                    {
                        jsonData.Append(line + "\n");
                    }

                    return jsonData;
                }
                else
                {
                    return "Error " + con.ResponseCode.ToString() + " : " + con.ResponseMessage;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return "Error " + e.Message;
            }

        }
    }

}