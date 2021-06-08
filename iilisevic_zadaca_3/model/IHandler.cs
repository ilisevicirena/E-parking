using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3.model
{
    public interface IHandler
    {
        void handleRequest(object request);
    }
}
