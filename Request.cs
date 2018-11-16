using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Http.Server
{
    class Request 
    {
        private string strRequest = null;

        private bool nullRequest = true;


        private Request(string data)
        {
            Console.WriteLine("data has gotten: " + strRequest);
            strRequest = data;

            nullRequest = false;
        }

        public static Request Get(string uri)
        {
         

                Console.WriteLine("i have new request object! from uri: " + uri);
            string strData = uri;
         
                     if (strData != null)
                        return new Request(strData);

                   return GetNullRequest();
                
        }

        public string GetRequestString()
        {
            return strRequest;
        }

        public static Request GetNullRequest()
        {
            return new Request(null);
        }


    }
}
