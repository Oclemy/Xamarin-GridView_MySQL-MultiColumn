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

namespace GridView_MySQL_MultiColumn.m_Code.m_Model
{
    class Spacecraft
    {
        private string name, propellant, description;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Propellant
        {
            get { return propellant; }
            set { propellant = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}