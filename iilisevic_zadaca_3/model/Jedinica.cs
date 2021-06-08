using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public abstract class Jedinica
    {
        public Jedinica()
        {

        }
        public void TemplateMethod(Jedinica jedinica, bool add, bool remove)
        {
            this.Operacija();
            this.IzracunajKumulativ((OrganizacijskaJedinica)jedinica);
            if (add)
            {
                this.Add(jedinica);
            }
            if (remove)
            {
                this.Remove(jedinica);
            }
            this.IsComposite();
        }
        public abstract string Operacija();
        public abstract void IzracunajKumulativ(OrganizacijskaJedinica jedinica);
        public virtual void Add(Jedinica jedinica)
        {

        }
        public virtual void Remove(Jedinica jedinica)
        {

        }
        public virtual bool IsComposite()
        {
            return true;
        }
    }
}
