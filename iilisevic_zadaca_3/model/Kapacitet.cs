using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Kapacitet
    {
        private Lokacija Lokacija;
        private Vozilo Vozilo;
        private int BrojMjestaZaVozilo;
        private int RaspoloziviBrojVrstaVozila;

        public static List<Kapacitet> SveLokacijeKapaciteti = new List<Kapacitet>();

        public Kapacitet(Lokacija lokacija, Vozilo vozilo, int brojMjesta, int raspoloziviBrojVozila)
        {
            this.Lokacija = lokacija;
            this.Vozilo = vozilo;
            this.BrojMjestaZaVozilo = brojMjesta;
            this.RaspoloziviBrojVrstaVozila = raspoloziviBrojVozila;

            SveLokacijeKapaciteti.Add(this);
        }

        public void SetLokacija(Lokacija lokacija)
        {
            Lokacija = lokacija;
        }
        public void SetVozilo(Vozilo vozilo)
        {
            Vozilo = vozilo;
        }
        public void SetBrojMjestaZaVozilo(int brojMjesta)
        {
            BrojMjestaZaVozilo = brojMjesta;
        }
        public void SetRaspoloziviBrojVrstaVozila(int brojVrsta)
        {
            RaspoloziviBrojVrstaVozila = brojVrsta;
        }

        public Lokacija GetLokacija() { return Lokacija; }
        public Vozilo GetVozilo() { return Vozilo; }
        public int GetBrojMjestaZaVozilo() { return BrojMjestaZaVozilo; }
        public int GetRaspoloziviBrojVrstaVozila() { return RaspoloziviBrojVrstaVozila; }

        public static int BrojRaspolozivihVrstaVozila(Lokacija lokacija, Vozilo vozilo, DateTime datum)
        {
            int brojVozila = 0;
            Posuđeno posuđeno = new Posuđeno();
            foreach (var item in VoziloInstanca.SveInstanceVozila)
            {
                if (item.GetLokacija() == lokacija && item.ID == vozilo.GetId() && posuđeno.GetStanje(item) == false)
                {
                    if (NaPunjenju.ProvjeriStanjeBaterije(item, datum) == 100)
                    {
                        brojVozila++;
                    }
                }
            }
            return brojVozila;
        }
        public static int BrojRaspolozivihMjesta(Lokacija lokacija, Vozilo vozilo)
        {
            foreach (var item in SveLokacijeKapaciteti)
            {
                if (item.Lokacija == lokacija && item.Vozilo == vozilo)
                {
                    return item.BrojMjestaZaVozilo - item.RaspoloziviBrojVrstaVozila;
                }
            }
            return 0;
        }
        public static void UnajmiVozilo(Lokacija lokacija, Vozilo vozilo)
        {
            foreach (var item in SveLokacijeKapaciteti)
            {
                if (item.Lokacija == lokacija && item.Vozilo == vozilo)
                {
                    item.RaspoloziviBrojVrstaVozila -= 1;
                }
            }
        }
        public static void VratiVozilo(Lokacija lokacija, Vozilo vozilo)
        {
            foreach (var item in SveLokacijeKapaciteti)
            {
                if (item.Lokacija == lokacija && item.Vozilo == vozilo)
                {
                    item.RaspoloziviBrojVrstaVozila += 1;
                }
            }
        }
        public static List<Vozilo> DohvatiVrsteVozilaPoLokaciji(Lokacija lokacija)
        {
            List<Vozilo> vozila = new List<Vozilo>();
            foreach (var item in SveLokacijeKapaciteti)
            {
                if (item.Lokacija == lokacija)
                {
                    vozila.Add(item.Vozilo);
                }
            }
            return vozila;
        }
    }
}
