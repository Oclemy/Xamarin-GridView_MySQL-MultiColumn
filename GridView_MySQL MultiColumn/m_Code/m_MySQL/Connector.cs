using System;
using System.Collections.Generic;
using System.Linq;

using Java.IO;
using Java.Net;
using Console = System.Console;

namespace GridView_MySQL_MultiColumn.m_Code.m_MySQL
{
    class Connector
    {
        public static Java.Lang.Object connect(string urlAddress)
        {
            try
            {
                URL url = new URL(urlAddress);
                HttpURLConnection con = (HttpURLConnection) url.OpenConnection();

                //CON PROPS
                con.RequestMethod = "GET";
                con.ConnectTimeout = 15000;
                con.ReadTimeout = 15000;
                con.DoInput = true;

                return con;

            }
            catch (MalformedURLException e)
            {

                Console.WriteLine("Error " + e.Message);
                return "Error " + e.Message;
            }
            catch (IOException e)
            {

                Console.WriteLine("Error " + e.Message);
                return "Error " + e.Message;
            }
        }
    }
} 