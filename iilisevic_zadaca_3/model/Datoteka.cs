using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Datoteka
    {
        public static int UcitajDatoteke()
        {
            try
            {
                ProcitajDatotekuLokacije();
            }
            catch (Exception)
            {

                model.Model.podaci.Add("Greška: Datoteka lokacije nije pronađena!");
                return 1;
            }
            try
            {
                ProcitajDatotekuVozila();
            }
            catch (Exception)
            {
                model.Model.podaci.Add("Greška: Datoteka vozila nije pronađena!");
                return 1;
            }
            try
            {
                ProcitajDatotekuOsobe();
            }
            catch (Exception)
            {
                model.Model.podaci.Add("Greška: Datoteka osobe nije pronađena!");
                return 1;
            }
            try
            {
                ProcitajDatotekuCjenik();
            }
            catch (Exception)
            {
                model.Model.podaci.Add("Greška: Datoteka cjenik nije pronađena!");
                return 1;
            }
            try
            {
                ProcitajDatotekuKapaciteti();
            }
            catch (Exception)
            {
                model.Model.podaci.Add("Greška: Datoteka kapaciteti nije pronađena!");
                return 1;
            }
            try
            {
                ProcitajDatotekuTvrtka();
            }
            catch (Exception)
            {
                model.Model.podaci.Add("Greška: Datoteka tvrtka nije pronađena!");
                return 1;
            }
            return 0;
        }
        public static void ProcitajDatotekuLokacije()
        {
            string[] lokacije = File.ReadAllLines(model.Model.DatLokacije);
            lokacije = lokacije.Skip(1).ToArray();
            foreach (var item in lokacije)
            {
                try
                {
                    bool ispravan = true;
                    string[] polje = item.Split(';');
                    var id = 0;
                    try
                    {
                        id = int.Parse(polje[0]);
                    }
                    catch (Exception)
                    {
                        ispravan = false;
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje ID nije broj!");
                    }
                    string naziv = polje[1];
                    if (naziv == " ")
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Naziv je prazno!");
                        ispravan = false;
                    }
                    string adresa = polje[2];
                    if (adresa == " ")
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Adresa je prazno!");
                        ispravan = false;
                    }
                    string gps = polje[3];
                    if (gps == " ")
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Gps je prazno!");
                        ispravan = false;
                    }
                    if (Lokacija.DohvatiLokaciju(id) != null)
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: lokacija s danim ID-jem već postoji!");
                        ispravan = false;
                    }
                    if (ispravan)
                    {
                        Lokacija novaLokacija = new Lokacija(id, naziv, adresa, gps);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        public static void ProcitajDatotekuVozila()
        {
            string[] vozila = File.ReadAllLines(model.Model.DatVozila);
            vozila = vozila.Skip(1).ToArray();
            foreach (var item in vozila)
            {
                try
                {
                    bool ispravan = true;
                    string[] polje = item.Split(';');
                    var id = 0;
                    try
                    {
                        id = int.Parse(polje[0]);
                    }
                    catch (Exception)
                    {
                        ispravan = false;
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje ID nije broj!");
                    }
                    string naziv = polje[1];
                    if (naziv == " ")
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Naziv je prazno!");
                        ispravan = false;
                    }
                    int vrijemePunjenja = 0;
                    try
                    {
                        vrijemePunjenja = int.Parse(polje[2]);
                    }
                    catch (Exception)
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Vrijeme punjenja nije broj!");
                        ispravan = false;
                    }
                    int domet = 0;
                    try
                    {
                        domet = int.Parse(polje[3]);
                    }
                    catch (Exception)
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Domet nije broj!");
                        ispravan = false;
                    }
                    if (Vozilo.DohvatiVozilo(id) != null)
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: vozilo s danim ID-jem već postoji!");
                        ispravan = false;
                    }
                    if (ispravan)
                    {
                        Vozilo novoVozilo = new Vozilo(id, naziv, vrijemePunjenja, domet);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        public static void ProcitajDatotekuOsobe()
        {
            string[] osobe = File.ReadAllLines(model.Model.DatOsobe);
            osobe = osobe.Skip(1).ToArray();
            foreach (var item in osobe)
            {
                try
                {
                    bool ispravan = true;
                    string[] polje = item.Split(';');
                    var id = 0;
                    try
                    {
                        id = int.Parse(polje[0]);
                    }
                    catch (Exception)
                    {
                        model.Model.podaci.Add(item);
                        ispravan = false;
                        model.Model.podaci.Add("Neispravan redak: polje ID nije broj!");
                    }
                    string imePrezime = polje[1];
                    if (imePrezime == " ")
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Ime i prezime je prazno!");
                        ispravan = false;
                    }
                    int ugovor = 0;
                    try
                    {
                        ugovor = int.Parse(polje[2]);
                    }
                    catch (Exception)
                    {
                        model.Model.podaci.Add(item);
                        ispravan = false;
                        model.Model.podaci.Add("Neispravan redak: polje Ugovor nije broj!");

                    }
                    if (ugovor!=0 && ugovor!=1)
                    {
                        model.Model.podaci.Add(item);
                        ispravan = false;
                        model.Model.podaci.Add("Neispravan redak: polje Ugovor nije 1 ili 0!");
                    }
                    if (Osoba.DohvatiOsobu(id) != null)
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: osoba s danim ID-jem već postoji!");
                        ispravan = false;
                    }
                    if (ispravan)
                    {
                        bool imaUgovor = false;
                        if (ugovor==1)
                        {
                            imaUgovor = true;
                        }
                        Osoba novaOsoba = new Osoba(id, imePrezime, imaUgovor);
                        novaOsoba.SetDugovanje(0);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        public static void ProcitajDatotekuCjenik()
        {
            string[] cjenik = File.ReadAllLines(model.Model.DatCjenik);
            cjenik = cjenik.Skip(1).ToArray();
            foreach (var item in cjenik)
            {
                try
                {
                    bool ispravan = true;
                    string[] polje = item.Split(';');
                    var voziloID = 0;
                    Vozilo voziloCjenik = null;
                    try
                    {
                        voziloID = int.Parse(polje[0]);
                        voziloCjenik = Vozilo.DohvatiVozilo(voziloID);
                        if (voziloCjenik == null)
                        {
                            model.Model.podaci.Add(item);
                            model.Model.podaci.Add("Neispravan redak: vozilo s danim ID-jem ne postoji!");
                            ispravan = false;
                        }
                    }
                    catch (Exception)
                    {
                        ispravan = false;
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje ID vrste vozila nije broj!");
                    }
                    var najam = 0.0;
                    try
                    {
                        najam = double.Parse(polje[1]);
                    }
                    catch (Exception)
                    {
                        ispravan = false;
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Najam nije broj!");
                    }
                    var poSatu = 0.0;
                    try
                    {
                        poSatu = double.Parse(polje[2]);
                    }
                    catch (Exception)
                    {
                        ispravan = false;
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Po satu nije broj!");
                    }
                    var poKm = 0.0;
                    try
                    {
                        poKm = double.Parse(polje[3]);
                    }
                    catch (Exception)
                    {
                        ispravan = false;
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Po km nije broj!");
                    }
                    if (ispravan)
                    {
                        Cjenik noviCjenik = new Cjenik(voziloCjenik, najam, poSatu, poKm);

                    }
                }
                catch (Exception)
                {
                }
            }
      }
        public static void ProcitajDatotekuKapaciteti()
        {
            string[] lokacijeKapaciteti = File.ReadAllLines(model.Model.DatKapaciteti);
            lokacijeKapaciteti = lokacijeKapaciteti.Skip(1).ToArray();
            Random rand = new Random();
            foreach (var item in lokacijeKapaciteti)
            {
                try
                {
                    bool ispravan = true;
                    string[] polje = item.Split(';');
                    var lokacijaID = 0;
                    Lokacija lokacijaKapaciteta = null;
                    try
                    {
                        lokacijaID = int.Parse(polje[0]);
                        lokacijaKapaciteta = Lokacija.DohvatiLokaciju(lokacijaID);
                        if (lokacijaKapaciteta == null)
                        {
                            model.Model.podaci.Add(item);
                            model.Model.podaci.Add("Neispravan redak: lokacija s danim ID-jem ne postoji!");
                            ispravan = false;
                        }
                    }
                    catch (Exception)
                    {
                        ispravan = false;
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje ID lokacije nije broj!");
                    }
                    var voziloID = 0;
                    Vozilo voziloKapaciteta = null;
                    try
                    {
                        voziloID = int.Parse(polje[1]);
                        voziloKapaciteta = Vozilo.DohvatiVozilo(voziloID);
                        if (voziloKapaciteta == null)
                        {
                            model.Model.podaci.Add(item);
                            model.Model.podaci.Add("Neispravan redak: vozilo s danim ID-jem ne postoji!");
                            ispravan = false;
                        }
                    }
                    catch (Exception)
                    {
                        ispravan = false;
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje ID vrste vozila nije broj!");
                    }
                    var brojMjesta = 0;
                    try
                    {
                        brojMjesta = int.Parse(polje[2]);
                    }
                    catch (Exception)
                    {
                        ispravan = false;
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Broj mjesta po vrsti vozila nije broj!");
                    }
                    var raspoloziviBrojVozila = 0;
                    try
                    {
                        raspoloziviBrojVozila = int.Parse(polje[3]);
                    }
                    catch (Exception)
                    {
                        ispravan = false;
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: polje Raspoloživi broj vozila nije broj!");
                    }
                    if (raspoloziviBrojVozila > brojMjesta)
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: broj vozila ne može biti veći od broja mjesta!");
                        ispravan = false;
                    }
                    if (ispravan)
                    {
                        Kapacitet noviKapacitet = new Kapacitet(lokacijaKapaciteta, voziloKapaciteta, brojMjesta, raspoloziviBrojVozila);

                        VoziloInstanca voziloInstanca = new VoziloInstanca
                        {
                            ID = voziloKapaciteta.GetId(),
                            Naziv = voziloKapaciteta.GetNaziv(),
                            Domet = voziloKapaciteta.GetDomet(),
                            VrijemePunjenja = voziloKapaciteta.GetVrijemePunjenja(),
                        };
                        voziloInstanca.SetLokacija(lokacijaKapaciteta);
                        voziloInstanca.SetStanjeBaterije(100);
                        voziloInstanca.SetBrojKm(0);
                        voziloInstanca.SetBrojNajma(0);
                        NaPunjenju naPunjenju = new NaPunjenju();
                        naPunjenju.SetStanje(voziloInstanca, false);
                        Posuđeno posuđeno = new Posuđeno();
                        posuđeno.SetStanje(voziloInstanca, false);
                        Neispravno neispravno = new Neispravno();
                        neispravno.SetStanje(voziloInstanca, false);
                        for (int i = 0; i < raspoloziviBrojVozila; i++)
                        {
                            voziloInstanca.SetRegistracija(rand.Next(1000, 9999) + i);
                            voziloInstanca.Clone();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        public static void ProcitajDatotekuTvrtka()
        {
            string[] tvrtka = File.ReadAllLines(model.Model.DatTvrtka);
            tvrtka = tvrtka.Skip(1).ToArray();
            foreach (var item in tvrtka)
            {
                string[] polje = item.Split(';');
                bool ispravan = true;
                int id = 0;
                string naziv = " ";
                try
                {
                    id = int.Parse(polje[0]);
                }
                catch
                {
                    model.Model.podaci.Add(item);
                    model.Model.podaci.Add("Neispravan redak: ID mora biti broj!");
                    ispravan = false;
                }
                naziv = polje[1];
                int nadredjenaJedinica = 0;
                bool nadredjena = false;
                if (polje[2] == " ")
                {
                    nadredjena = true;
                }
                else if (nadredjena == false)
                {
                    try
                    {
                        nadredjenaJedinica = int.Parse(polje[2]);
                    }
                    catch (Exception)
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: ID nadređene lokacije mora biti broj!");
                        ispravan = false;
                    }
                }
                int[] lokacije = { };
                if (polje[3] != "")
                {
                    try
                    {
                        lokacije = polje[3].Split(',').Select(x => int.Parse(x)).ToArray();
                    }
                    catch (Exception)
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: ID lokacije nije broj!");
                        ispravan = false;
                    }
                }
                List<Lokacija> podredjeneLokacije = new List<Lokacija>();
                foreach (var lokacija in lokacije)
                {
                    if (Lokacija.DohvatiLokaciju(lokacija) == null)
                    {
                        model.Model.podaci.Add(item);
                        model.Model.podaci.Add("Neispravan redak: Lokacija s danim ID=" + lokacija + "ne postoji!");
                        ispravan = false;
                    }
                    else
                    {
                        podredjeneLokacije.Add(Lokacija.DohvatiLokaciju(lokacija));
                    }
                }
                OrganizacijskaJedinica orgJedinica = model.Model.sveJedinice.Where(x => x.GetId() == nadredjenaJedinica).FirstOrDefault();
                if (orgJedinica == null && !nadredjena)
                {
                    ispravan = false;
                    model.Model.podaci.Add(item);
                    model.Model.podaci.Add("Neispravan redak: Organizacijska jednica koja je nadređena ne postoji!");
                }
                OrganizacijskaJedinica stablo = new OrganizacijskaJedinica();
                if (ispravan)
                {
                    OrganizacijskaJedinica grana = new OrganizacijskaJedinica();
                    if (nadredjena)
                    {
                        Tvrtka.Instance.Id = 1;
                        Tvrtka.Instance.Naziv = naziv;
                        grana.SetId(id);
                        grana.SetNaziv(naziv);
                        grana.SetLokacije(podredjeneLokacije);
                        if (orgJedinica == null)
                        {
                            orgJedinica = grana;
                        }
                    }
                    else
                    {
                        grana.SetId(id);
                        grana.SetNaziv(naziv);
                        grana.SetNadredjenaJedinica(orgJedinica);
                        grana.SetLokacije(podredjeneLokacije);
                        orgJedinica.TemplateMethod(grana, true, false);
                    }
                    model.Model.sveJedinice.Add(grana);
                    stablo.TemplateMethod(grana, true, false);

                }
                Lokacija list = new Lokacija();
                if (stablo.IsComposite())
                {
                    stablo.Add(list);
                }
            }
            model.Model.iterator = model.Model.jedinicaIterator.GetIterator(model.Model.sveJedinice);
        }
    }
}
