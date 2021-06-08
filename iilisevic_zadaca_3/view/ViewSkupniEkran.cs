using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3.view
{
    public class ViewSkupniEkran : View
    {
        public ViewSkupniEkran()
        {
            Console.WriteLine("Aktivan pogled: Skupni način rada ispis na ekran!");
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
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var item in s)
            {
                Console.WriteLine(item);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override void TraziUnos()
        {
            
        }
    }
}
