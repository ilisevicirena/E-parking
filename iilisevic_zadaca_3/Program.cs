using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    class Program
    {
        static int Main(string[] args)
        {
            controller.Controller controller = new controller.Controller();
            return controller.PokreniMVC(args);
        }
    }      
}
