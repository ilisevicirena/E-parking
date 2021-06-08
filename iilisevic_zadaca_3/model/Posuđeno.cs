using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Posuđeno : IStanje
    {
        public bool GetStanje(VoziloInstanca instanca)
        {
            return instanca.GetPosudjeno();
        }
        public void SetStanje(VoziloInstanca instanca, bool stanje)
        {
            instanca.SetPosudjeno(stanje);
        }
        public static VoziloInstanca DohvatiVoziloZaPosudbu(Vozilo vozilo, Lokacija lokacija)
        {
            VoziloInstanca vrati = null;
            List<VoziloInstanca> vozilaSNajmanjimBrojemNajma = new List<VoziloInstanca>();
            List<VoziloInstanca> dostupnaVozila = new List<VoziloInstanca>();
            Neispravno neispravno = new Neispravno();
            foreach (var item in VoziloInstanca.SveInstanceVozila)
            {
                if (item.ID == vozilo.GetId() && item.GetStanjeBaterije() == 100 && item.GetPosudjeno() == false && item.GetLokacija() == lokacija && neispravno.GetStanje(item) == false)
                {
                    dostupnaVozila.Add(item);
                }
            }
            var min = dostupnaVozila.OrderBy(x => x.GetBrojNajma()).First();
            foreach (var item in dostupnaVozila)
            {
                if (item.GetBrojNajma() == min.GetBrojNajma())
                {
                    vozilaSNajmanjimBrojemNajma.Add(item);
                }
            }
            if (vozilaSNajmanjimBrojemNajma.Count() == 1)
            {
                vrati = vozilaSNajmanjimBrojemNajma.First();
            }
            else
            {
                List<VoziloInstanca> vozilaSNajmanjimBrojemKm = new List<VoziloInstanca>();
                var minKm = vozilaSNajmanjimBrojemNajma.OrderBy(x => x.GetBrojKm()).First();
                foreach (var item in vozilaSNajmanjimBrojemNajma)
                {
                    if (item.GetBrojKm() == minKm.GetBrojKm())
                    {
                        vozilaSNajmanjimBrojemKm.Add(item);
                    }
                }
                if (vozilaSNajmanjimBrojemKm.Count() == 1)
                {
                    vrati = vozilaSNajmanjimBrojemKm.First();
                }
                else
                {
                    vrati = vozilaSNajmanjimBrojemKm.OrderBy(x => x.GetRegistracija()).First();
                }
            }
            return vrati;
        }
    }
}
