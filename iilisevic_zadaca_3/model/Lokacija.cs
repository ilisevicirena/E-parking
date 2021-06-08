using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Lokacija : Jedinica
    {
        private int Id;
        private string Naziv;
        private string Adresa;
        private string Gps;
        public static List<Lokacija> SveLokacije = new List<Lokacija>();
        public Lokacija()
        {

        }
        public Lokacija(int id, string naziv, string adresa, string gps)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.Adresa = adresa;
            this.Gps = gps;
            SveLokacije.Add(this);
        }

        public int GetId() { return Id; }
        public string GetNaziv() { return Naziv; }
        public string GetAdresa() { return Adresa; }
        public string GetGps() { return Gps; }

        public void SetId(int id)
        {
            Id = id;
        }
        public void SetNaziv(string naziv)
        {
            Naziv = naziv;
        }
        public void SetAdresa(string adresa)
        {
            Adresa = adresa;
        }
        public void SetGps(string gps)
        {
            Gps = gps;
        }

        public static Lokacija DohvatiLokaciju(int id)
        {
            foreach (var item in SveLokacije)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }
        public static Lokacija DohvatiLokacijuPoImenu(string ime)
        {
            foreach (var item in SveLokacije)
            {
                if (item.Naziv == ime)
                {
                    return item;
                }
            }
            return null;
        }
        public override string Operacija()
        {
            return "List";
        }
        public override bool IsComposite()
        {
            return false;
        }
        public override void IzracunajKumulativ(OrganizacijskaJedinica jedinica)
        {

        }
    }
}
