using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3
{
    public interface IStanje
    {
        bool GetStanje(VoziloInstanca instanca);
        void SetStanje(VoziloInstanca instanca, bool stanje);
    }
}
