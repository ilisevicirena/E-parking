using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Provjera
    {
        public static int ProvjeriUnesenoVrijeme()
        {
            try
            {
                model.Model.Vrijeme = DateTime.Parse(model.Model.UnesenoVrijeme);
            }
            catch (Exception)
            {
                model.Model.podaci.Add("Vrijeme nije ispravno uneseno");
                return 1;
            }
            return 0;
        }
        public static bool ProvjeriDatumVeciOdPrethodnog(DateTime datumAktivnosti)
        {
            Aktivnost aktivnosti = Aktivnost.VratiPosljednjuAktivnost();          
            DateTime datumPrethodneAktivnosti;
            if (aktivnosti == null)
            {
                datumPrethodneAktivnosti = model.Model.Vrijeme;
            }
            else
            {
                datumPrethodneAktivnosti = aktivnosti.GetDatum();
            }
            if (datumAktivnosti < datumPrethodneAktivnosti)
            {
                return false;
            }
            return true;
        }
        public static Osoba ProvjeriKorisnika(string idKorisnika)
        {
            int id;
            try
            {
                id = int.Parse(idKorisnika);
            }
            catch (Exception)
            {
                model.Model.podaci.Add("Greška: ID korisnika mora biti broj!");
                return null;
            }
            if (Osoba.DohvatiOsobu(id) != null)
            {
                Osoba korisnik = Osoba.DohvatiOsobu(id);
                return korisnik;
            }
            else
            {
                model.Model.podaci.Add("Greška: korisnik ne postoji!");
            }
            return null;
        }
        public static Lokacija ProvjeriLokaciju(string idLokacija)
        {
            int id;
            try
            {
                id = int.Parse(idLokacija);
            }
            catch (Exception)
            {
                model.Model.podaci.Add("Greška: ID lokacije mora biti broj!");
                return null;
            }
            if (Lokacija.DohvatiLokaciju(id) != null)
            {
                Lokacija lokacija = Lokacija.DohvatiLokaciju(id);
                return lokacija;
            }
            else
            {
                model.Model.podaci.Add("Greška: lokacija ne postoji!");
            }
            return null;
        }
        public static Vozilo ProvjeriVozilo(string idVozilo)
        {
            int id;
            try
            {
                id = int.Parse(idVozilo);
            }
            catch (Exception)
            {
                model.Model.podaci.Add("Greška: ID vozila mora biti broj!");
                return null;
            }
            if (Vozilo.DohvatiVozilo(id) != null)
            {
                Vozilo vozilo = Vozilo.DohvatiVozilo(id);
                return vozilo;
            }
            else
            {
                model.Model.podaci.Add("Greška: vozilo ne postoji!");
            }
            return null;
        }
    }
}
