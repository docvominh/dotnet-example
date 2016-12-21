using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace http_request
{
    class Program
    {
        static void Main(string[] args)
        {
            // Html return
            string url = "https://www.google.com";

            // Json return
            string url2 = "http://www.w3schools.com/angular/customers.php";

            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create((url2));
            request.Method = "GET";
            request.ContentLength = 0;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    result = reader.ReadToEnd();
                }
            }

            // Write out all html content
            Console.WriteLine(result);
            Console.Read();
        }
    }
}
