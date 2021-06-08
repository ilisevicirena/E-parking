using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Aktivnost
    {
        private readonly int Id;
        private readonly DateTime Vrijeme;
        private readonly Osoba Korisnik;
        private readonly Lokacija Lokacija;
        private readonly Vozilo Vozilo;
        private readonly int BrojKm;
        private readonly VoziloInstanca Instanca;
        private readonly string OpisProblema;

        private Aktivnost(AktivnostBuilder builder)
        {
            this.Id = builder.id;
            this.Vrijeme = builder.datum;
            this.Vozilo = builder.vozilo;
            this.Lokacija = builder.lokacija;
            this.Korisnik = builder.osoba;
            this.BrojKm = builder.brojKm;
            this.Instanca = builder.instanca;
            this.OpisProblema = builder.opisProblema;
        }

        public static List<Aktivnost> SveAktivnosti = new List<Aktivnost>();
        public static List<Aktivnost> NajamAktivnosti = new List<Aktivnost>();

        public Vozilo GetVozilo() { return Vozilo; }
        public Lokacija GetLokacija() { return Lokacija; }
        public Osoba GetOsoba() { return Korisnik; }
        public DateTime GetDatum() { return Vrijeme; }
        public int GetId() { return Id; }
        public int GetBrojKm() { return BrojKm; }
        public string GetOpisProblema() { return OpisProblema; }

        public static Aktivnost VratiPosljednjuAktivnost()
        {
            if (SveAktivnosti.Count > 0)
            {
                return SveAktivnosti.Last();
            }
            return null;
        }
        public static bool ProvjeriKorisnikImaNajam(Osoba osoba, Vozilo vozilo)
        {
            foreach (var item in NajamAktivnosti)
            {
                if (item.Korisnik == osoba && item.Vozilo == vozilo)
                {
                    return true;
                }
            }
            return false;
        }
        public static void IzbrisiNajam(Osoba osoba, Vozilo vozilo)
        {
            Aktivnost zaBrisanje = null;
            foreach (var item in NajamAktivnosti)
            {
                if (item.Korisnik == osoba && item.Vozilo == vozilo)
                {
                    zaBrisanje = item;
                }
            }
            NajamAktivnosti.Remove(zaBrisanje);
        }
        public static DateTime VrijemePosudbe(Osoba osoba, Vozilo vozilo)
        {
            foreach (var item in NajamAktivnosti)
            {
                if (item.Korisnik == osoba && item.Vozilo == vozilo)
                {
                    return item.Vrijeme;
                }
            }
            return DateTime.Now;
        }
        public static VoziloInstanca DohvatiPosudjenuInstancu(Osoba osoba, Vozilo vozilo)
        {
            foreach (var item in NajamAktivnosti)
            {
                if (item.Korisnik == osoba && item.Vozilo == vozilo)
                {
                    return item.Instanca;
                }
            }
            return null;
        }
        public static int BrojNajmaZaVrstuVozila(Vozilo vozilo, Lokacija lokacija, DateTime pocetak, DateTime kraj)
        {
            int ukupno = 0;
            foreach (var item in SveAktivnosti)
            {
                if (item.Vrijeme >= pocetak && item.Vrijeme <= kraj && item.Id == 2 && item.Lokacija == lokacija && item.Vozilo == vozilo)
                {
                    ukupno += 1;
                }
            }
            return ukupno;
        }

        public class AktivnostBuilder
        {
            public int id;
            public DateTime datum;
            public Vozilo vozilo;
            public Lokacija lokacija;
            public Osoba osoba;
            public int brojKm;
            public VoziloInstanca instanca;
            public string opisProblema;

            public AktivnostBuilder SetInstanca(VoziloInstanca instanca)
            {
                this.instanca = instanca;
                return this;
            }
            public AktivnostBuilder(int id, DateTime datum)
            {
                this.id = id;
                this.datum = datum;
            }
            public AktivnostBuilder SetVozilo(Vozilo vozilo)
            {
                this.vozilo = vozilo;
                return this;
            }
            public AktivnostBuilder SetLokacija(Lokacija lokacija)
            {
                this.lokacija = lokacija;
                return this;
            }
            public AktivnostBuilder SetOsoba(Osoba osoba)
            {
                this.osoba = osoba;
                return this;
            }
            public AktivnostBuilder SetBrojKm(int brojKm)
            {
                this.brojKm = brojKm;
                return this;
            }
            public AktivnostBuilder SetOpisProblema(string opis)
            {
                this.opisProblema = opis;
                return this;
            }
            public Aktivnost Build()
            {
                return new Aktivnost(this);
            }
        }
    }
}
