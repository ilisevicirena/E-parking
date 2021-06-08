using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public class JedinicaIterator : ICollectionIterator
    {
        public List<OrganizacijskaJedinica> collection;
        public Iterator GetIterator(List<OrganizacijskaJedinica> jedinice)
        {
            collection = jedinice;
            return new OrganizacijskeJediniceIterator(collection);
        }

        private class OrganizacijskeJediniceIterator : Iterator
        {
            private List<OrganizacijskaJedinica> SveJedinice;
            private int current = 0;
            private List<int> ListaID;

            public OrganizacijskeJediniceIterator(List<OrganizacijskaJedinica> collection)
            {
                SveJedinice = collection;
                ListaID = new List<int>();
                PopuniIDje();
            }
            public object CurrentItem()
            {
                Jedinica jedinica = getObjectByIndex(current);
                return jedinica;
            }
            public object First()
            {
                Jedinica jedinica = getObjectByIndex(0);
                return jedinica;
            }
            public bool IsDone()
            {
                return current >= ListaID.Count;
            }
            public object Next()
            {
                current++;
                Jedinica trenutna = null;
                if (current < ListaID.Count)
                {
                    trenutna = getObjectByIndex(current);
                }
                return trenutna;
            }
            public void SetCurrent(int index)
            {
                if (index >= 0 && index < ListaID.Count)
                {
                    current = index;
                }
                else
                {
                    current = 0;
                }
            }
            public void PopuniIDje()
            {
                foreach (OrganizacijskaJedinica item in SveJedinice)
                {
                    ListaID.Add(item.GetId());
                }
                ListaID.Sort();
            }
            private OrganizacijskaJedinica getObjectByIndex(int index)
            {
                int id = ListaID.ElementAt(index);
                OrganizacijskaJedinica trenutna = null;
                foreach (OrganizacijskaJedinica item in SveJedinice)
                {
                    if (item.GetId() == id)
                    {
                        trenutna = item;
                        break;
                    }
                }
                return trenutna;
            }
            OrganizacijskaJedinica Iterator.GetObjectByIndex(int index)
            {
                int id = index;
                OrganizacijskaJedinica trenutna = null;
                foreach (OrganizacijskaJedinica item in SveJedinice)
                {
                    if (item.GetId() == id)
                    {
                        trenutna = item;
                        break;
                    }
                }
                return trenutna;
            }
        }
    }
}
