﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] prefixes = { "http://*:8081/index/" };
            SimpleListenerExample(prefixes);
        }

        // This example requires the System and System.Net namespaces. 
        public static void SimpleListenerExample(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required, 
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            while (true) { 
                // Create a listener.
                HttpListener listener = new HttpListener();
                // Add the prefixes. 
                foreach (string s in prefixes)
                {
                    listener.Prefixes.Add(s);
                }
                listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;

                listener.Start();
                Console.WriteLine("Listening...");
                
                // Note: The GetContext method blocks while waiting for a request. 
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                
                //Close the input stream but send a response ! :)
                request.InputStream.Close();
                Console.WriteLine("InputStream closed! Waiting...");
                Thread.Sleep(2000);
                Console.WriteLine("Send response...");
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                response.StatusCode = 400;
                // Construct a response. 
                string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
                listener.Stop();
                Console.WriteLine("Closed... Do it again !");
            }
        }
    }
}
