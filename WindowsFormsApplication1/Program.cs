using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace WindowsFormsApplication1
{
    class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Program program = new Program();
            //program.function();

            sendAndReceive();
            return;


            //var request = System.Net.HttpWebRequest.CreateHttp("http://localhost:8081/index/");
            var request = System.Net.HttpWebRequest.Create("http://localhost:8081/index/");
            request.Method = "POST";
            string filename = "foo\u00A0bar.dat"; // Invalid characters in filename, the server will refuse it
            request.Headers["Content-Disposition"] = string.Format("attachment; filename*=utf-8''{0}", Uri.EscapeDataString(filename));
            request.ContentType = "application/octet-stream";
            request.ContentLength = 100 * 1024 * 1024;

            // Upload the "file" (just random data in this case)
            try
            {
                using (var stream = request.GetRequestStream())
                {
                    byte[] buffer = new byte[1024 * 1024];
                    new Random().NextBytes(buffer);
                    for (int i = 0; i < 100; i++)
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                // here I get an IOException; InnerException is a SocketException
                Console.WriteLine("Error writing to stream: {0}", ex);
            }

            // Now try to read the response
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    Console.WriteLine("{0} - {1}", (int)response.StatusCode, response.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                // here I get a WebException; InnerException is the IOException from the previous catch
                Console.WriteLine("Error getting the response: {0}", ex);
                var webEx = ex as WebException;
                if (webEx != null)
                {
                    Console.WriteLine(webEx.Status); // SendFailure
                    var response = (HttpWebResponse)webEx.Response;
                    if (response != null)
                    {
                        Console.WriteLine("{0} - {1}", (int)response.StatusCode, response.StatusDescription);
                    }
                    else
                    {
                        Console.WriteLine("No response");
                    }
                }
            }
        }

        static WebRequest request;
        private static void sendAndReceive()
        {
            // The request with a big timeout for receiving large amout of data
            request = HttpWebRequest.Create("http://localhost:8081/index/");
            request.Timeout = 10;

            request.Method = "POST";
            string filename = "foo\u00A0bar.dat"; // Invalid characters in filename, the server will refuse it
            request.Headers["Content-Disposition"] = string.Format("attachment; filename*=utf-8''{0}", Uri.EscapeDataString(filename));
            request.ContentType = "application/octet-stream";
            request.ContentLength = 100 * 1024 * 1024;



            // The connection timeout
            var ConnectionTimeoutTime = 10000;
            Timer timer = new Timer(ConnectionTimeoutTime);
            timer.Elapsed += connectionTimeout;
            timer.Enabled = true;

            Console.WriteLine("Connecting...");
            try
            {
                using (var stream = request.GetRequestStream())
                {
                    Console.WriteLine("Connection success !");
                    timer.Enabled = false;

                    /*
                     *  Sending data ...
                     */
                    System.Threading.Thread.Sleep(1000000);
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    /*
                     *  Receiving datas...
                     */
                }
            }
            catch (WebException e)
            {
                if(e.Status==WebExceptionStatus.RequestCanceled) 
                    Console.WriteLine("Connection canceled (timeout)");
                else if(e.Status==WebExceptionStatus.ConnectFailure)
                    Console.WriteLine("Can't connect to server");
                else if(e.Status==WebExceptionStatus.Timeout)
                    Console.WriteLine("Timeout");
                else
                    Console.WriteLine("Error");
            }
        }

        static void connectionTimeout(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("Connection failed...");
            Timer timer = (Timer)sender;
            timer.Enabled = false;

            request.Abort();
        }
    }
}
