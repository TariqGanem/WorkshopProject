using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Client.Code
{
    public class system
    {
        private const string server_domain = "https://127.0.0.1:5000/api";

        public static string SendApi(string method_name, string Parameters)
        {
            string service = "";
            service = "facade";

            string URI = string.Format("{0}/{1}/{2}?{3}", server_domain, service, method_name, Parameters);
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URI);
                request.Method = "GET";
                String test = String.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    test = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }
                return test;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}