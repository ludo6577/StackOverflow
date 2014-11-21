using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            statusStrip1.Padding = new Padding(statusStrip1.Padding.Left, statusStrip1.Padding.Top, statusStrip1.Padding.Left, statusStrip1.Padding.Bottom);


            WebBrowser webBrowser = new WebBrowser();

            //webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(delegate(object o, WebBrowserDocumentCompletedEventArgs ea)
            //{
            //    baa(some_value);
            //});

            string a = "abc";
            string b = "A𠈓C";
            Console.WriteLine("Length 1 = {0}", a.Length);
            Console.WriteLine("Length 2 = {0}", b.Length);

            //Timer timer = new Timer();
            //timer.Interval = 10;

            //timer.Tick += new EventHandler(delegate(object o, EventArgs ea)
            //{
            //    //timer.Stop();
            //    Console.WriteLine("Tick");
            //});
            //timer.Start();

            //while (timer.Enabled)
            //{
            //    Application.DoEvents();
            //    Console.WriteLine("Run...");
            //}
            //Console.WriteLine("End...");
        }
    }
}
