using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestStackOverflow1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Test();
            getLocalVar();
        }

        public static void Test()
        {
            string bar = "";
            int foo = 24;
        }

        public static void getLocalVar()
        {
             var flags = BindingFlags.Static | BindingFlags.Public;
             var fields = typeof(MainWindow).GetMethod("Test").GetMethodBody();

             //foreach (var scope in fields.RootScope.GetChildren())
             //{
             //    foreach (var loc in scope.GetLocals())
             //    {
             //        Console.WriteLine("{0}: {1} {2}", loc.AddressField1, locals.FirstOrDefault(l => l.LocalIndex == loc.AddressField1).LocalType, loc.Name);
             //    }
             //}
        }

         public void LocalVariableNameReader (MethodInfo m)
        {
            ISymbolReader symReader = SymUtil.GetSymbolReaderForFile (m.DeclaringType.Assembly.Location, null);
            ISymbolMethod met = symReader.GetMethod (new SymbolToken (m.MetadataToken));
            VisitLocals (met.RootScope);
        }

        void VisitLocals (ISymbolScope iSymbolScope)
        {
            foreach (var s in iSymbolScope.GetLocals ()) _names [s.AddressField1] = s.Name;
            foreach (var c in iSymbolScope.GetChildren ()) VisitLocals (c);
        }
       
    }
}
