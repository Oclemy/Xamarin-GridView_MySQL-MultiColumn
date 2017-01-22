using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GridView_MySQL_MultiColumn.m_Code.m_Model;
using Object = Java.Lang.Object;

namespace GridView_MySQL_MultiColumn.m_Code.m_UI
{
    class CustomAdapter : BaseAdapter
    {
        private Context c;
        private JavaList<Spacecraft> spacecrafts;

        public CustomAdapter(Context c, JavaList<Spacecraft> spacecrafts)
        {
            this.c = c;
            this.spacecrafts = spacecrafts;
        }

        public override Object GetItem(int position)
        {
            return spacecrafts.Get(position);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView==null)
            {
                convertView = LayoutInflater.From(c).Inflate(Resource.Layout.Model, parent, false);
            }

            TextView nameTxt = convertView.FindViewById<TextView>(Resource.Id.nameTxt);
            TextView propTxt = convertView.FindViewById<TextView>(Resource.Id.propellantTxt);
            TextView descTxt = convertView.FindViewById<TextView>(Resource.Id.descTxt);

            Spacecraft s = spacecrafts[position];

            nameTxt.Text = s.Name;
            propTxt.Text = s.Propellant;
            descTxt.Text = s.Description;

            return convertView;
        }

        public override int Count
        {
            get { return spacecrafts.Size(); }
        }
    }
}