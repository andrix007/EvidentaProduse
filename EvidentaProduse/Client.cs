using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaProduse
{
    public class Client
    {
        private string[] Inbox = new string[10];
        private static int nrMesajeInbox = -1;
        public string Email { get; set; }
        public Moneda Moneda { get; set; }
        public List<Guid> ProduseFavorite { get; set; }

        public bool Notifica(string mesaj)
        {

            if (mesaj.Length > 60)
            {
                return false;
            }
            if(nrMesajeInbox+1 > 10)
            {
                throw new OutOfMemoryException("Prea multe mesaje in Inbox!!!");
            }
            Inbox[++nrMesajeInbox] = mesaj;
            return true;
        }
    }
}
