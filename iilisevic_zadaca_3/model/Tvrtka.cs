using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Tvrtka
    {
        public int Id;
        public string Naziv;
        private static Tvrtka instance = null;
        public static Tvrtka Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Tvrtka();
                }
                return instance;
            }
        }
    }
}
