using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3.controller
{
    public class Controller
    {
        private model.Model Model;
        private view.View View;

        public Controller(model.Model model, view.View view)
        {
            this.Model = model;
            this.View = view;
        }
        public Controller()
        {
            Model = new model.Model();
        }
        public int PokreniMVC(string[] args) 
        {
            int vrati = 0;
            if (Model.ParseArguments(args)==1)
            {
                if (Model.UcitajParametreIzDatKonfiguracije() == 1)
                {
                    View.Ispis(Model.GetPodaciZaIspis());
                    Model.EmptyData();
                    vrati = -1;
                }
                else
                {
                    if (Model.OdrediPocetniView()==1)
                    {
                        View = new view.ViewInteraktivniEkran();
                    }
                    else if (Model.OdrediPocetniView() == 2)
                    {
                        View = new view.ViewSkupniEkran();
                    }
                    else if (Model.OdrediPocetniView() == 3)
                    {
                        View = new view.ViewSkupniDatoteka();
                        Model.SetOutputStream();
                    }
                    else if (Model.OdrediPocetniView()==0)
                    {
                        return -1;
                    }
                    if (Provjera.ProvjeriUnesenoVrijeme() == 1)
                    {
                        model.Model.podaci.Add("Vrijeme nije ispravno uneseno");
                        View.Ispis(Model.GetPodaciZaIspis());
                        Model.EmptyData();
                        return -1;
                    }
                   
                    Model.UcitajDatoteke();
                    View.IspisGreskeIzDatoteka(Model.GetPodaciZaIspis());
                    Model.EmptyData();
                    
                    vrati = KreniSRadom();
                    View.Ispis(Model.GetPodaciZaIspis());
                    Model.EmptyData();
                    if (model.Model.streamWriter != null)
                    {
                        model.Model.streamWriter.Flush();
                        model.Model.streamWriter.Close();
                    }
                }
            }
            else if (Model.ParseArguments(args)==0)
            {
                View.Ispis(Model.GetPodaciZaIspis());
                Model.EmptyData();
                vrati = -1;
            }          
            return vrati;     
        }
        public int KreniSRadom()
        {
            int vrati = 0;
            if (NacinRada.Instance.SkupniNacinRada == true)
            {           
                try
                {
                    string[] aktivnosti = File.ReadAllLines(model.Model.DatAktivnosti);
                    aktivnosti = aktivnosti.Skip(1).ToArray();
                    foreach (var item in aktivnosti)
                    {
                        string[] polje = item.Split(';');
                        model.Model.podaci.Add(item);
                        vrati = model.Model.UtvrdiAktivnost(polje);
                        View.Ispis(Model.GetPodaciZaIspis());
                        Model.EmptyData();
                    }
                    if (vrati != -1)
                    {
                        if (model.Model.DatIzlaz!=" " && NacinRada.Instance.SkupniNacinRada)
                        {
                            if (model.Model.streamWriter != null)
                            {
                                model.Model.streamWriter.Flush();
                            }
                            return -1;
                        }
                        View = new view.ViewInteraktivniEkran();
                        NacinRada.Instance.InteraktivniNacinRada = true;
                        NacinRada.Instance.SkupniNacinRada = false;
                        int vratiti;
                        do
                        {
                            View.TraziUnos();
                            vratiti = model.Model.UcitajAktivnostInteraktivniNacinRada();
                            View.Ispis(Model.GetPodaciZaIspis());
                            Model.EmptyData();
                        } while (vratiti != -1);
                    }
               }
                catch (Exception)
                {
                    model.Model.podaci.Add("Greška: datoteka aktivnosti nije pronađena!");
                    vrati = -1;
                }
                return vrati;            
            }
            else
            {
               int vratiti;
                do
                {
                    View.TraziUnos();
                    vratiti = model.Model.UcitajAktivnostInteraktivniNacinRada();
                    View.Ispis(Model.GetPodaciZaIspis());
                    Model.EmptyData();
                } while (vratiti != -1);            
            }
            return vrati;
        }
        public int InteraktivniUSkupni()
        {
            int vrati=0;
            NacinRada.Instance.InteraktivniNacinRada = false;
            NacinRada.Instance.SkupniNacinRada = true;        
            try
            {
                string[] aktivnosti = File.ReadAllLines(model.Model.DatAktivnosti);
                aktivnosti = aktivnosti.Skip(1).ToArray();
                foreach (var item in aktivnosti)
                {
                    string[] polje = item.Split(';');
                    model.Model.podaci.Add(item);
                    vrati = model.Model.UtvrdiAktivnost(polje);
                    View.Ispis(Model.GetPodaciZaIspis());
                    Model.EmptyData();
                }
                if (vrati != -1)
                {
                    View = new view.ViewInteraktivniEkran();
                    NacinRada.Instance.InteraktivniNacinRada = true;
                    NacinRada.Instance.SkupniNacinRada = false;
                    int vratiti;
                    do
                    {
                        View.TraziUnos();
                        vratiti = model.Model.UcitajAktivnostInteraktivniNacinRada();
                        View.Ispis(Model.GetPodaciZaIspis());
                        Model.EmptyData();
                    } while (vratiti != -1);
                }
            }
            catch (Exception)
            {
                model.Model.podaci.Add("Greška: datoteka aktivnosti nije pronađena!");
                vrati = -1;
            }
            return vrati;
        }
    }
}
