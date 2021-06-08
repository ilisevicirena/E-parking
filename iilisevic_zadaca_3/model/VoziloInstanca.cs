using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class VoziloInstanca : VrstaVozila
    {
        private int StanjeBaterije;
        private Lokacija Lokacija;
        private DateTime ZadnjeVracanje;
        private bool Posudjeno;
        private int Registracija;
        private int BrojNajma;
        private int BrojKm;
        private bool Neispravno;
        private bool NaPunjenju;

        public static List<VoziloInstanca> SveInstanceVozila = new List<VoziloInstanca>();

        public VoziloInstanca()
        {
        }
        public override object Clone()
        {
            VoziloInstanca kopija = new VoziloInstanca
            {
                ID = this.ID,
                Lokacija = this.Lokacija,
                VrijemePunjenja = this.VrijemePunjenja,
                ZadnjeVracanje = this.ZadnjeVracanje,
                Naziv = this.Naziv,
                Domet = this.Domet,
                Posudjeno = this.Posudjeno,
                StanjeBaterije = this.StanjeBaterije,
                BrojNajma = this.BrojNajma,
                BrojKm = this.BrojKm,
                Registracija = this.Registracija,
                Neispravno = this.Neispravno,
                NaPunjenju=this.NaPunjenju
            };

            SveInstanceVozila.Add(kopija);
            return kopija;
        }
        public void SetStanjeBaterije(int stanje)
        {
            StanjeBaterije = stanje;
        }
        public void SetLokacija(Lokacija lokacija)
        {
            Lokacija = lokacija;
        }
        public void SetZadnjeVracanje(DateTime datum)
        {
            ZadnjeVracanje = datum;
        }
        public void SetPosudjeno(bool status)
        {
            Posudjeno = status;
        }
        public void SetRegistracija(int registracija)
        {
            Registracija = registracija;
        }
        public void SetBrojNajma(int brojNajma)
        {
            BrojNajma = brojNajma;
        }
        public void SetBrojKm(int brojKm)
        {
            BrojKm = brojKm;
        }
        public void SetNeispravno(bool neispravno)
        {
            Neispravno = neispravno;
        }
        public void SetNaPunjenju(bool naPunjenju)
        {
            NaPunjenju = naPunjenju;
        }

        public Lokacija GetLokacija() { return Lokacija; }
        public int GetId() { return ID; }
        public int GetVrijemePunjenja() { return VrijemePunjenja; }
        public DateTime GetZadnjeVracanje() { return ZadnjeVracanje; }
        public string GetNaziv() { return Naziv; }
        public int GetDomet() { return Domet; }
        public bool GetPosudjeno() { return Posudjeno; }
        public int GetStanjeBaterije() { return StanjeBaterije; }
        public int GetRegistracija() { return Registracija; }
        public int GetBrojNajma() { return BrojNajma; }
        public int GetBrojKm() { return BrojKm; }
        public bool GetNeispravno() { return Neispravno; }
        public bool GetNaPunjenju() { return NaPunjenju; }

        public static VoziloInstanca DohvatiVoziloTipa(Vozilo vozilo, bool posudjeno)
        {
            foreach (var item in SveInstanceVozila)
            {
                if (item.ID == vozilo.GetId() && item.StanjeBaterije == 100 && item.Posudjeno == posudjeno)
                {
                    return item;
                }
            }
            return null;
        }    
    }
}
