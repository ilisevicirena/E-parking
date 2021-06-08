using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Neispravno : IStanje
    {
        public bool GetStanje(VoziloInstanca instanca)
        {
           return instanca.GetNeispravno();
        }
        public void SetStanje(VoziloInstanca instanca, bool stanje)
        {
            instanca.SetNeispravno(stanje);
        }
        public static int BrojNeispravnihVozilaPoVrsti(Vozilo vozilo, Lokacija lokacija)
        {
            int ukupno = 0;
            foreach (var item in VoziloInstanca.SveInstanceVozila)
            {
                if (item.ID == vozilo.GetId() && item.GetNeispravno() == true && item.GetLokacija() == lokacija)
                {
                    ukupno += 1;
                }
            }
            return ukupno;
        }
    }
}
