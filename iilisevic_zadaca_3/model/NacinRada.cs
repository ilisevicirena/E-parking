using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class NacinRada
    {
        public bool SkupniNacinRada;
        public bool InteraktivniNacinRada;
        private static NacinRada instance = null;
        public static NacinRada Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NacinRada { SkupniNacinRada = false, InteraktivniNacinRada = true };
                }
                return instance;
            }
        }
    }
}
