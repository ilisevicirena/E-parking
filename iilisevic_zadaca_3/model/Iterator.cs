using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public interface Iterator
    {
        object First();
        object Next();
        bool IsDone();
        object CurrentItem();
        void SetCurrent(int index);
        OrganizacijskaJedinica GetObjectByIndex(int index);
    }
}
