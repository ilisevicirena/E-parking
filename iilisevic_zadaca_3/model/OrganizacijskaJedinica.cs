using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class OrganizacijskaJedinica : Jedinica
    {

        private int Id;
        private string Naziv;
        private OrganizacijskaJedinica NadredjenaJedinica;
        private List<Lokacija> Lokacije;
        public List<Jedinica> Podređene;
        public List<Jedinica> SvaDjeca;
        public List<KeyValuePair<Vozilo, int>> BrojMjesta;
        public List<KeyValuePair<Vozilo, int>> BrojVozila;
        public List<KeyValuePair<Vozilo, int>> BrojNeispravnihVozila;

        public OrganizacijskaJedinica()
        {
            Podređene = new List<Jedinica>();
            BrojMjesta = new List<KeyValuePair<Vozilo, int>>();
            BrojVozila = new List<KeyValuePair<Vozilo, int>>();
            BrojNeispravnihVozila = new List<KeyValuePair<Vozilo, int>>();
        }
        public override void Add(Jedinica jedinica)
        {
            this.Podređene.Add(jedinica);
        }
        public override void Remove(Jedinica jedinica)
        {
            this.Podređene.Remove(jedinica);
        }
        public void SetId(int id)
        {
            Id = id;
        }
        public void SetNaziv(string naziv)
        {
            Naziv = naziv;
        }
        public void SetNadredjenaJedinica(OrganizacijskaJedinica jedinica)
        {
            NadredjenaJedinica = jedinica;
        }
        public void SetLokacije(List<Lokacija> lokacije)
        {
            Lokacije = lokacije;
        }
        public int GetId() { return Id; }
        public string GetNaziv() { return Naziv; }
        public OrganizacijskaJedinica GetNadredjenaJedinica() { return NadredjenaJedinica; }
        public List<Lokacija> GetLokacije() { return Lokacije; }

        public static void PopuniStupce(OrganizacijskaJedinica ishodisna, List<string> stupci, List<IspisRed> rezultat)
        {
            if (ishodisna.Podređene.Count == 0)
            {
                foreach (Lokacija item in ishodisna.GetLokacije())
                {
                    IspisRed ispis = new IspisRed();
                    ispis.Stupac.AddRange(stupci);
                    ispis.Stupac.Add(item.GetNaziv());
                    rezultat.Add(ispis);
                }
                stupci = new List<string>();
                return;
            }
            foreach (OrganizacijskaJedinica item in ishodisna.Podređene)
            {
                stupci.Add(item.GetNaziv());
                PopuniStupce(item, stupci, rezultat);
                stupci = new List<string>();
            }
        }
        public override void IzracunajKumulativ(OrganizacijskaJedinica jedinica)
        {
            List<KeyValuePair<Vozilo, int>> svaMjesta = new List<KeyValuePair<Vozilo, int>>();
            List<KeyValuePair<Vozilo, int>> svaVozila = new List<KeyValuePair<Vozilo, int>>();
            List<KeyValuePair<Vozilo, int>> svaNeispravnaVozila = new List<KeyValuePair<Vozilo, int>>();

            foreach (Lokacija item in jedinica.Lokacije)
            {
                foreach (var vozilo in Vozilo.SvaVozila)
                {
                    svaMjesta.Add(new KeyValuePair<Vozilo, int>(vozilo, Kapacitet.BrojRaspolozivihMjesta(item, vozilo)));
                    svaVozila.Add(new KeyValuePair<Vozilo, int>(vozilo, Kapacitet.BrojRaspolozivihVrstaVozila(item, vozilo, DateTime.Now)));
                    svaNeispravnaVozila.Add(new KeyValuePair<Vozilo, int>(vozilo, Neispravno.BrojNeispravnihVozilaPoVrsti(vozilo, item)));
                }
            }
            foreach (var vozilo in Vozilo.SvaVozila)
            {
                int sumaMjesta = 0;
                int sumaVozila = 0;
                int sumaPokvarenih = 0;
                foreach (var item in svaMjesta)
                {
                    if (item.Key == vozilo)
                    {
                        sumaMjesta += item.Value;
                    }
                }
                foreach (var item in svaVozila)
                {
                    if (item.Key == vozilo)
                    {
                        sumaVozila += item.Value;
                    }
                }
                foreach (var item in svaNeispravnaVozila)
                {
                    if (item.Key == vozilo)
                    {
                        sumaPokvarenih += item.Value;
                    }
                }
                jedinica.BrojMjesta.Add(new KeyValuePair<Vozilo, int>(vozilo, sumaMjesta));
                jedinica.BrojVozila.Add(new KeyValuePair<Vozilo, int>(vozilo, sumaVozila));
                jedinica.BrojNeispravnihVozila.Add(new KeyValuePair<Vozilo, int>(vozilo, sumaPokvarenih));
            }
        }
        public override string Operacija()
        {
            int i = 0;
            string rezultat = "Grana(";
            foreach (Jedinica jedinica in this.Podređene)
            {
                rezultat += jedinica.Operacija();
                if (i != this.Podređene.Count - 1)
                {
                    rezultat += "+";
                }
                i++;
            }
            return rezultat + ")";
        }
    }

    public class IspisRed
    {
        public List<string> Stupac;
        public IspisRed()
        {
            Stupac = new List<string>();
        }
    }
}
