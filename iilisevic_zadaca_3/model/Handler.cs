using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iilisevic_zadaca_3.model
{
    public class Handler : IHandler
    {
        private IHandler handler = null;

        public IHandler GetHandler()
        {
            return handler;
        }
        public void SetHandler(IHandler handler)
        {
            this.handler = handler;
        }
       
        public void handleRequest(object request)
        {
            handler.handleRequest(request);
        }
    }
}
