using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3.view
{
    public class ViewSkupniDatoteka : View
    {
        public ViewSkupniDatoteka()
        {
            Console.WriteLine("Aktivan pogled: Skupni način rada ispis u datoteku!");
        }
        public override void Ispis(List<string> s)
        {
            foreach (var item in s)
            {
                Console.WriteLine(item);
            }
        }
        public override void IspisGreskeIzDatoteka(List<string> s)
        {
            foreach (var item in s)
            {
                Console.WriteLine(item);
                foreach (var slovo in item)
                {
                    Console.Write("X");
                }
                Console.WriteLine();
            }
        }
        public override void TraziUnos()
        {
           
        }
    }
}
