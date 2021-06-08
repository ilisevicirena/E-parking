using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Cjenik
    {
        private Vozilo Vozilo;
        private double Najam;
        private double PoSatu;
        private double PoKm;

        public static List<Cjenik> SveCjene = new List<Cjenik>();

        public Cjenik(Vozilo vozilo, double najam, double poSatu, double poKm)
        {
            this.Vozilo = vozilo;
            this.Najam = najam;
            this.PoSatu = poSatu;
            this.PoKm = poKm;

            SveCjene.Add(this);
        }

        public void SetVozilo(Vozilo vozilo)
        {
            Vozilo = vozilo;
        }
        public void SetNajam(double najam)
        {
            Najam = najam;
        }
        public void SetPoSatu(double poSatu)
        {
            PoSatu = poSatu;
        }
        public void SetPoKm(double poKm)
        {
            PoKm = poKm;
        }

        public Vozilo GetVozilo() { return Vozilo; }
        public double GetNajam() { return Najam; }
        public double GetPoSatu() { return PoSatu; }
        public double GetPoKm() { return PoKm; }

        public static double IzracunajNajam(Vozilo vozilo)
        {
            foreach (var item in SveCjene)
            {
                if (item.Vozilo == vozilo)
                {
                    return item.Najam;
                }
            }
            return 0;
        }
        public static double IzracunajCjenuPoKM(Vozilo vozilo, int brojPređenihKM)
        {
            foreach (var item in SveCjene)
            {
                if (item.Vozilo == vozilo)
                {
                    return (item.PoKm * brojPređenihKM);
                }
            }
            return 0;
        }
        public static double IzracunajCjenuPoSatu(Vozilo vozilo, double brojSati)
        {
            foreach (var item in SveCjene)
            {
                if (item.Vozilo == vozilo)
                {
                    return item.PoSatu * brojSati;
                }
            }
            return 0;
        }
    }
}
