using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Timers;

namespace TestConsoleMode
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer(1000);

            timer.Tick += new EventHandler(delegate(object o, EventArgs ea)
            {
                Console.WriteLine("Tick");
            });
            
            while (timer.Enabled)
            {
                Console.WriteLine("Run...");
            }

        }
    }
}
