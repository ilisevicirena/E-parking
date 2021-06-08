using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class Vozilo
    {
        private int Id;
        private string Naziv;
        private int VrijemePunjenja;
        private int Domet;

        public static List<Vozilo> SvaVozila = new List<Vozilo>();

        public Vozilo(int id, string naziv, int vrijemePunjenja, int domet)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.VrijemePunjenja = vrijemePunjenja;
            this.Domet = domet;

            SvaVozila.Add(this);
        }

        public int GetId() { return Id; }
        public string GetNaziv() { return Naziv; }
        public int GetVrijemePunjenja() { return VrijemePunjenja; }
        public int GetDomet() { return Domet; }

        public void SetId(int id)
        {
            Id = id;
        }
        public void SetNaziv(string naziv)
        {
            Naziv = naziv;
        }
        public void SetVrijemePunjenja(int vrijeme)
        {
            VrijemePunjenja = vrijeme;
        }
        public void SetDomet(int domet)
        {
            Domet = domet;
        }

        public static Vozilo DohvatiVozilo(int id)
        {
            foreach (var item in SvaVozila)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
