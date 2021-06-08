using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Ispis
    {
        private static readonly int SirinaTablice = 110;
        public static void IspisiCrtu()
        {
            model.Model.podaci.Add(new string('-', SirinaTablice));
        }
        public static void IspisiRed(params string[] stupci)
        {
            int sirina = (SirinaTablice - stupci.Length) / stupci.Length;
            string red = "|";
            foreach (string stupac in stupci)
            {
                red += PoravnajTekst(stupac, sirina) + "|";
            }
            model.Model.podaci.Add(red);
        }
        static string PoravnajTekst(string tekst, int sirina)
        {
            tekst = tekst.Length > sirina ? tekst.Substring(0, sirina - 3) : tekst;
            if (string.IsNullOrEmpty(tekst))
            {
                return new string(' ', sirina);
            }
            else
            {
                return tekst.PadRight(sirina).PadLeft(sirina);
            }
        }
        public static void IspisStrukture(OrganizacijskaJedinica ishodisna)
        {
            List<IspisRed> redovi = new List<IspisRed>();
            OrganizacijskaJedinica.PopuniStupce(ishodisna, new List<string>(), redovi);

            foreach (var item in ishodisna.GetLokacije())
            {
                IspisRed ispis = new IspisRed();
                ispis.Stupac.Add(item.GetNaziv());
                redovi.Add(ispis);
            }
            int brojStupaca = redovi.Max(x => x.Stupac.Count);
            IspisiCrtu();
            List<string> zaglavlje = new List<string>();
            for (int i = 0; i < brojStupaca; i++)
            {
                zaglavlje.Add("Organizacijska jedinica");
            }
            zaglavlje.Add("Lokacija");
            IspisiRed(zaglavlje.ToArray());
            IspisiCrtu();
            IspisiCrtu();
            foreach (var item in redovi)
            {
                List<string> redak = new List<string>();
                redak.Add(ishodisna.GetNaziv());
                for (int i = 0; i < item.Stupac.Count; i++)
                {
                    if (brojStupaca > item.Stupac.Count && i == item.Stupac.Count - 1)
                    {
                        for (int j = 0; j < brojStupaca - item.Stupac.Count; j++)
                        {
                            redak.Add("");
                        }
                    }
                    redak.Add(item.Stupac[i]);
                }
                IspisiRed(redak.ToArray());
                IspisiCrtu();
            }
        }
        public static void IspisStruktureIStanja(OrganizacijskaJedinica ishodisna)
        {
            DateTime datum = DateTime.Now;
            IspisiCrtu();
            IspisiRed("Struktura", "Broj raspolozivih mjesta", "Broj raspoloživih vozila", "Broj neispravnih vozila");
            IspisiCrtu();
            IspisiCrtu();
            IspisiRed(ishodisna.GetNaziv(), " ", " ", " ");
            IspisiCrtu();

            foreach (OrganizacijskaJedinica item in ishodisna.Podređene)
            {
                IspisiRed("  " + item.GetNaziv(), " ", " ", " ");
                IspisiCrtu();
                if (item.Podređene.Count > 0)
                {
                    foreach (OrganizacijskaJedinica ite in item.Podređene)
                    {
                        IspisiRed("    " + ite.GetNaziv(), " ", " ", " ");
                        IspisiCrtu();
                        ite.TemplateMethod(ite, false, false);
                        if (ite.Podređene.Count > 0)
                        {
                            foreach (OrganizacijskaJedinica it in ite.Podređene)
                            {
                                IspisiRed("     " + it.GetNaziv(), " ", " ", " ");
                                IspisiCrtu();
                                if (it.Podređene.Count > 0)
                                {
                                    int ukMjesta = 0;
                                    int ukVozila = 0;
                                    int ukNeispravno = 0;
                                    int globUKMjesta4 = 0;
                                    int globUkVozila4 = 0;
                                    int globUkNeispravno4 = 0;

                                    foreach (OrganizacijskaJedinica i in it.Podređene)
                                    {
                                        IspisiRed("      " + it.GetNaziv(), " ", " ", " ");
                                        IspisiCrtu();
                                        i.TemplateMethod(i, false, false);
                                        foreach (var vozilo in Vozilo.SvaVozila)
                                        {
                                            int sumaMjesta = 0;
                                            int sumaVozila = 0;
                                            int sumaPokvarenih = 0;
                                            foreach (var mjesto in i.BrojMjesta)
                                            {
                                                if (mjesto.Key == vozilo)
                                                {
                                                    sumaMjesta += mjesto.Value;
                                                }
                                            }
                                            foreach (var vozilovrsta in i.BrojVozila)
                                            {
                                                if (vozilovrsta.Key == vozilo)
                                                {
                                                    sumaVozila += vozilovrsta.Value;
                                                }
                                            }
                                            foreach (var neispravno in i.BrojNeispravnihVozila)
                                            {
                                                if (neispravno.Key == vozilo)
                                                {
                                                    sumaPokvarenih += neispravno.Value;
                                                }
                                            }
                                            IspisiRed("   " + vozilo.GetNaziv(),
                                                sumaMjesta.ToString(),
                                                sumaVozila.ToString(),
                                                sumaPokvarenih.ToString());
                                            IspisiCrtu();
                                            ukMjesta += sumaMjesta;
                                            ukVozila += sumaVozila;
                                            ukNeispravno += sumaPokvarenih;
                                        }
                                        IspisiRed("      " + "Kumulativ",
                                            ukMjesta.ToString(),
                                            ukVozila.ToString(),
                                            ukNeispravno.ToString());
                                        IspisiCrtu();
                                        globUKMjesta4 += ukMjesta;
                                        globUkVozila4 += ukVozila;
                                        globUkNeispravno4 += ukNeispravno;
                                    }

                                    IspisiRed("    Kumulativ",
                                        globUKMjesta4.ToString(),
                                        globUkVozila4.ToString(),
                                        globUkNeispravno4.ToString());
                                    IspisiCrtu();
                                }
                                int globUKMjesta3 = 0;
                                int globUkVozila3 = 0;
                                int globUkNeispravno3 = 0;

                                foreach (Lokacija lokacija in it.GetLokacije())
                                {
                                    int ukMjesta = 0;
                                    int ukVozila = 0;
                                    int ukNeispravno = 0;
                                    IspisiRed("        " + lokacija.GetNaziv(), " ", " ", " ");
                                    IspisiCrtu();
                                    foreach (var vozilo in Vozilo.SvaVozila)
                                    {
                                        IspisiRed("         " + vozilo.GetNaziv(),
                                            Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo).ToString(),
                                            Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum).ToString(),
                                            Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija).ToString());
                                        IspisiCrtu();
                                        ukMjesta += Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo);
                                        ukVozila += Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum);
                                        ukNeispravno += Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija);
                                    }

                                    IspisiRed("          " + "Kumulativ",
                                        ukMjesta.ToString(),
                                        ukVozila.ToString(),
                                        ukNeispravno.ToString());
                                    IspisiCrtu();
                                    globUKMjesta3 += ukMjesta;
                                    globUkVozila3 += ukVozila;
                                    globUkNeispravno3 += ukNeispravno;
                                }
                                IspisiRed("        Kumulativ",
                                    globUKMjesta3.ToString(),
                                    globUkVozila3.ToString(),
                                    globUkNeispravno3.ToString());
                                IspisiCrtu();
                            }
                        }
                        int globUKMjesta2 = 0;
                        int globUkVozila2 = 0;
                        int globUkNeispravno2 = 0;
                        foreach (Lokacija lokacija in ite.GetLokacije())
                        {
                            int ukMjesta = 0;
                            int ukVozila = 0;
                            int ukNeispravno = 0;
                            IspisiRed("      " + lokacija.GetNaziv(), " ", " ", " ");
                            IspisiCrtu();
                            foreach (var vozilo in Vozilo.SvaVozila)
                            {
                                IspisiRed("       " + vozilo.GetNaziv(),
                                    Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo).ToString(),
                                    Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum).ToString(),
                                    Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija).ToString());
                                IspisiCrtu();
                                ukMjesta += Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo);
                                ukVozila += Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum);
                                ukNeispravno += Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija);
                            }
                            IspisiRed("       " + "Kumulativ",
                                ukMjesta.ToString(),
                                ukVozila.ToString(),
                                ukNeispravno.ToString());
                            IspisiCrtu();
                            globUKMjesta2 += ukMjesta;
                            globUkVozila2 += ukVozila;
                            globUkNeispravno2 += ukNeispravno;
                        }
                        IspisiRed("     Kumulativ",
                            globUKMjesta2.ToString(),
                            globUkVozila2.ToString(),
                            globUkNeispravno2.ToString());
                        IspisiCrtu();
                    }
                }
                int globUKMjesta1 = 0;
                int globUkVozila1 = 0;
                int globUkNeispravno1 = 0;
                foreach (Lokacija lokacija in item.GetLokacije())
                {
                    int ukMjesta = 0;
                    int ukVozila = 0;
                    int ukNeispravno = 0;
                    IspisiRed("    " + lokacija.GetNaziv(), " ", " ", " ");
                    IspisiCrtu();
                    foreach (var vozilo in Vozilo.SvaVozila)
                    {
                        IspisiRed("     " + vozilo.GetNaziv(),
                            Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo).ToString(),
                            Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum).ToString(),
                            Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija).ToString());
                        IspisiCrtu();
                        ukMjesta += Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo);
                        ukVozila += Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum);
                        ukNeispravno += Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija);
                    }
                    IspisiRed("      " + "Kumulativ",
                        ukMjesta.ToString(),
                        ukVozila.ToString(),
                        ukNeispravno.ToString());
                    IspisiCrtu();
                    globUKMjesta1 += ukMjesta;
                    globUkVozila1 += ukVozila;
                    globUkNeispravno1 += ukNeispravno;
                }
                IspisiRed("     Kumulativ",
                    globUKMjesta1.ToString(),
                    globUkVozila1.ToString(),
                    globUkNeispravno1.ToString());
                IspisiCrtu();
            }
            int globUKMjesta = 0;
            int globUkVozila = 0;
            int globUkNeispravno = 0;
            foreach (Lokacija lokacija in ishodisna.GetLokacije())
            {
                int ukMjesta = 0;
                int ukVozila = 0;
                int ukNeispravno = 0;
                IspisiRed("  " + lokacija.GetNaziv(), " ", " ", " ");
                IspisiCrtu();
                foreach (var vozilo in Vozilo.SvaVozila)
                {
                    IspisiRed("   " + vozilo.GetNaziv(),
                        Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo).ToString(),
                        Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum).ToString(),
                        Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija).ToString());
                    IspisiCrtu();
                    ukMjesta += Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo);
                    ukVozila += Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum);
                    ukNeispravno += Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija);
                }
                IspisiRed("    " + "Kumulativ",
                    ukMjesta.ToString(),
                    ukVozila.ToString(),
                    ukNeispravno.ToString());
                IspisiCrtu();
                globUKMjesta += ukMjesta;
                globUkVozila += ukVozila;
                globUkNeispravno += ukNeispravno;
            }
            IspisiRed("  Kumulativ",
                globUKMjesta.ToString(),
                globUkVozila.ToString(),
                globUkNeispravno.ToString());
            IspisiCrtu();
        }
        public static void IspisStanja(OrganizacijskaJedinica ishodisna)
        {
            DateTime datum = DateTime.Now;
            IspisiCrtu();
            IspisiRed("Vrsta vozila", "Broj raspolozivih mjesta", "Broj raspoloživih vozila", "Broj neispravnih vozila");
            IspisiCrtu();
            IspisiCrtu();
            foreach (OrganizacijskaJedinica item in ishodisna.Podređene)
            {
                if (item.Podređene.Count > 0)
                {
                    foreach (OrganizacijskaJedinica ite in item.Podređene)
                    {
                        ite.TemplateMethod(ite, false, false);
                        if (ite.Podređene.Count > 0)
                        {
                            foreach (OrganizacijskaJedinica it in ite.Podređene)
                            {
                                if (it.Podređene.Count > 0)
                                {
                                    int ukMjesta = 0;
                                    int ukVozila = 0;
                                    int ukNeispravno = 0;
                                    int globUKMjesta4 = 0;
                                    int globUkVozila4 = 0;
                                    int globUkNeispravno4 = 0;
                                    foreach (OrganizacijskaJedinica i in it.Podređene)
                                    {
                                        i.TemplateMethod(i, false, false);
                                        foreach (var vozilo in Vozilo.SvaVozila)
                                        {
                                            int sumaMjesta = 0;
                                            int sumaVozila = 0;
                                            int sumaPokvarenih = 0;
                                            foreach (var mjesto in i.BrojMjesta)
                                            {
                                                if (mjesto.Key == vozilo)
                                                {
                                                    sumaMjesta += mjesto.Value;
                                                }
                                            }
                                            foreach (var vozilovrsta in i.BrojVozila)
                                            {
                                                if (vozilovrsta.Key == vozilo)
                                                {
                                                    sumaVozila += vozilovrsta.Value;
                                                }
                                            }
                                            foreach (var neispravno in i.BrojNeispravnihVozila)
                                            {
                                                if (neispravno.Key == vozilo)
                                                {
                                                    sumaPokvarenih += neispravno.Value;
                                                }
                                            }
                                            IspisiRed("   " + vozilo.GetNaziv(),
                                                sumaMjesta.ToString(),
                                                sumaVozila.ToString(),
                                                sumaPokvarenih.ToString());
                                            IspisiCrtu();
                                            ukMjesta += sumaMjesta;
                                            ukVozila += sumaVozila;
                                            ukNeispravno += sumaPokvarenih;
                                        }
                                        IspisiRed("      " + "Kumulativ",
                                            ukMjesta.ToString(),
                                            ukVozila.ToString(),
                                            ukNeispravno.ToString());
                                        IspisiCrtu();
                                        globUKMjesta4 += ukMjesta;
                                        globUkVozila4 += ukVozila;
                                        globUkNeispravno4 += ukNeispravno;
                                    }

                                    IspisiRed("    Kumulativ", globUKMjesta4.ToString(), globUkVozila4.ToString(), globUkNeispravno4.ToString());
                                    IspisiCrtu();
                                }
                                int globUKMjesta3 = 0;
                                int globUkVozila3 = 0;
                                int globUkNeispravno3 = 0;
                                foreach (Lokacija lokacija in it.GetLokacije())
                                {
                                    int ukMjesta = 0;
                                    int ukVozila = 0;
                                    int ukNeispravno = 0;
                                    foreach (var vozilo in Vozilo.SvaVozila)
                                    {
                                        IspisiRed("         " + vozilo.GetNaziv(),
                                            Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo).ToString(),
                                            Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum).ToString(),
                                            Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija).ToString());
                                        IspisiCrtu();
                                        ukMjesta += Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo);
                                        ukVozila += Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum);
                                        ukNeispravno += Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija);
                                    }
                                    IspisiRed("          " + "Kumulativ", ukMjesta.ToString(), ukVozila.ToString(), ukNeispravno.ToString());
                                    IspisiCrtu();
                                    globUKMjesta3 += ukMjesta;
                                    globUkVozila3 += ukVozila;
                                    globUkNeispravno3 += ukNeispravno;
                                }
                                IspisiRed("        Kumulativ", globUKMjesta3.ToString(), globUkVozila3.ToString(), globUkNeispravno3.ToString());
                                IspisiCrtu();
                            }
                        }
                        int globUKMjesta2 = 0;
                        int globUkVozila2 = 0;
                        int globUkNeispravno2 = 0;
                        foreach (Lokacija lokacija in ite.GetLokacije())
                        {
                            int ukMjesta = 0;
                            int ukVozila = 0;
                            int ukNeispravno = 0;
                            foreach (var vozilo in Vozilo.SvaVozila)
                            {
                                IspisiRed("       " + vozilo.GetNaziv(),
                                    Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo).ToString(),
                                    Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum).ToString(),
                                    Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija).ToString());
                                IspisiCrtu();
                                ukMjesta += Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo);
                                ukVozila += Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum);
                                ukNeispravno += Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija);
                            }
                            IspisiRed("       " + "Kumulativ", ukMjesta.ToString(), ukVozila.ToString(), ukNeispravno.ToString());
                            IspisiCrtu();
                            globUKMjesta2 += ukMjesta;
                            globUkVozila2 += ukVozila;
                            globUkNeispravno2 += ukNeispravno;
                        }
                        IspisiRed("     Kumulativ", globUKMjesta2.ToString(), globUkVozila2.ToString(), globUkNeispravno2.ToString());
                        IspisiCrtu();
                    }
                }
                int globUKMjesta1 = 0;
                int globUkVozila1 = 0;
                int globUkNeispravno1 = 0;
                foreach (Lokacija lokacija in item.GetLokacije())
                {
                    int ukMjesta = 0;
                    int ukVozila = 0;
                    int ukNeispravno = 0;
                    foreach (var vozilo in Vozilo.SvaVozila)
                    {
                        IspisiRed("     " + vozilo.GetNaziv(),
                            Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo).ToString(),
                            Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum).ToString(),
                            Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija).ToString());
                        IspisiCrtu();
                        ukMjesta += Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo);
                        ukVozila += Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum);
                        ukNeispravno += Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija);
                    }
                    IspisiRed("      " + "Kumulativ", ukMjesta.ToString(), ukVozila.ToString(), ukNeispravno.ToString());
                    IspisiCrtu();
                    globUKMjesta1 += ukMjesta;
                    globUkVozila1 += ukVozila;
                    globUkNeispravno1 += ukNeispravno;
                }
                IspisiRed("     Kumulativ", globUKMjesta1.ToString(), globUkVozila1.ToString(), globUkNeispravno1.ToString());
                IspisiCrtu();
            }
            int globUKMjesta = 0;
            int globUkVozila = 0;
            int globUkNeispravno = 0;
            foreach (Lokacija lokacija in ishodisna.GetLokacije())
            {
                int ukMjesta = 0;
                int ukVozila = 0;
                int ukNeispravno = 0;
                foreach (var vozilo in Vozilo.SvaVozila)
                {
                    IspisiRed("   " + vozilo.GetNaziv(),
                        Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo).ToString(),
                        Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum).ToString(),
                        Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija).ToString());
                    IspisiCrtu();
                    ukMjesta += Kapacitet.BrojRaspolozivihMjesta(lokacija, vozilo);
                    ukVozila += Kapacitet.BrojRaspolozivihVrstaVozila(lokacija, vozilo, datum);
                    ukNeispravno += Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, lokacija);
                }
                IspisiRed("    " + "Kumulativ", ukMjesta.ToString(), ukVozila.ToString(), ukNeispravno.ToString());
                IspisiCrtu();
                globUKMjesta += ukMjesta;
                globUkVozila += ukVozila;
                globUkNeispravno += ukNeispravno;
            }
            IspisiRed("  Kumulativ", globUKMjesta.ToString(), globUkVozila.ToString(), globUkNeispravno.ToString());
            IspisiCrtu();
        }
        public static void IspisStrukturaNajam(OrganizacijskaJedinica ishodisna, DateTime pocetak, DateTime kraj)
        {
            IspisiCrtu();
            IspisiRed("Vrsta vozila", "Broj najma", "Trajanje najma");
            IspisiCrtu();
            IspisiCrtu();
            IspisiRed(ishodisna.GetNaziv(), " ", " ");
            IspisiCrtu();

            foreach (OrganizacijskaJedinica item in ishodisna.Podređene)
            {
                IspisiRed("  " + item.GetNaziv(), " ", " ");
                IspisiCrtu();
                if (item.Podređene.Count > 0)
                {
                    foreach (OrganizacijskaJedinica ite in item.Podređene)
                    {
                        IspisiRed("    " + ite.GetNaziv(), " ", " ");
                        IspisiCrtu();
                        if (ite.Podređene.Count > 0)
                        {
                            foreach (OrganizacijskaJedinica it in ite.Podređene)
                            {
                                IspisiRed("      " + it.GetNaziv(), " ", " ");
                                IspisiCrtu();
                                if (it.Podređene.Count > 0)
                                {
                                    foreach (OrganizacijskaJedinica i in it.Podređene)
                                    {
                                        IspisiRed("        " + i.GetNaziv(), " ", " ");
                                        IspisiCrtu();
                                    }
                                }
                                foreach (Lokacija lokacija in it.GetLokacije())
                                {
                                    IspisiRed("        " + lokacija.GetNaziv(), " ", " ");
                                    IspisiCrtu();
                                    foreach (var vozilo in Vozilo.SvaVozila)
                                    {
                                        IspisiRed("         " + vozilo.GetNaziv(),
                                            Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                                            Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString());
                                        IspisiCrtu();
                                    }
                                }
                            }
                        }
                        foreach (Lokacija lokacija in ite.GetLokacije())
                        {
                            IspisiRed("      " + lokacija.GetNaziv(), " ", " ");
                            IspisiCrtu();
                            foreach (var vozilo in Vozilo.SvaVozila)
                            {
                                IspisiRed("       " + vozilo.GetNaziv(),
                                    Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                                    Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString());
                                IspisiCrtu();
                            }
                        }
                    }
                }
                foreach (Lokacija lokacija in item.GetLokacije())
                {
                    IspisiRed("    " + lokacija.GetNaziv(), " ", " ");
                    IspisiCrtu();
                    foreach (var vozilo in Vozilo.SvaVozila)
                    {
                        IspisiRed("     " + vozilo.GetNaziv(),
                            Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                            Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString());
                        IspisiCrtu();
                    }
                }
            }
            foreach (Lokacija lokacija in ishodisna.GetLokacije())
            {
                IspisiRed("  " + lokacija.GetNaziv(), " ", " ");
                IspisiCrtu();
                foreach (var vozilo in Vozilo.SvaVozila)
                {
                    IspisiRed("   " + vozilo.GetNaziv(),
                        Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                        Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString());
                    IspisiCrtu();
                }
            }
        }
        public static void IspisNajam(OrganizacijskaJedinica ishodisna, DateTime pocetak, DateTime kraj)
        {
            IspisiCrtu();
            IspisiRed("Vrsta vozila", "Broj najma", "Trajanje najma");
            IspisiCrtu();
            IspisiCrtu();

            foreach (OrganizacijskaJedinica item in ishodisna.Podređene)
            {
                if (item.Podređene.Count > 0)
                {
                    foreach (OrganizacijskaJedinica ite in item.Podređene)
                    {
                        if (ite.Podređene.Count > 0)
                        {
                            foreach (OrganizacijskaJedinica it in ite.Podređene)
                            {
                                foreach (Lokacija lokacija in it.GetLokacije())
                                {
                                    foreach (var vozilo in Vozilo.SvaVozila)
                                    {
                                        IspisiRed("         " + vozilo.GetNaziv(),
                                            Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                                            Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString());
                                        IspisiCrtu();
                                    }
                                }
                            }
                        }
                        foreach (Lokacija lokacija in ite.GetLokacije())
                        {
                            foreach (var vozilo in Vozilo.SvaVozila)
                            {
                                IspisiRed("       " + vozilo.GetNaziv(),
                                    Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                                    Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString());
                                IspisiCrtu();
                            }
                        }
                    }
                }
                foreach (Lokacija lokacija in item.GetLokacije())
                {
                    foreach (var vozilo in Vozilo.SvaVozila)
                    {
                        IspisiRed("     " + vozilo.GetNaziv(),
                            Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                            Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString());
                        IspisiCrtu();
                    }
                }
            }
            foreach (Lokacija lokacija in ishodisna.GetLokacije())
            {
                foreach (var vozilo in Vozilo.SvaVozila)
                {
                    IspisiRed("   " + vozilo.GetNaziv(), Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(), Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString());
                    IspisiCrtu();
                }
            }
        }
        public static void IspisStrukturaZarada(OrganizacijskaJedinica ishodisna, DateTime pocetak, DateTime kraj)
        {
            IspisiCrtu();
            IspisiRed("Struktura", "Zarada");
            IspisiCrtu();
            IspisiCrtu();
            IspisiRed(ishodisna.GetNaziv(), " ");
            IspisiCrtu();

            foreach (OrganizacijskaJedinica item in ishodisna.Podređene)
            {
                IspisiRed("  " + item.GetNaziv(), " ");
                IspisiCrtu();
                if (item.Podređene.Count > 0)
                {
                    foreach (OrganizacijskaJedinica ite in item.Podređene)
                    {
                        IspisiRed("    " + ite.GetNaziv(), " ");
                        IspisiCrtu();
                        if (ite.Podređene.Count > 0)
                        {
                            foreach (OrganizacijskaJedinica it in ite.Podređene)
                            {
                                IspisiRed("     " + it.GetNaziv(), " ");
                                IspisiCrtu();
                                if (it.Podređene.Count > 0)
                                {
                                    foreach (OrganizacijskaJedinica i in it.Podređene)
                                    {
                                        IspisiRed("      " + it.GetNaziv(), " ");
                                        IspisiCrtu();
                                    }
                                }
                                foreach (Lokacija lokacija in it.GetLokacije())
                                {
                                    IspisiRed("        " + lokacija.GetNaziv(), " ");
                                    IspisiCrtu();
                                    foreach (var vozilo in Vozilo.SvaVozila)
                                    {
                                        IspisiRed("         " + vozilo.GetNaziv(),
                                            Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                                        IspisiCrtu();
                                    }
                                }
                            }
                        }
                        foreach (Lokacija lokacija in ite.GetLokacije())
                        {
                            IspisiRed("      " + lokacija.GetNaziv(), " ");
                            IspisiCrtu();
                            foreach (var vozilo in Vozilo.SvaVozila)
                            {
                                IspisiRed("       " + vozilo.GetNaziv(),
                                    Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                                IspisiCrtu();
                            }
                        }
                    }
                }
                foreach (Lokacija lokacija in item.GetLokacije())
                {
                    IspisiRed("    " + lokacija.GetNaziv(), " ");
                    IspisiCrtu();
                    foreach (var vozilo in Vozilo.SvaVozila)
                    {
                        IspisiRed("     " + vozilo.GetNaziv(), Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                        IspisiCrtu();
                    }
                }
            }
            foreach (Lokacija lokacija in ishodisna.GetLokacije())
            {
                IspisiRed("  " + lokacija.GetNaziv(), " ");
                IspisiCrtu();
                foreach (var vozilo in Vozilo.SvaVozila)
                {
                    IspisiRed("   " + vozilo.GetNaziv(), Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                    IspisiCrtu();
                }
            }
        }
        public static void IspisZarada(OrganizacijskaJedinica ishodisna, DateTime pocetak, DateTime kraj)
        {
            IspisiCrtu();
            IspisiRed("Vrsta vozila", "Zarada");
            IspisiCrtu();
            IspisiCrtu();

            foreach (OrganizacijskaJedinica item in ishodisna.Podređene)
            {
                if (item.Podređene.Count > 0)
                {
                    foreach (OrganizacijskaJedinica ite in item.Podređene)
                    {
                        if (ite.Podređene.Count > 0)
                        {
                            foreach (OrganizacijskaJedinica it in ite.Podređene)
                            {
                                foreach (Lokacija lokacija in it.GetLokacije())
                                {
                                    foreach (var vozilo in Vozilo.SvaVozila)
                                    {
                                        IspisiRed("         " + vozilo.GetNaziv(),
                                            Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                                        IspisiCrtu();
                                    }
                                }
                            }
                        }
                        foreach (Lokacija lokacija in ite.GetLokacije())
                        {
                            foreach (var vozilo in Vozilo.SvaVozila)
                            {
                                IspisiRed("       " + vozilo.GetNaziv(),
                                    Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                                IspisiCrtu();
                            }
                        }
                    }
                }
                foreach (Lokacija lokacija in item.GetLokacije())
                {
                    foreach (var vozilo in Vozilo.SvaVozila)
                    {
                        IspisiRed("     " + vozilo.GetNaziv(), Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                        IspisiCrtu();
                    }
                }
            }
            foreach (Lokacija lokacija in ishodisna.GetLokacije())
            {
                foreach (var vozilo in Vozilo.SvaVozila)
                {
                    IspisiRed("   " + vozilo.GetNaziv(), Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                    IspisiCrtu();
                }
            }
        }
        public static void IspisNajamZarada(OrganizacijskaJedinica ishodisna, DateTime pocetak, DateTime kraj)
        {
            IspisiCrtu();
            IspisiRed("Vrsta vozila", "Broj najma", "Trajanje najma", "Zarada");
            IspisiCrtu();
            IspisiCrtu();
            foreach (OrganizacijskaJedinica item in ishodisna.Podređene)
            {
                if (item.Podređene.Count > 0)
                {
                    foreach (OrganizacijskaJedinica ite in item.Podređene)
                    {
                        if (ite.Podređene.Count > 0)
                        {
                            foreach (OrganizacijskaJedinica it in ite.Podređene)
                            {
                                foreach (Lokacija lokacija in it.GetLokacije())
                                {
                                    foreach (var vozilo in Vozilo.SvaVozila)
                                    {
                                        IspisiRed("         " + vozilo.GetNaziv(),
                                            Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                                            Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString(),
                                            Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                                        IspisiCrtu();
                                    }
                                }
                            }
                        }
                        foreach (Lokacija lokacija in ite.GetLokacije())
                        {
                            foreach (var vozilo in Vozilo.SvaVozila)
                            {
                                IspisiRed("       " + vozilo.GetNaziv(),
                                    Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                                    Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString(),
                                    Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                                IspisiCrtu();
                            }
                        }
                    }
                }
                foreach (Lokacija lokacija in item.GetLokacije())
                {
                    foreach (var vozilo in Vozilo.SvaVozila)
                    {
                        IspisiRed("     " + vozilo.GetNaziv(),
                            Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                            Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString(),
                            Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                        IspisiCrtu();
                    }
                }
            }
            foreach (Lokacija lokacija in ishodisna.GetLokacije())
            {
                foreach (var vozilo in Vozilo.SvaVozila)
                {
                    IspisiRed("   " + vozilo.GetNaziv(),
                        Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                        Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString(),
                        Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                    IspisiCrtu();
                }
            }
        }
        public static void IspisStrukturaNajamZarada(OrganizacijskaJedinica ishodisna, DateTime pocetak, DateTime kraj)
        {
            IspisiCrtu();
            IspisiRed("Struktura", "Broj najma", "Trajanje najma", "Zarada");
            IspisiCrtu();
            IspisiCrtu();
            IspisiRed(ishodisna.GetNaziv(), " ", " ", " ");
            IspisiCrtu();

            foreach (OrganizacijskaJedinica item in ishodisna.Podređene)
            {
                IspisiRed("  " + item.GetNaziv(), " ", " ", " ");
                IspisiCrtu();
                if (item.Podređene.Count > 0)
                {
                    foreach (OrganizacijskaJedinica ite in item.Podređene)
                    {
                        IspisiRed("    " + ite.GetNaziv(), " ", " ", " ");
                        IspisiCrtu();
                        if (ite.Podređene.Count > 0)
                        {
                            foreach (OrganizacijskaJedinica it in ite.Podređene)
                            {
                                IspisiRed("     " + it.GetNaziv(), " ", " ", " ");
                                IspisiCrtu();
                                if (it.Podređene.Count > 0)
                                {
                                    foreach (OrganizacijskaJedinica i in it.Podređene)
                                    {
                                        IspisiRed("      " + it.GetNaziv(), " ", " ", " ");
                                        IspisiCrtu();
                                    }
                                }
                                foreach (Lokacija lokacija in it.GetLokacije())
                                {
                                    IspisiRed("        " + lokacija.GetNaziv(), " ", " ", " ");
                                    IspisiCrtu();
                                    foreach (var vozilo in Vozilo.SvaVozila)
                                    {
                                        IspisiRed("         " + vozilo.GetNaziv(),
                                            Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                                            Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString(),
                                            Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                                        IspisiCrtu();
                                    }
                                }
                            }
                        }
                        foreach (Lokacija lokacija in ite.GetLokacije())
                        {
                            IspisiRed("      " + lokacija.GetNaziv(), " ", " ", " ");
                            IspisiCrtu();
                            foreach (var vozilo in Vozilo.SvaVozila)
                            {
                                IspisiRed("       " + vozilo.GetNaziv(),
                                    Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                                    Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString(),
                                    Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                                IspisiCrtu();
                            }
                        }
                    }
                }
                foreach (Lokacija lokacija in item.GetLokacije())
                {
                    IspisiRed("    " + lokacija.GetNaziv(), " ", " ", " ");
                    IspisiCrtu();
                    foreach (var vozilo in Vozilo.SvaVozila)
                    {
                        IspisiRed("     " + vozilo.GetNaziv(),
                            Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                            Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString(),
                           Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                        IspisiCrtu();
                    }
                }
            }
            foreach (Lokacija lokacija in ishodisna.GetLokacije())
            {
                IspisiRed("  " + lokacija.GetNaziv(), " ", " ", " ");
                IspisiCrtu();
                foreach (var vozilo in Vozilo.SvaVozila)
                {
                    IspisiRed("   " + vozilo.GetNaziv(),
                        Aktivnost.BrojNajmaZaVrstuVozila(vozilo, lokacija, pocetak, kraj).ToString(),
                        Račun.TrajanjeNajmaPoLokaciji(lokacija, pocetak, kraj, vozilo).ToString(),
                        Račun.IzracunajZaraduZaLokaciju(lokacija, pocetak, kraj, vozilo).ToString());
                    IspisiCrtu();
                }
            }
        }
        public static void IspisStrukturaRacuni(OrganizacijskaJedinica ishodisna, DateTime pocetak, DateTime kraj)
        {
            IspisiCrtu();
            IspisiRed("Struktura", "Vrijeme izdavanja", "Osoba", "ID lokacije najma", "Lokacija najma", "ID lokacije vraćanja", "Lokacija vraćanja", "Najam", "Po satu", "Po km", "Iznos");
            IspisiCrtu();
            IspisiCrtu();
            IspisiRed(ishodisna.GetNaziv(), " ", "", "", "", "", "", "", "", "", "");
            IspisiCrtu();

            foreach (OrganizacijskaJedinica item in ishodisna.Podređene)
            {
                IspisiRed(item.GetNaziv(), " ", "", "", "", "", "", "", "", "", "");
                IspisiCrtu();
                if (item.Podređene.Count > 0)
                {
                    foreach (OrganizacijskaJedinica ite in item.Podređene)
                    {
                        IspisiRed(ite.GetNaziv(), " ", "", "", "", "", "", "", "", "", "");
                        IspisiCrtu();
                        if (ite.Podređene.Count > 0)
                        {
                            foreach (OrganizacijskaJedinica it in ite.Podređene)
                            {
                                IspisiRed(it.GetNaziv(), " ", "", "", "", "", "", "", "", "", "");
                                IspisiCrtu();
                                if (it.Podređene.Count > 0)
                                {
                                    foreach (OrganizacijskaJedinica i in it.Podređene)
                                    {
                                        IspisiRed(it.GetNaziv(), " ", "", "", "", "", "", "", "", "", "");
                                        IspisiCrtu();
                                    }
                                }
                                foreach (Lokacija lokacija in it.GetLokacije())
                                {
                                    IspisiRed(lokacija.GetNaziv(), " ", "", "", "", "", "", "", "", "", "");
                                    IspisiCrtu();
                                    foreach (var vozilo in Vozilo.SvaVozila)
                                    {
                                        IspisiRed(vozilo.GetNaziv(), " ", "", "", "", "", "", "", "", "", "");
                                        IspisiCrtu();
                                        foreach (var rac in Račun.DohvatiRacunePoLokacijiIVrstiVozila(vozilo, lokacija, pocetak, kraj))
                                        {
                                            IspisiRed(rac.GetId().ToString(),
                                         rac.GetDatumIzdavanja().ToString(),
                                         rac.GetOsoba().GetImePrezime(),
                                         rac.GetLokacijaNajmaId().ToString(),
                                         Lokacija.DohvatiLokaciju(rac.GetLokacijaNajmaId()).GetNaziv(),
                                         rac.GetLokacijaVracanjaId().ToString(),
                                         Lokacija.DohvatiLokaciju(rac.GetLokacijaVracanjaId()).GetNaziv(),
                                         rac.GetNajam().ToString(),
                                         rac.GetPoSatu().ToString(),
                                         rac.GetPoKm().ToString(),
                                         rac.GetIznos().ToString()
                                         );
                                            IspisiCrtu();
                                        }
                                    }
                                }
                            }
                        }
                        foreach (Lokacija lokacija in ite.GetLokacije())
                        {
                            IspisiRed(lokacija.GetNaziv(), " ", "", "", "", "", "", "", "", "", "");
                            IspisiCrtu();
                            foreach (var vozilo in Vozilo.SvaVozila)
                            {
                                IspisiRed(vozilo.GetNaziv(), " ", "", "", "", "", "", "", "", "", "");
                                IspisiCrtu();
                                foreach (var rac in Račun.DohvatiRacunePoLokacijiIVrstiVozila(vozilo, lokacija, pocetak, kraj))
                                {
                                    IspisiRed(rac.GetId().ToString(),
                                        rac.GetDatumIzdavanja().ToString(),
                                        rac.GetOsoba().GetImePrezime(),
                                        rac.GetLokacijaNajmaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaNajmaId()).GetNaziv(),
                                        rac.GetLokacijaVracanjaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaVracanjaId()).GetNaziv(),
                                        rac.GetNajam().ToString(),
                                        rac.GetPoSatu().ToString(),
                                        rac.GetPoKm().ToString(),
                                        rac.GetIznos().ToString()
                                        );
                                    IspisiCrtu();
                                }
                            }
                        }
                    }
                }
                foreach (Lokacija lokacija in item.GetLokacije())
                {
                    IspisiRed(lokacija.GetNaziv(), " ", " ", " ", "", "", "", "", "", "", "");
                    IspisiCrtu();
                    foreach (var vozilo in Vozilo.SvaVozila)
                    {
                        IspisiRed(vozilo.GetNaziv(), "", "", "", "", "", "", "", "", "", "");
                        IspisiCrtu();
                        foreach (var rac in Račun.DohvatiRacunePoLokacijiIVrstiVozila(vozilo, lokacija, pocetak, kraj))
                        {
                            IspisiRed(rac.GetId().ToString(),
                                        rac.GetDatumIzdavanja().ToString(),
                                        rac.GetOsoba().GetImePrezime(),
                                        rac.GetLokacijaNajmaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaNajmaId()).GetNaziv(),
                                        rac.GetLokacijaVracanjaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaVracanjaId()).GetNaziv(),
                                        rac.GetNajam().ToString(),
                                        rac.GetPoSatu().ToString(),
                                        rac.GetPoKm().ToString(),
                                        rac.GetIznos().ToString()
                                        );
                            IspisiCrtu();
                        }
                    }
                }
            }
            foreach (Lokacija lokacija in ishodisna.GetLokacije())
            {
                IspisiRed(lokacija.GetNaziv(), " ", " ", " ", "", "", "", "", "", "", "");
                IspisiCrtu();
                foreach (var vozilo in Vozilo.SvaVozila)
                {
                    IspisiRed(vozilo.GetNaziv(), "", "", "", "", "", "", "", "", "", "");
                    IspisiCrtu();
                    foreach (var rac in Račun.DohvatiRacunePoLokacijiIVrstiVozila(vozilo, lokacija, pocetak, kraj))
                    {
                        IspisiRed(rac.GetId().ToString(),
                                        rac.GetDatumIzdavanja().ToString(),
                                        rac.GetOsoba().GetImePrezime(),
                                        rac.GetLokacijaNajmaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaNajmaId()).GetNaziv(),
                                        rac.GetLokacijaVracanjaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaVracanjaId()).GetNaziv(),
                                        rac.GetNajam().ToString(),
                                        rac.GetPoSatu().ToString(),
                                        rac.GetPoKm().ToString(),
                                        rac.GetIznos().ToString()
                                        );
                        IspisiCrtu();
                    }
                }
            }
        }
        public static void IspisRacuni(OrganizacijskaJedinica ishodisna, DateTime pocetak, DateTime kraj)
        {
            IspisiCrtu();
            IspisiRed("ID računa", "Vrijeme izdavanja", "Osoba", "ID lokacije najma", "Lokacija najma", "ID lokacije vraćanja", "Lokacija vraćanja", "Najam", "Po satu", "Po km", "Iznos");
            IspisiCrtu();
            IspisiCrtu();
            foreach (OrganizacijskaJedinica item in ishodisna.Podređene)
            {
                if (item.Podređene.Count > 0)
                {
                    foreach (OrganizacijskaJedinica ite in item.Podređene)
                    {
                        if (ite.Podređene.Count > 0)
                        {
                            foreach (OrganizacijskaJedinica it in ite.Podređene)
                            {
                                foreach (Lokacija lokacija in it.GetLokacije())
                                {
                                    foreach (var vozilo in Vozilo.SvaVozila)
                                    {
                                        foreach (var rac in Račun.DohvatiRacunePoLokacijiIVrstiVozila(vozilo, lokacija, pocetak, kraj))
                                        {
                                            IspisiRed("         " + rac.GetId(),
                                         rac.GetDatumIzdavanja().ToString(),
                                         rac.GetOsoba().GetImePrezime(),
                                         rac.GetLokacijaNajmaId().ToString(),
                                         Lokacija.DohvatiLokaciju(rac.GetLokacijaNajmaId()).GetNaziv(),
                                         rac.GetLokacijaVracanjaId().ToString(),
                                         Lokacija.DohvatiLokaciju(rac.GetLokacijaVracanjaId()).GetNaziv(),
                                         rac.GetNajam().ToString(),
                                         rac.GetPoSatu().ToString(),
                                         rac.GetPoKm().ToString(),
                                         rac.GetIznos().ToString()
                                         );
                                            IspisiCrtu();
                                        }
                                    }
                                }
                            }
                        }
                        foreach (Lokacija lokacija in ite.GetLokacije())
                        {
                            foreach (var vozilo in Vozilo.SvaVozila)
                            {
                                foreach (var rac in Račun.DohvatiRacunePoLokacijiIVrstiVozila(vozilo, lokacija, pocetak, kraj))
                                {
                                    IspisiRed("         " + rac.GetId(),
                                        rac.GetDatumIzdavanja().ToString(),
                                        rac.GetOsoba().GetImePrezime(),
                                        rac.GetLokacijaNajmaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaNajmaId()).GetNaziv(),
                                        rac.GetLokacijaVracanjaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaVracanjaId()).GetNaziv(),
                                        rac.GetNajam().ToString(),
                                        rac.GetPoSatu().ToString(),
                                        rac.GetPoKm().ToString(),
                                        rac.GetIznos().ToString()
                                        );
                                    IspisiCrtu();
                                }
                            }
                        }
                    }
                }
                foreach (Lokacija lokacija in item.GetLokacije())
                {
                    foreach (var vozilo in Vozilo.SvaVozila)
                    {
                        foreach (var rac in Račun.DohvatiRacunePoLokacijiIVrstiVozila(vozilo, lokacija, pocetak, kraj))
                        {
                            IspisiRed("         " + rac.GetId(),
                                        rac.GetDatumIzdavanja().ToString(),
                                        rac.GetOsoba().GetImePrezime(),
                                        rac.GetLokacijaNajmaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaNajmaId()).GetNaziv(),
                                        rac.GetLokacijaVracanjaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaVracanjaId()).GetNaziv(),
                                        rac.GetNajam().ToString(),
                                        rac.GetPoSatu().ToString(),
                                        rac.GetPoKm().ToString(),
                                        rac.GetIznos().ToString()
                                        );
                            IspisiCrtu();
                        }
                    }
                }
            }
            foreach (Lokacija lokacija in ishodisna.GetLokacije())
            {
                foreach (var vozilo in Vozilo.SvaVozila)
                {
                    foreach (var rac in Račun.DohvatiRacunePoLokacijiIVrstiVozila(vozilo, lokacija, pocetak, kraj))
                    {
                        IspisiRed("         " + rac.GetId(),
                                        rac.GetDatumIzdavanja().ToString(),
                                        rac.GetOsoba().GetImePrezime(),
                                        rac.GetLokacijaNajmaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaNajmaId()).GetNaziv(),
                                        rac.GetLokacijaVracanjaId().ToString(),
                                        Lokacija.DohvatiLokaciju(rac.GetLokacijaVracanjaId()).GetNaziv(),
                                        rac.GetNajam().ToString(),
                                        rac.GetPoSatu().ToString(),
                                        rac.GetPoKm().ToString(),
                                        rac.GetIznos().ToString()
                                        );
                        IspisiCrtu();
                    }
                }
            }
        }
        public static void IspisFinancijskogStanjaKorisnika()
        {
            IspisiCrtu();
            IspisiRed("ID", "Ime i prezime", "Stanje dugovanja", "Zadnji najam");
            IspisiCrtu();
            foreach (var item in Osoba.SveOsobe)
            {
                if (item.GetZadnjiNajam().ToString()!= "01.01.0001. 00:00:00")
                {
                    IspisiRed(item.GetId().ToString(), item.GetImePrezime(), item.GetDugovanje().ToString(), item.GetZadnjiNajam().ToString());
                    IspisiCrtu();
                }
            }
        }
        public static void IspisRacunaKorisnika(Osoba korisnik, DateTime pocetak, DateTime kraj)
        {
            IspisiCrtu();
            IspisiRed("Broj racuna", "Iznos", "Datum", "Status", "Vrsta vozila", "Lokacija");
            IspisiCrtu();
            List<Račun> neplaceni = Račun.DohvatiNeplaćeneRačuneKorisnika(korisnik, pocetak, kraj);
            List<Račun> placeni = Račun.DohvatiPlaćeneRačuneKorisnika(korisnik, pocetak, kraj);
            foreach (var item in neplaceni)
            {
                string status = "placen";
                if (item.GetPlaćen()==false)
                {
                    status = "dug";
                }
                Lokacija lokacija = Lokacija.DohvatiLokaciju(item.GetLokacijaNajmaId());
                IspisiRed(item.GetId().ToString(), item.GetIznos().ToString(), item.GetDatumIzdavanja().ToString(), status, item.GetVozilo().GetNaziv(), lokacija.GetNaziv());
                IspisiCrtu();
            }
            foreach (var item in placeni)
            {
                string status = "placen";
                if (item.GetPlaćen() == false)
                {
                    status = "dug";
                }
                Lokacija lokacija = Lokacija.DohvatiLokaciju(item.GetLokacijaNajmaId());
                IspisiRed(item.GetId().ToString(), item.GetIznos().ToString(), item.GetDatumIzdavanja().ToString(), status, item.GetVozilo().GetNaziv(), lokacija.GetNaziv());
                IspisiCrtu();
            }

        }
    }
}
