using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public interface ICollectionIterator
    {
        Iterator GetIterator(List<OrganizacijskaJedinica> jedinice);
    }

}
