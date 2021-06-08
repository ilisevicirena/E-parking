using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class NaPunjenju : IStanje
    {
        public bool GetStanje(VoziloInstanca instanca)
        {
            return instanca.GetNaPunjenju();
        }
        public void SetStanje(VoziloInstanca instanca, bool stanje)
        {
            instanca.SetNaPunjenju(stanje);
        }
        public static int ProvjeriStanjeBaterije(VoziloInstanca vozilo, DateTime datum)
        {
            if (vozilo.GetStanjeBaterije() < 100)
            {
                int trenutnoStanjeBaterije = vozilo.GetStanjeBaterije();
                int preostaloZaNapuniti = 100 - trenutnoStanjeBaterije;
                var vrijemePunjenja = ((double)preostaloZaNapuniti / 100) * (double)vozilo.VrijemePunjenja;
                var protekloVrijeme = (datum - (DateTime)vozilo.GetZadnjeVracanje()).TotalHours;
                vozilo.SetNaPunjenju(true);
                if ((double)protekloVrijeme >= (double)vrijemePunjenja)
                {
                    vozilo.SetStanjeBaterije(100);
                    vozilo.SetNaPunjenju(false);
                }
            }
            return vozilo.GetStanjeBaterije();
        }
        public static void IzracunajStanjeBaterije(int brojPređenihKm, VoziloInstanca vozilo)
        {
            double rez = (double)brojPređenihKm / (double)vozilo.Domet;
            int novoStanjeBaterije = Convert.ToInt32(rez * 100);
            vozilo.SetStanjeBaterije(novoStanjeBaterije);
            if (novoStanjeBaterije < 100)
            {
                vozilo.SetNaPunjenju(true);
            }
        }
    }
}
