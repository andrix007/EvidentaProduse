using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidentaProduse.Auxiliare;
using EvidentaProduse.DelegateArgs;

namespace EvidentaProduse
{
    public class Client
    { 
        private string[] Inbox = new string[50]; //am facut thresholdul 40 pentru ca altfel erau prea multe mesaje in inbox
        private int nrMesajeInbox = -1; 
        public string Email { get; set; }
        public Moneda Moneda { get; set; }
        public List<Guid> ProduseFavorite { get; set; }

        public bool Notifica(string mesaj) 
        {
            if (mesaj.Length > 100) //am schimbat la 100 ca 60 era prea mic
            {
                Console.WriteLine("Mesajul este prea mare!");
                return false;
            }
            if(nrMesajeInbox+1 > 50)
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
            for(int i = 0; i <= nrMesajeInbox; i++)
            {
                temp += $"{(i+1).ToRoman().ToString().ToLower().PadLeft(7)}. <{Inbox[i]}>\n";
            }

            mesaj += temp+"\n";

            return mesaj;
        }
    }

}
