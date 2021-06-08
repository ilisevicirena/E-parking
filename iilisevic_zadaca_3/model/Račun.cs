using iilisevic_zadaca_3.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Račun: model.IHandler
    {
        private IHandler next = null;
        public IHandler getNext()
        {
            return next;
        }
        private int Id;
        private double Najam;
        private double PoSatu;
        private double PoKM;
        private double Iznos;
        private int LokacijaNajmaID;
        private int LokacijaVracanjaID;
        private Vozilo Vozilo;
        private VoziloInstanca Instanca;
        private Osoba Korisnik;
        private DateTime DatumIzdavanja;
        private double BrojSatiUPosudbi;
        private bool Plaćen;

        public static List<Račun> SviRacuni = new List<Račun>();
        public Račun()
        {
            SviRacuni.Add(this);
        }

        public void SetId(int id)
        {
            Id = id;
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
            PoKM = poKm;
        }
        public void SetIznos(double iznos)
        {
            Iznos = iznos;
        }
        public void SetLokacijaNajma(int id)
        {
            LokacijaNajmaID = id;
        }
        public void SetLokacijaVracanja(int id)
        {
            LokacijaVracanjaID = id;
        }
        public void SetVozilo(Vozilo vozilo)
        {
            Vozilo = vozilo;
        }
        public void SetVoziloInstanca(VoziloInstanca voziloInstanca)
        {
            Instanca = voziloInstanca;
        }
        public void SetOsoba(Osoba osoba)
        {
            Korisnik = osoba;
        }
        public void SetDatumIzdavanja(DateTime datum)
        {
            DatumIzdavanja = datum;
        }
        public void SetPlaćen(bool plaćen)
        {
            Plaćen = plaćen;
        }
        public void SetBrojSatiUPosudbi(double broj)
        {
            BrojSatiUPosudbi = broj;
        }

        public int GetId() { return Id; }
        public double GetNajam() { return Najam; }
        public double GetPoSatu() { return PoSatu; }
        public double GetPoKm() { return PoKM; }
        public double GetIznos() { return Iznos; }
        public int GetLokacijaNajmaId() { return LokacijaNajmaID; }
        public int GetLokacijaVracanjaId() { return LokacijaVracanjaID; }
        public Vozilo GetVozilo() { return Vozilo; }
        public VoziloInstanca GetInstanca() { return Instanca; }
        public Osoba GetOsoba() { return Korisnik; }
        public DateTime GetDatumIzdavanja() { return DatumIzdavanja; }
        public bool GetPlaćen() { return Plaćen; }

        public static void IzracunajRacun(Vozilo vozilo, double brojSatiUPosudbi, int brojPredenihKM, VoziloInstanca instanca, Osoba korisnik, int lokacijaNajmaId, int lokacijaVracanjaId, DateTime datum)
        {
            var najam = Cjenik.IzracunajNajam(vozilo);
            var poSatu = Cjenik.IzracunajCjenuPoSatu(vozilo, brojSatiUPosudbi);
            var poKm = Cjenik.IzracunajCjenuPoKM(vozilo, brojPredenihKM);
            double iznos = najam + poSatu + poKm;
            var id = 1;
            if (SviRacuni.Count > 0)
            {
                id = SviRacuni.OrderBy(x => x.Id).Last().Id + 1;
            }
            Račun noviRacun = new Račun { Id = id, Instanca = instanca, Iznos = iznos, LokacijaNajmaID = lokacijaNajmaId, LokacijaVracanjaID = lokacijaVracanjaId, Najam = najam, Korisnik = korisnik, PoKM = poKm, PoSatu = poSatu, Vozilo = vozilo, DatumIzdavanja = datum, BrojSatiUPosudbi = brojSatiUPosudbi };
            if (korisnik.GetUgovor()==true)
            {
               Osoba.PovećajDugovanje(korisnik, iznos);
               noviRacun.Plaćen = false;
            }
            else
            {
                noviRacun.Plaćen = true;
            }
            IspisiRacun(noviRacun);
        }
        public static void IspisiRacun(Račun račun)
        {
            model.Model.podaci.Add("Račun: " + račun.Id);
            model.Model.podaci.Add("Datum i vrijeme izdavanja: " + račun.DatumIzdavanja);
            model.Model.podaci.Add("............");
            model.Model.podaci.Add("Najam: " + račun.Najam + " kn");
            model.Model.podaci.Add("Najam po satu: " + račun.PoSatu + " kn");
            model.Model.podaci.Add("Najam po km: " + račun.PoKM + " kn");
            model.Model.podaci.Add("Ukupno: " + račun.Iznos + " kn");
            model.Model.podaci.Add(".............");
        }
        public static double IzracunajZaraduZaLokaciju(Lokacija lokacija, DateTime pocetak, DateTime kraj, Vozilo vozilo)
        {
            double zarada = 0;
            foreach (var item in SviRacuni)
            {
                if (item.LokacijaNajmaID == lokacija.GetId() && item.DatumIzdavanja > pocetak && item.DatumIzdavanja < kraj && item.Vozilo == vozilo)
                {
                    zarada += item.Iznos;
                }
            }
            return zarada;
        }
        public static double TrajanjeNajmaPoLokaciji(Lokacija lokacija, DateTime pocetak, DateTime kraj, Vozilo vozilo)
        {
            double trajanje = 0;
            foreach (var item in SviRacuni)
            {
                if (item.LokacijaNajmaID == lokacija.GetId() && item.DatumIzdavanja > pocetak && item.DatumIzdavanja < kraj && item.Vozilo == vozilo)
                {
                    trajanje += item.BrojSatiUPosudbi;
                }
            }
            return trajanje;
        }
        public static List<Račun> DohvatiRacunePoLokacijiIVrstiVozila(Vozilo vozilo, Lokacija lokacija, DateTime pocetak, DateTime kraj)
        {
            List<Račun> racuni = new List<Račun>();
            foreach (var item in SviRacuni)
            {
                if (item.Vozilo == vozilo && item.LokacijaNajmaID == lokacija.GetId() && item.DatumIzdavanja > pocetak && item.DatumIzdavanja < kraj)
                {
                    racuni.Add(item);
                }
            }
            return racuni;
        }
        public static List<Račun> DohvatiNeplaćeneRačuneKorisnika(Osoba osoba, DateTime pocetak, DateTime kraj)
        {
            List<Račun> neplaceniRacuni = new List<Račun>();
            foreach (var item in SviRacuni)
            {
                if (item.Korisnik==osoba && item.DatumIzdavanja>pocetak && item.DatumIzdavanja<kraj && item.Plaćen==false)
                {
                    neplaceniRacuni.Add(item);
                }
            }
            return neplaceniRacuni;
        }
        public static List<Račun> DohvatiPlaćeneRačuneKorisnika(Osoba osoba, DateTime pocetak, DateTime kraj)
        {
            List<Račun> placeniRacuni = new List<Račun>();
            foreach (var item in SviRacuni)
            {
                if (item.Korisnik == osoba && item.DatumIzdavanja > pocetak && item.DatumIzdavanja < kraj && item.Plaćen == true)
                {
                    placeniRacuni.Add(item);
                }
            }
            return placeniRacuni;
        }
        public static void PodmiriRačune(Osoba korisnik, double iznos)
        {
            List<Račun> podmireniRačuni = new List<Račun>();
            double preostaliIznos = iznos;
            foreach (var item in SviRacuni)
            {
                if (item.Korisnik==korisnik && item.Plaćen==false && item.Iznos<preostaliIznos)
                {
                    podmireniRačuni.Add(item);
                    item.Plaćen = true;
                    preostaliIznos = preostaliIznos - item.Iznos;
                    Osoba.SmanjiDugovanje(korisnik, item.Iznos);
                }
            }
            IspišiPodmireneRačune(podmireniRačuni, preostaliIznos);
        }
        public static void IspišiPodmireneRačune(List<Račun> računi, double ostatak)
        {
            model.Model.podaci.Add("Podmireni su računi:");
            model.Model.podaci.Add("----------------------");
            foreach (var item in računi)
            {
                model.Model.podaci.Add("Broj računa: " + item.GetId() + ", Datum: " + item.GetDatumIzdavanja() + ", Iznos: " + item.GetIznos());

            }
            model.Model.podaci.Add("-----------------------");
            model.Model.podaci.Add("Nakon podmirenja računa za vratiti novaca: " + ostatak + " kn");
        }

        public void handleRequest(object request)
        {
            if ((request as string)=="evidentiraj")
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
