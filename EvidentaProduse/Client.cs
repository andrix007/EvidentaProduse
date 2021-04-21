using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidentaProduse.Auxiliare;

namespace EvidentaProduse
{
    public class Client
    {
        private string[] Inbox = new string[10]; //de ce nu e lista Inboxul? ar fi avut aceeasi functionalitate si ar fi fost mai usor de folosit
        private int nrMesajeInbox = -1; //index pentru mesaje inbox
        public string Email { get; set; }
        public Moneda Moneda { get; set; }
        public List<Guid> ProduseFavorite { get; set; } //de ce nu este un set aceasta lista? ar fi fost mai usor cu gasirea elementelor in O(logn) in loc de 
        // O(n) ca la lista (ar ajuta banuiesc atunci cand trebuie sa abonam clienti / dezabonam clienti la un catalog)

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

        public override string ToString()
        {
            string mesaj = $"a. Email: <{Email}>\n" +
                "b. Produse: ";

            string temp = "";
            for(int i = 0; i < ProduseFavorite.Count; i++)
            {
                string produsI;
                Produs.NameForId.TryGetValue(ProduseFavorite[i], out produsI);

                temp += $"<{produsI}>";

                if (i != ProduseFavorite.Count - 1)
                {
                    temp += ", ";
                }
            }

            mesaj += temp + "\n";
            mesaj += "c. Inbox:\n";

            temp = "";
            for(int i = 1; i <= nrMesajeInbox; i++)
            {
                temp += $"{i.ToRoman().ToString().ToLower()}. <{Inbox[i]}>\n".PadLeft(15);
            }

            mesaj += temp+"\n";

            return mesaj;
        }
    }

}
