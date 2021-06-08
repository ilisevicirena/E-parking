using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Osoba:model.IHandler
    {
        private model.IHandler next = null;
        public model.IHandler getNext()
        {
            return next;
        }
        private int Id;
        private string ImePrezime;
        private int BrojNeispravnihVozila;
        private bool Ugovor;
        private double Dugovanje;
        private DateTime ZadnjiNajam;

        public static List<Osoba> SveOsobe = new List<Osoba>();

        public Osoba(int id, string imePrezime, bool ugovor)
        {
            this.Id = id;
            this.ImePrezime = imePrezime;
            this.Ugovor = ugovor;

            SveOsobe.Add(this);
        }

        public void SetId(int id)
        {
            Id = id;
        }
        public void SetImePrezime(string ime)
        {
            ImePrezime = ime;
        }
        public void SetBrojNeispravnihVozila(int broj)
        {
            BrojNeispravnihVozila = broj;
        }
        public void SetUgovor(bool ugovor)
        {
            Ugovor = ugovor;
        }
        public void SetDugovanje(double dugovanje)
        {
            Dugovanje = dugovanje;
        }
        public void SetZadnjiNajam(DateTime datum)
        {
            ZadnjiNajam = datum;
        }

        public int GetId() { return Id; }
        public string GetImePrezime() { return ImePrezime; }
        public int GetBrojNeispranihVozila() { return BrojNeispravnihVozila; }
        public bool GetUgovor() { return Ugovor; }
        public double GetDugovanje() { return Dugovanje; }
        public DateTime GetZadnjiNajam() { return ZadnjiNajam; }

        public static Osoba DohvatiOsobu(int id)
        {
            foreach (var item in SveOsobe)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public static void PovećajDugovanje(Osoba osoba, double iznos)
        {
            double trenutnoDugovanje = osoba.GetDugovanje();
            double novoDugovanje = trenutnoDugovanje + iznos;
            osoba.SetDugovanje(novoDugovanje);
        }
        public static void SmanjiDugovanje(Osoba osoba, double iznos)
        {
            double trenutnoDugovanje = osoba.GetDugovanje();
            double novoDugovanje = trenutnoDugovanje - iznos;
            osoba.SetDugovanje(novoDugovanje);
        }

        public void handleRequest(object request)
        {
            if ((request as string) == "pretrazi")
            {

            }
            else
            {
                if (next != null)
                {
                    next.handleRequest(request);
                }
            }
        }
    }
}
