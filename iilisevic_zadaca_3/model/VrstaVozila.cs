using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public abstract class VrstaVozila : ICloneable
    {
        public int ID;
        public string Naziv;
        public int Domet;
        public int VrijemePunjenja;
        public abstract object Clone();
    }
}
