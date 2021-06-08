using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3.view
{
    public abstract class View
    {
        public abstract void Ispis(List<string> s);
        public abstract void TraziUnos();
        public abstract void IspisGreskeIzDatoteka(List<string> s);
    }
}
