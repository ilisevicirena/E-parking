using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3.model
{
    public class Model
    {
        public static string DatVozila = " ";
        public static string DatLokacije = " ";
        public static string DatCjenik = " ";
        public static string DatKapaciteti = " ";
        public static string DatOsobe = " ";
        public static string DatAktivnosti = " ";
        public static string DatTvrtka = " ";
        public static string DatKonfiguracija = " ";
        public static string DatIzlaz = " ";
        public static DateTime Vrijeme = DateTime.Now;
        public static string UnesenoVrijeme = " ";
        public static List<OrganizacijskaJedinica> sveJedinice = new List<OrganizacijskaJedinica>();
        public static JedinicaIterator jedinicaIterator = new JedinicaIterator();
        public static Iterator iterator;
        public static Neispravno neispravnoStanje = new Neispravno();
        public static Posuđeno posuđenoStanje = new Posuđeno();
        public static NaPunjenju naPunjenjuStanje = new NaPunjenju();
        public static double MaxDugovanje;
        public static FileStream fileStream;
        public static StreamWriter streamWriter;
        public static TextWriter textWriter = Console.Out;
        public static List<string> podaci;

        public Model()
        {
            podaci = new List<string>();
        }

        public List<string> GetPodaciZaIspis()
        {
            return podaci;
        }
        public void EmptyData()
        {
            podaci.Clear();
        }
        public int ParseArguments(string[] args)
        {
            int vrati=0;
            if (args.Length == 1)
            {
                bool ispravno = true;
                try
                {
                    DatKonfiguracija = args[0];
                }
                catch (Exception)
                {
                    podaci.Add("Greška: Pogrešno unesen argument!");
                    ispravno = false;
                }
                if (ispravno)
                {
                    vrati = 1;
                }               
            }
            else
            {
                vrati = 2;
            }
            return vrati;
        }
        public int SetOutputStream()
        {
            if (DatIzlaz != " " && NacinRada.Instance.SkupniNacinRada)
            {
                try
                {
                    fileStream = new FileStream(DatIzlaz, FileMode.OpenOrCreate, FileAccess.Write);
                    streamWriter = new StreamWriter(fileStream);
                }
                catch (Exception)
                {
                    podaci.Add("Greška: Nemoguće pisati u datoteku!");
                    return 1;
                }
                Console.SetOut(streamWriter);
            }
            return 0;
        }
        public int OdrediPocetniView()
        {
            if (DatIzlaz==" " && DatAktivnosti==" " && NacinRada.Instance.InteraktivniNacinRada)
            {
                return 1;
            }
            else if (DatIzlaz == " " && DatAktivnosti !=" " && NacinRada.Instance.SkupniNacinRada)
            {
                return 2;
            }
            else if (DatIzlaz!=" " && DatAktivnosti!=" " && NacinRada.Instance.SkupniNacinRada)
            {
                return 3;
            }
            return 0;
        }
        public int UcitajDatoteke()
        {
            int vrati = Datoteka.UcitajDatoteke();
            return vrati;
        }    
        public int UcitajParametreIzDatKonfiguracije()
        {
            string[] konfiguracija;
            try
            {
                konfiguracija = File.ReadAllLines(DatKonfiguracija);
            }
            catch (Exception)
            {
                podaci.Add("Greška: Datoteka konfiguracije nije pronađena!");
                return 1;
            }
            konfiguracija = konfiguracija.ToArray();
            foreach (var item in konfiguracija)
            {
                string[] polje = item.Split('=');
                for (int i = 0; i < polje.Length; i++)
                {
                    switch (polje[i])
                    {
                        case "vozila":
                            DatVozila = polje[i + 1];
                            break;
                        case "lokacije":
                            DatLokacije = polje[i + 1];
                            break;
                        case "cjenik":
                            DatCjenik = polje[i + 1];
                            break;
                        case "kapaciteti":
                            DatKapaciteti = polje[i + 1];
                            break;
                        case "osobe":
                            DatOsobe = polje[i + 1];
                            break;
                        case "vrijeme":
                            string vrijeme = polje[i + 1];
                            string datum1 = vrijeme.Replace('„', '\0');
                            vrijeme = datum1.Replace('"', '\0');
                            datum1 = vrijeme.Replace("-", ".");
                            UnesenoVrijeme = datum1;
                            break;
                        case "struktura":
                            DatTvrtka = polje[i + 1];
                            break;
                        case "aktivnosti":
                            DatAktivnosti = polje[i + 1];
                            NacinRada.Instance.SkupniNacinRada = true;
                            NacinRada.Instance.InteraktivniNacinRada = false;
                            break;
                        case "dugovanje":
                            MaxDugovanje = double.Parse(polje[i + 1]);
                            break;
                        case "izlaz":
                            DatIzlaz = polje[i + 1];
                            break;
                        default:
                            break;
                    }
                }
            }
            return 0;
        }
        public static int UcitajAktivnostInteraktivniNacinRada()
        {          
            string aktivnost = Console.ReadLine();
            string[] polje = aktivnost.Split(';');
            int vratiti = UtvrdiAktivnost(polje);
            return vratiti;
        }
        public static int UtvrdiAktivnost(string[] polje)
        {
            int vrati = 9;
            DateTime datum = DateTime.Now;
            if (polje[0] == "5" || polje[0] == "6" || polje[0] == "7" || polje[0] == "8" || polje[0] == "9" || polje[0] == "10" || polje[0] == "11")
            {
                _ = UtvrdiKomandu(polje, datum);
            }
            else
            {
                try
                {
                    datum = DateTime.Parse(FormatirajDatumIVrijeme(polje));
                }
                catch (Exception)
                {
                    podaci.Add("Greška: datum neispravno unesen!");
                    return vrati;
                }
                if (!Provjera.ProvjeriDatumVeciOdPrethodnog(datum))
                {
                    podaci.Add("Greška: datum aktivnosti mora biti veći od datuma prethodne aktivnosti!");
                    return vrati;
                }
                switch (polje[0])
                {
                    case "0":
                        Aktivnost novaAktivnost = new Aktivnost.AktivnostBuilder(0, datum).Build();
                        Aktivnost.SveAktivnosti.Add(novaAktivnost);
                        return -1;

                    case "1":
                        Osoba korisnik = Provjera.ProvjeriKorisnika(polje[2]);
                        Lokacija lokacija = Provjera.ProvjeriLokaciju(polje[3]);
                        Vozilo vozilo = Provjera.ProvjeriVozilo(polje[4]);
                        if (korisnik != null && lokacija != null && vozilo != null)
                        {
                            Aktivnost novaAktivnost1 = new Aktivnost.AktivnostBuilder(1, datum).SetLokacija(lokacija).SetOsoba(korisnik).SetVozilo(vozilo).Build();
                            Aktivnost.SveAktivnosti.Add(novaAktivnost1);
                            podaci.Add("Raspoloživi broj vozila " + vozilo.GetNaziv() + " na lokaciji " + lokacija.GetNaziv() + " je: " + Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum));
                        }
                        break;

                    case "2":
                        korisnik = Provjera.ProvjeriKorisnika(polje[2]);
                        lokacija = Provjera.ProvjeriLokaciju(polje[3]);
                        vozilo = Provjera.ProvjeriVozilo(polje[4]);
                        bool ispravno = true;
                        if (Aktivnost.ProvjeriKorisnikImaNajam(korisnik, vozilo))
                        {
                            podaci.Add("Greška: korisnik već ima unajmljeno vozilo!");
                            ispravno = false;
                        }
                        else if (Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum) <= 0)
                        {
                            podaci.Add("Greška: nema raspoloživih vozila za najam!");
                            ispravno = false;
                        }
                        else if (VoziloInstanca.DohvatiVoziloTipa(vozilo, false) == null)
                        {
                            podaci.Add("Greška: vozilo nema punu bateriju!");
                            ispravno = false;
                        }
                        if (ispravno && korisnik != null && lokacija != null && vozilo != null)
                        {
                            if (korisnik.GetDugovanje() >= MaxDugovanje)
                            {
                                podaci.Add("Greška: Dugovanje korisnika veće od maksimalnog dugovanja!");
                            }
                            else
                            {
                                VoziloInstanca instanca = Posuđeno.DohvatiVoziloZaPosudbu(vozilo, lokacija);
                                Aktivnost novaAktivnost2 = new Aktivnost.AktivnostBuilder(2, datum).SetVozilo(vozilo).SetLokacija(lokacija).SetOsoba(korisnik).SetInstanca(instanca).Build();
                                Aktivnost.SveAktivnosti.Add(novaAktivnost2);
                                Aktivnost.NajamAktivnosti.Add(novaAktivnost2);
                                posuđenoStanje.SetStanje(instanca, true);
                                instanca.SetBrojNajma(instanca.GetBrojNajma() + 1);
                                korisnik.SetZadnjiNajam(datum);
                                Kapacitet.UnajmiVozilo(lokacija, vozilo);
                                podaci.Add("Vozilo je unajmljeno!" + " Registracija vozila je: " + instanca.GetRegistracija());
                            }

                        }
                        break;

                    case "3":
                        korisnik = Provjera.ProvjeriKorisnika(polje[2]);
                        lokacija = Provjera.ProvjeriLokaciju(polje[3]);
                        vozilo = Provjera.ProvjeriVozilo(polje[4]);
                        if (korisnik != null && lokacija != null && vozilo != null)
                        {
                            Aktivnost novaAktivnost3 = new Aktivnost.AktivnostBuilder(3, datum).SetOsoba(korisnik).SetVozilo(vozilo).SetLokacija(lokacija).Build();
                            Aktivnost.SveAktivnosti.Add(novaAktivnost3);
                            podaci.Add("Raspoloživi broj mjesta za vozilo " + vozilo.GetNaziv() + " na lokaciji " + lokacija.GetNaziv() + " je " + Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo));
                        }
                        break;

                    case "4":
                        korisnik = Provjera.ProvjeriKorisnika(polje[2]);
                        lokacija = Provjera.ProvjeriLokaciju(polje[3]);
                        vozilo = Provjera.ProvjeriVozilo(polje[4]);
                        int brojKM = 0;
                        bool ispravan = true;
                        bool neispravno = false;
                        string opisProblema = "";
                        try
                        {
                            opisProblema = polje[6];
                            neispravno = true;
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            brojKM = int.Parse(polje[5]);
                        }
                        catch (Exception)
                        {
                            podaci.Add("Greška: broj km mora biti broj!");
                            ispravan = false;
                        }
                        if (Aktivnost.ProvjeriKorisnikImaNajam(korisnik, vozilo) == false)
                        {
                            podaci.Add("Greška: korisnik nema unajmljeno vozilo!");
                            ispravan = false;
                        }
                        else if (Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo) <= 0)
                        {
                            podaci.Add("Greška: nema raspoloživih mjesta!");
                            ispravan = false;
                        }
                        if (ispravan && korisnik != null && lokacija != null && vozilo != null)
                        {
                            try
                            {
                                VoziloInstanca instanca = Aktivnost.DohvatiPosudjenuInstancu(korisnik, vozilo);
                                bool uRedu = true;
                                int domet = vozilo.GetDomet();
                                int pocetniBrojKM = instanca.GetBrojKm();
                                int brojPredenihKM = brojKM - pocetniBrojKM;
                                if (brojKM <= instanca.GetBrojKm())
                                {
                                    podaci.Add("Greška: broj kilometara ne može biti manji od prethodnog broja kilometara!");
                                    uRedu = false;
                                }
                                if (brojPredenihKM > domet)
                                {
                                    podaci.Add("Greška: broj pređenih kilometara ne smije biti veći od dometa vozila!");
                                    uRedu = false;
                                }
                                if (uRedu)
                                {
                                    DateTime vrijemePosudbe = Aktivnost.VrijemePosudbe(korisnik, vozilo);
                                    Aktivnost.IzbrisiNajam(korisnik, vozilo);
                                    NaPunjenju.IzracunajStanjeBaterije(brojPredenihKM, instanca);
                                    instanca.SetZadnjeVracanje(datum);
                                    Kapacitet.VratiVozilo(lokacija, vozilo);
                                    podaci.Add("Vozilo vraćeno! Registracija vozila je: " + instanca.GetRegistracija());
                                    double brojSatiUPosudbi = (int)Math.Ceiling(((datum - vrijemePosudbe).TotalHours));
                                    int lokacijaNajmaID = instanca.GetLokacija().GetId();
                                    int lokacijaVracanjaID = lokacija.GetId();
                                    Račun.IzracunajRacun(vozilo, brojSatiUPosudbi, brojPredenihKM, instanca, korisnik, lokacijaNajmaID, lokacijaVracanjaID, datum);
                                    
                                    instanca.SetBrojKm(brojKM);
                                    instanca.SetLokacija(lokacija);
                                    if (neispravno)
                                    {
                                        Aktivnost novaAktivnost4 = new Aktivnost.AktivnostBuilder(4, datum).SetBrojKm(brojKM).SetLokacija(lokacija).SetOsoba(korisnik).SetVozilo(vozilo).SetInstanca(instanca).SetOpisProblema(opisProblema).Build();
                                        Aktivnost.SveAktivnosti.Add(novaAktivnost4);
                                        korisnik.SetBrojNeispravnihVozila(korisnik.GetBrojNeispranihVozila() + 1);
                                        neispravnoStanje.SetStanje(instanca, true);
                                    }
                                    else
                                    {
                                        Aktivnost novaAktivnost4 = new Aktivnost.AktivnostBuilder(4, datum).SetBrojKm(brojKM).SetLokacija(lokacija).SetOsoba(korisnik).SetVozilo(vozilo).SetInstanca(instanca).Build();
                                        Aktivnost.SveAktivnosti.Add(novaAktivnost4);
                                        posuđenoStanje.SetStanje(instanca, false);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Korisnik nije unajmio to vozilo!");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return 0;
        }
        public static int UtvrdiKomandu(string[] polje, DateTime datum)
        {
            switch (polje[0])
            {
                case "5":
                    string datoteka = "";
                    bool ispravno5 = true;
                    try
                    {
                        datoteka = polje[1];
                    }
                    catch (Exception)
                    {
                        podaci.Add("Greška: Naziv datoteke s aktivnostima nije unesen!");
                        ispravno5 = false;
                    }
                    if (ispravno5)
                    {
                        if (datoteka.StartsWith(" "))
                        {
                            datoteka = datoteka.Remove(0, 1);
                        }
                        DatAktivnosti = datoteka;
                        controller.Controller controller = new controller.Controller(new Model(),new view.ViewSkupniEkran());
                        controller.InteraktivniUSkupni();
                    }
                    break;

                case "6":
                    string komanda = "";
                    bool ispravno6 = true;
                    try
                    {
                        komanda = polje[1];
                    }
                    catch (Exception)
                    {
                        podaci.Add("Greška: Niste unijeli željenu komandu!");
                        ispravno6 = false;
                    }
                    if (ispravno6)
                    {
                        if (komanda.StartsWith(" "))
                        {
                            komanda = komanda.Remove(0, 1);
                        }
                        string[] komande = komanda.Split(' ');
                        if (komande.Length == 1)
                        {
                            if (komande[0] == "struktura")
                            {
                                podaci.Add("---------Ispis strukture tvrtke------------");
                                podaci.Add(" ");
                                Ispis.IspisStrukture(iterator.GetObjectByIndex(Tvrtka.Instance.Id));
                            }
                            else if (komande[0] == "stanje")
                            {
                                podaci.Add("---------Ispis stanja tvrtke------------");
                                podaci.Add(" ");
                                Ispis.IspisStanja(iterator.GetObjectByIndex(Tvrtka.Instance.Id));
                            }
                            else
                            {
                                podaci.Add("Greška: Komanda ne postoji!");
                            }
                        }
                        else if (komande.Length == 2)
                        {
                            if ((komande[0] == "struktura" && komande[1] == "stanje") || (komande[0] == "stanje" && komande[1] == "struktura"))
                            {
                                podaci.Add("---------Ispis strukture i stanja tvrtke------------");
                                podaci.Add(" ");
                                Ispis.IspisStruktureIStanja(iterator.GetObjectByIndex(Tvrtka.Instance.Id));
                            }
                            else if (komande[0] == "struktura")
                            {
                                int id = 0;
                                bool ispranvanID = true;
                                try
                                {
                                    id = int.Parse(komande[1]);
                                }
                                catch (Exception)
                                {
                                    podaci.Add("Greška: ID mora biti broj!");
                                    ispranvanID = false;
                                }
                                if (iterator.GetObjectByIndex(id) == null)
                                {
                                    podaci.Add("Greška: Organizacijska jedinica s danim ID-jem ne postoji!");
                                    ispranvanID = false;
                                }
                                if (ispranvanID)
                                {
                                    podaci.Add("----Ispis strukture organizacijske jedinice----");
                                    podaci.Add(" ");
                                    Ispis.IspisStrukture(iterator.GetObjectByIndex(id));
                                }
                            }
                            else if (komande[0] == "stanje")
                            {
                                int id = 0;
                                bool ispranvanID = true;
                                try
                                {
                                    id = int.Parse(komande[1]);
                                }
                                catch (Exception)
                                {
                                    podaci.Add("Greška: ID mora biti broj!");
                                    ispranvanID = false;
                                }
                                if (iterator.GetObjectByIndex(id) == null)
                                {
                                    podaci.Add("Greška: Organizacijska jedinica s danim ID-jem ne postoji!");
                                    ispranvanID = false;
                                }
                                if (ispranvanID)
                                {
                                    podaci.Add("----Ispis stanja organizacijske jedinice----");
                                    podaci.Add(" ");
                                    Ispis.IspisStanja(iterator.GetObjectByIndex(id));
                                }
                            }
                            else
                            {
                                podaci.Add("Greška: Komanda ne postoji!");
                            }
                        }
                        else if (komande.Length == 3)
                        {
                            if ((komande[0] == "struktura" && komande[1] == "stanje") || (komande[0] == "stanje" && komande[1] == "struktura"))
                            {
                                int id = 0;
                                bool ispranvanID = true;
                                try
                                {
                                    id = int.Parse(komande[2]);
                                }
                                catch (Exception)
                                {
                                    podaci.Add("Greška: ID mora biti broj!");
                                    ispranvanID = false;
                                }
                                if (iterator.GetObjectByIndex(id) == null)
                                {
                                    podaci.Add("Greška: Organizacijska jedinica s danim ID-jem ne postoji!");
                                    ispranvanID = false;
                                }
                                if (ispranvanID)
                                {
                                    podaci.Add("----Ispis strukture i stanja organizacijske jedinice----");
                                    podaci.Add(" ");
                                    Ispis.IspisStruktureIStanja(iterator.GetObjectByIndex(id));
                                }
                            }
                            else
                            {
                                podaci.Add("Greška: Komanda ne postoji!");
                            }
                        }
                        else
                        {
                            podaci.Add("Greška: Komanda ne postoji!");
                        }
                    }
                    break;

                case "7":
                    string komanda7 = "";
                    bool ispravno7 = true;
                    try
                    {
                        komanda7 = polje[1];
                    }
                    catch (Exception)
                    {
                        podaci.Add("Greška: Niste unijeli željenu komandu!");
                        ispravno7 = false;
                    }
                    if (ispravno7)
                    {
                        if (komanda7.StartsWith(" "))
                        {
                            komanda7 = komanda7.Remove(0, 1);
                        }
                        string[] komande = komanda7.Split(' ');
                        if (komande.Length == 3)
                        {
                            bool datumOk = true;
                            DateTime pocetak = DateTime.Now;
                            DateTime kraj = DateTime.Now;
                            try
                            {
                                pocetak = DateTime.ParseExact(komande[1], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            try
                            {
                                kraj = DateTime.ParseExact(komande[2], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            if (datumOk)
                            {
                                if (komande[0] == "struktura")
                                {
                                    podaci.Add("---------Ispis strukture tvrtke------------");
                                    podaci.Add(" ");
                                    Ispis.IspisStrukture(iterator.GetObjectByIndex(Tvrtka.Instance.Id));
                                }
                                else if (komande[0] == "najam")
                                {
                                    podaci.Add("---------Ispis najma tvrtke------------");
                                    podaci.Add(" ");
                                    Ispis.IspisNajam(iterator.GetObjectByIndex(Tvrtka.Instance.Id), pocetak, kraj);
                                }
                                else if (komande[0] == "zarada")
                                {
                                    podaci.Add("---------Ispis zarade tvrtke------------");
                                    podaci.Add(" ");
                                    Ispis.IspisZarada(iterator.GetObjectByIndex(Tvrtka.Instance.Id), pocetak, kraj);
                                }
                                else
                                {
                                    podaci.Add("Greška: Komanda ne postoji!");
                                }
                            }
                        }
                        else if (komande.Length == 4)
                        {
                            if (komande[1] == "struktura" || komande[1] == "najam" || komande[2] == "zarada")
                            {
                                bool datumOk = true;
                                DateTime pocetak = DateTime.Now;
                                DateTime kraj = DateTime.Now;
                                try
                                {
                                    pocetak = DateTime.ParseExact(komande[2], "dd.mm.yyyy", null);
                                }
                                catch (Exception)
                                {
                                    podaci.Add("Greška: Datum neispravno unesen!");
                                    datumOk = false;
                                }
                                try
                                {
                                    kraj = DateTime.ParseExact(komande[3], "dd.mm.yyyy", null);
                                }
                                catch (Exception)
                                {
                                    podaci.Add("Greška: Datum neispravno unesen!");
                                    datumOk = false;
                                }
                                if (datumOk)
                                {
                                    if ((komande[0] == "struktura" && komande[1] == "najam") || (komande[0] == "najam" && komande[1] == "struktura"))
                                    {
                                        podaci.Add("---------Ispis strukture i najma tvrtke------------");
                                        podaci.Add(" ");
                                        Ispis.IspisStrukturaNajam(iterator.GetObjectByIndex(Tvrtka.Instance.Id), pocetak, kraj);
                                    }
                                    else if ((komande[0] == "struktura" && komande[1] == "zarada") || (komande[0] == "zarada" && komande[1] == "struktura"))
                                    {
                                        podaci.Add("---------Ispis strukture i zarade tvrtke------------");
                                        podaci.Add(" ");
                                        Ispis.IspisStrukturaZarada(iterator.GetObjectByIndex(Tvrtka.Instance.Id), pocetak, kraj);
                                    }
                                    else if ((komande[0] == "najam" && komande[1] == "zarada") || (komande[0] == "zarada" && komande[1] == "najam"))
                                    {
                                        podaci.Add("---------Ispis najma i zarade tvrtke------------");
                                        podaci.Add(" ");
                                        Ispis.IspisNajamZarada(iterator.GetObjectByIndex(Tvrtka.Instance.Id), pocetak, kraj);
                                    }
                                }
                            }
                            else
                            {
                                bool datumOk = true;
                                DateTime pocetak = DateTime.Now;
                                DateTime kraj = DateTime.Now;
                                try
                                {
                                    pocetak = DateTime.ParseExact(komande[1], "dd.mm.yyyy", null);
                                }
                                catch (Exception)
                                {
                                    podaci.Add("Greška: Datum neispravno unesen!");
                                    datumOk = false;
                                }
                                try
                                {
                                    kraj = DateTime.ParseExact(komande[2], "dd.mm.yyyy", null);
                                }
                                catch (Exception)
                                {
                                    podaci.Add("Greška: Datum neispravno unesen!");
                                    datumOk = false;
                                }
                                if (datumOk)
                                {
                                    if (komande[0] == "struktura")
                                    {
                                        int id = 0;
                                        bool ispranvanID = true;
                                        try
                                        {
                                            id = int.Parse(komande[3]);
                                        }
                                        catch (Exception)
                                        {
                                            podaci.Add("Greška: ID mora biti broj!");
                                            ispranvanID = false;
                                        }
                                        if (iterator.GetObjectByIndex(id) == null)
                                        {
                                            podaci.Add("Greška: Organizacijska jedinica s danim ID-jem ne postoji!");
                                            ispranvanID = false;
                                        }
                                        if (ispranvanID)
                                        {
                                            podaci.Add("----Ispis strukture organizacijske jedinice----");
                                            podaci.Add(" ");
                                            Ispis.IspisStrukture(iterator.GetObjectByIndex(id));
                                        }
                                    }
                                    else if (komande[0] == "najam")
                                    {
                                        int id = 0;
                                        bool ispranvanID = true;
                                        try
                                        {
                                            id = int.Parse(komande[3]);
                                        }
                                        catch (Exception)
                                        {
                                            podaci.Add("Greška: ID mora biti broj!");
                                            ispranvanID = false;
                                        }
                                        if (iterator.GetObjectByIndex(id) == null)
                                        {
                                            podaci.Add("Greška: Organizacijska jedinica s danim ID-jem ne postoji!");
                                            ispranvanID = false;
                                        }
                                        if (ispranvanID)
                                        {
                                            podaci.Add("----Ispis najma organizacijske jedinice----");
                                            podaci.Add(" ");
                                            Ispis.IspisNajam(iterator.GetObjectByIndex(id), pocetak, kraj);
                                        }
                                    }
                                    else if (komande[0] == "zarada")
                                    {
                                        int id = 0;
                                        bool ispranvanID = true;
                                        try
                                        {
                                            id = int.Parse(komande[3]);
                                        }
                                        catch (Exception)
                                        {
                                            podaci.Add("Greška: ID mora biti broj!");
                                            ispranvanID = false;
                                        }
                                        if (iterator.GetObjectByIndex(id) == null)
                                        {
                                            podaci.Add("Greška: Organizacijska jedinica s danim ID-jem ne postoji!");
                                            ispranvanID = false;
                                        }
                                        if (ispranvanID)
                                        {
                                            podaci.Add("----Ispis zarade organizacijske jedinice----");
                                            podaci.Add(" ");
                                            Ispis.IspisZarada(iterator.GetObjectByIndex(id), pocetak, kraj);
                                        }
                                    }
                                }
                                else
                                {
                                    podaci.Add("Greška: Komanda ne postoji!");
                                }
                            }
                        }

                        else if (komande.Length == 5)
                        {
                            bool datumOk = true;
                            DateTime pocetak = DateTime.Now;
                            DateTime kraj = DateTime.Now;
                            try
                            {
                                pocetak = DateTime.ParseExact(komande[2], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            try
                            {
                                kraj = DateTime.ParseExact(komande[3], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            OrganizacijskaJedinica jedinica = new OrganizacijskaJedinica();
                            try
                            {
                                jedinica = iterator.GetObjectByIndex(int.Parse(komande[4]));
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Organizacijka jedinica s danim ID-jem ne postoji!");
                                datumOk = false;
                            }
                            if (datumOk)
                            {
                                if ((komande[0] == "struktura" && komande[1] == "najam") || (komande[0] == "najam" && komande[1] == "struktura"))
                                {
                                    podaci.Add("---------Ispis strukture i najma organizacijske jedinice------------");
                                    podaci.Add(" ");
                                    Ispis.IspisStrukturaNajam(jedinica, pocetak, kraj);
                                }
                                else if ((komande[0] == "struktura" && komande[1] == "zarada") || (komande[0] == "zarada" && komande[1] == "struktura"))
                                {
                                    podaci.Add("---------Ispis strukture i zarade organizacijske jedinice------------");
                                    podaci.Add(" ");
                                    Ispis.IspisStrukturaZarada(jedinica, pocetak, kraj);
                                }
                                else if ((komande[0] == "najam" && komande[1] == "zarada") || (komande[0] == "zarada" && komande[1] == "najam"))
                                {
                                    podaci.Add("---------Ispis najma i zarade organizacijke jedinice------------");
                                    podaci.Add(" ");
                                    Ispis.IspisNajamZarada(jedinica, pocetak, kraj);
                                }
                                else
                                {
                                    podaci.Add("Greška: Komanda ne postoji!");
                                }
                            }
                        }
                        else if (komande.Length == 6)
                        {
                            bool datumOk = true;
                            DateTime pocetak = DateTime.Now;
                            DateTime kraj = DateTime.Now;
                            try
                            {
                                pocetak = DateTime.ParseExact(komande[3], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            try
                            {
                                kraj = DateTime.ParseExact(komande[4], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            OrganizacijskaJedinica jedinica = new OrganizacijskaJedinica();
                            try
                            {
                                jedinica = iterator.GetObjectByIndex(int.Parse(komande[5]));
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Organizacijka jedinica s danim ID-jem ne postoji!");
                                datumOk = false;
                            }
                            if (datumOk)
                            {
                                if ((komande[0] == "struktura" && komande[1] == "najam" && komande[2] == "zarada") ||
                                    (komande[0] == "struktura" && komande[1] == "zarada" && komande[2] == "najam") ||
                                    (komande[0] == "najam" && komande[1] == "struktura" && komande[2] == "zarada") ||
                                    (komande[0] == "najam" && komande[1] == "zarada" && komande[2] == "struktura") ||
                                    (komande[0] == "zarada" && komande[1] == "struktura" && komande[2] == "najam") ||
                                    (komande[0] == "zarada" && komande[1] == "najam" && komande[2] == "struktura")
                                    )
                                {
                                    podaci.Add("---------Ispis strukture, najma i zarade organizacijske jednice------------");
                                    podaci.Add(" ");
                                    Ispis.IspisStrukturaNajamZarada(jedinica, pocetak, kraj);
                                }
                                else
                                {
                                    podaci.Add("Greška: Komanda ne postoji!");
                                }
                            }
                        }
                    }
                    break;

                case "8":
                    string komanda8 = "";
                    bool ispravno8 = true;
                    try
                    {
                        komanda8 = polje[1];
                    }
                    catch (Exception)
                    {
                        podaci.Add("Greška: Niste unijeli željenu komandu!");
                        ispravno8 = false;
                    }
                    if (ispravno8)
                    {
                        if (komanda8.StartsWith(" "))
                        {
                            komanda8 = komanda8.Remove(0, 1);
                        }
                        string[] komande = komanda8.Split(' ');
                        if (komande.Length == 3)
                        {
                            bool datumOk = true;
                            DateTime pocetak = DateTime.Now;
                            DateTime kraj = DateTime.Now;
                            try
                            {
                                pocetak = DateTime.ParseExact(komande[1], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            try
                            {
                                kraj = DateTime.ParseExact(komande[2], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            if (datumOk)
                            {
                                if (komande[0] == "struktura")
                                {
                                    podaci.Add("---------Ispis strukture tvrtke------------");
                                    podaci.Add(" ");
                                    Ispis.IspisStrukture(iterator.GetObjectByIndex(Tvrtka.Instance.Id));
                                }
                                else if (komande[0] == "računi")
                                {
                                    podaci.Add("---------Ispis računa tvrtke------------");
                                    podaci.Add(" ");
                                    Ispis.IspisRacuni(iterator.GetObjectByIndex(Tvrtka.Instance.Id), pocetak, kraj);
                                }
                                else
                                {
                                    podaci.Add("Greška: Komanda ne postoji!");
                                }
                            }
                        }
                        else if (komande.Length == 4)
                        {
                            if (komande[1] == "struktura" || komande[1] == "računi")
                            {
                                bool datumOk = true;
                                DateTime pocetak = DateTime.Now;
                                DateTime kraj = DateTime.Now;
                                try
                                {
                                    pocetak = DateTime.ParseExact(komande[2], "dd.mm.yyyy", null);
                                }
                                catch (Exception)
                                {
                                    podaci.Add("Greška: Datum neispravno unesen!");
                                    datumOk = false;
                                }
                                try
                                {
                                    kraj = DateTime.ParseExact(komande[3], "dd.mm.yyyy", null);
                                }
                                catch (Exception)
                                {
                                    podaci.Add("Greška: Datum neispravno unesen!");
                                    datumOk = false;
                                }
                                if (datumOk)
                                {
                                    if ((komande[0] == "struktura" && komande[1] == "računi") || (komande[0] == "računi" && komande[1] == "struktura"))
                                    {
                                        podaci.Add("---------Ispis strukture i računa tvrtke------------");
                                        podaci.Add(" ");
                                        Ispis.IspisStrukturaRacuni(iterator.GetObjectByIndex(Tvrtka.Instance.Id), pocetak, kraj);
                                    }
                                }
                            }
                            else
                            {
                                bool datumOk = true;
                                DateTime pocetak = DateTime.Now;
                                DateTime kraj = DateTime.Now;
                                try
                                {
                                    pocetak = DateTime.ParseExact(komande[1], "dd.mm.yyyy", null);
                                }
                                catch (Exception)
                                {
                                    podaci.Add("Greška: Datum neispravno unesen!");
                                    datumOk = false;
                                }
                                try
                                {
                                    kraj = DateTime.ParseExact(komande[2], "dd.mm.yyyy", null);
                                }
                                catch (Exception)
                                {
                                    podaci.Add("Greška: Datum neispravno unesen!");
                                    datumOk = false;
                                }
                                if (datumOk)
                                {
                                    if (komande[0] == "računi")
                                    {
                                        int id = 0;
                                        bool ispranvanID = true;
                                        try
                                        {
                                            id = int.Parse(komande[3]);
                                        }
                                        catch (Exception)
                                        {
                                            podaci.Add("Greška: ID mora biti broj!");
                                            ispranvanID = false;
                                        }
                                        if (iterator.GetObjectByIndex(id) == null)
                                        {
                                            podaci.Add("Greška: Organizacijska jedinica s danim ID-jem ne postoji!");
                                            ispranvanID = false;
                                        }
                                        if (ispranvanID)
                                        {
                                            podaci.Add("----Ispis računa organizacijske jedinice----");
                                            podaci.Add(" ");
                                            Ispis.IspisRacuni(iterator.GetObjectByIndex(id), pocetak, kraj);
                                        }
                                    }
                                    else if (komande[0] == "struktura")
                                    {
                                        int id = 0;
                                        bool ispranvanID = true;
                                        try
                                        {
                                            id = int.Parse(komande[3]);
                                        }
                                        catch (Exception)
                                        {
                                            podaci.Add("Greška: ID mora biti broj!");
                                            ispranvanID = false;
                                        }
                                        if (iterator.GetObjectByIndex(id) == null)
                                        {
                                            podaci.Add("Greška: Organizacijska jedinica s danim ID-jem ne postoji!");
                                            ispranvanID = false;
                                        }
                                        if (ispranvanID)
                                        {
                                            podaci.Add("----Ispis računa organizacijske jedinice----");
                                            podaci.Add(" ");
                                            Ispis.IspisStrukture(iterator.GetObjectByIndex(id));
                                        }
                                    }
                                }
                                else
                                {
                                    podaci.Add("Greška: Komanda ne postoji!");
                                }
                            }
                        }
                        else if (komande.Length == 5)
                        {
                            bool datumOk = true;
                            DateTime pocetak = DateTime.Now;
                            DateTime kraj = DateTime.Now;
                            try
                            {
                                pocetak = DateTime.ParseExact(komande[2], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            try
                            {
                                kraj = DateTime.ParseExact(komande[3], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            OrganizacijskaJedinica jedinica = new OrganizacijskaJedinica();
                            try
                            {
                                jedinica = iterator.GetObjectByIndex(int.Parse(komande[4]));
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Organizacijka jedinica s danim ID-jem ne postoji!");
                                datumOk = false;
                            }
                            if (datumOk)
                            {
                                if ((komande[0] == "struktura" && komande[1] == "računi") || (komande[0] == "računi" && komande[1] == "struktura"))
                                {
                                    podaci.Add("---------Ispis strukture i računa organizacijske jedinice------------");
                                    podaci.Add(" ");
                                    Ispis.IspisStrukturaRacuni(jedinica, pocetak, kraj);
                                }
                                else
                                {
                                    podaci.Add("Greška: Komanda ne postoji!");
                                }
                            }
                        }
                    }
                    break;

                case "9":
                    podaci.Add("-------------Ispis financijskog stanja korisnika-------------");
                    podaci.Add(" ");
                    Ispis.IspisFinancijskogStanjaKorisnika();
                    break;

                case "10":
                    string komanda10 = "";
                    bool ispravno10 = true;
                    try
                    {
                        komanda10 = polje[1];
                    }
                    catch (Exception)
                    {
                        podaci.Add("Greška: Niste unijeli željenu komandu!");
                        ispravno10 = false;
                    }
                    if (ispravno10)
                    {
                        if (komanda10.StartsWith(" "))
                        {
                            komanda10 = komanda10.Remove(0, 1);
                        }
                        string[] komande = komanda10.Split(' ');
                        if (komande.Length == 3)
                        {
                            bool datumOk = true;
                            DateTime pocetak = DateTime.Now;
                            DateTime kraj = DateTime.Now;
                            Osoba korisnik = Provjera.ProvjeriKorisnika(komande[0]);
                            if (korisnik == null)
                            {
                                datumOk = false;
                            }
                            try
                            {
                                pocetak = DateTime.ParseExact(komande[1], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            try
                            {
                                kraj = DateTime.ParseExact(komande[2], "dd.mm.yyyy", null);
                            }
                            catch (Exception)
                            {
                                podaci.Add("Greška: Datum neispravno unesen!");
                                datumOk = false;
                            }
                            if (datumOk)
                            {

                                podaci.Add("-------------Ispis podataka o računima korisnika-------------");
                                podaci.Add(" ");
                                Ispis.IspisRacunaKorisnika(korisnik, pocetak, kraj);
                            }
                        }
                        else
                        {
                            podaci.Add("Greška: Neispravna komanda!");
                        }
                    }
                    break;

                case "11":
                    string komanda11 = "";
                    bool ispravno11 = true;
                    try
                    {
                        komanda11 = polje[1];
                    }
                    catch (Exception)
                    {
                        podaci.Add("Greška: Niste unijeli željenu komandu!");
                        ispravno11 = false;
                    }
                    if (ispravno11)
                    {
                        if (komanda11.StartsWith(" "))
                        {
                            komanda11 = komanda11.Remove(0, 1);
                        }
                        string[] komande = komanda11.Split(' ');
                        bool ok = true;
                        Osoba korisnik = Provjera.ProvjeriKorisnika(komande[0]);
                        if (korisnik == null)
                        {
                            ok = false;
                        }
                        double iznos = 0;
                        try
                        {
                            iznos = double.Parse(komande[1]);
                        }
                        catch (Exception)
                        {
                            podaci.Add("Greška: Iznos mora biti broj!");
                            ok = false;
                        }
                        if (ok)
                        {
                            Račun.PodmiriRačune(korisnik, iznos);
                        }
                    }
                    break;
                default:
                    break;
            }
            return 0;
        }
        public static string FormatirajDatumIVrijeme(string[] polje)
        {
            string datum1, datum2;
            datum1 = polje[1];
            datum2 = datum1.Replace('“', ' ');
            datum1 = datum2.Replace('„', ' ');
            datum2 = datum1.Replace("-", ".");
            datum1 = datum2.Replace('"', ' ');
            return datum1;
        }
    }
}

