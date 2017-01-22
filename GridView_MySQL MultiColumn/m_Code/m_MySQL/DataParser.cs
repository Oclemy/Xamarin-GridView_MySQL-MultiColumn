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
using GridView_MySQL_MultiColumn.m_Code.m_Model;
using GridView_MySQL_MultiColumn.m_Code.m_UI;
using Org.Json;
using Object = Java.Lang.Object;

namespace GridView_MySQL_MultiColumn.m_Code.m_MySQL
{
    class DataParser : AsyncTask
    {
        private Context c;
        private string jsonData;
        private GridView gv;

        private ProgressDialog pd;
        private JavaList<Spacecraft> spacecrafts=new JavaList<Spacecraft>(); 

        public DataParser(Context c, string jsonData, GridView gv)
        {
            this.c = c;
            this.jsonData = jsonData;
            this.gv = gv;
        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            pd = new ProgressDialog(c);
            pd.SetTitle("Parse Data");
            pd.SetMessage("Parsing...Please wait");
            pd.Show();
        }

        protected override Object DoInBackground(params Object[] @params)
        {
            return ParseData();
        }

        protected override void OnPostExecute(Object result)
        {
            base.OnPostExecute(result);

            pd.Dismiss();

            if ((bool) result)
            {
                //BIND
                gv.Adapter=new CustomAdapter(c,spacecrafts);
                gv.ItemClick += gv_ItemClick;
            }
            else
            {
                Toast.MakeText(c,"Unable To Parse",ToastLength.Short).Show();
            }
        }

        void gv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(c, spacecrafts[e.Position].Name, ToastLength.Short).Show();

        }

        private Boolean ParseData()
        {
            try
            {
                JSONArray ja = new JSONArray(jsonData);
                JSONObject jo;

                spacecrafts.Clear();
                Spacecraft spacecraft;

                for (int i = 0; i < ja.Length(); i++)
                {
                    jo = ja.GetJSONObject(i);

                    string name = jo.GetString("name");
                    string propellant = jo.GetString("propellant");
                    string desc = jo.GetString("description");

                    spacecraft = new Spacecraft();

                    spacecraft.Name = name;
                    spacecraft.Propellant = propellant;
                    spacecraft.Description = desc;

                    spacecrafts.Add(spacecraft);

                }

                return true;

            }
            catch (JSONException e)
            {
                System.Console.Write(e.Message);
                return false;

            }
        }
    }
    }
