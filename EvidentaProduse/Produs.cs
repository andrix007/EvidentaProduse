using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidentaProduse.DelegateArgs;

namespace EvidentaProduse
{
    public class Produs
    {
        public event EventHandler<StockChangedArgs> StockChanged; // Cred ca aici sta cel mai bine eventul de stoc schimbat, desi in pdf scrie clasa Pret

        public static Dictionary<Guid, string> NameForId = new Dictionary<Guid, string>(); // Dictionar ca sa vad numele unui produs pentru un Guid

        public Guid Id { get; set; }
        public Pret Pret { get; set; }
        public Producator Producator { get; set; } 

        public string Name
        {
            get
            {
                return name;
            }
            set
            { //setul se face normal doar ca adaug valoarea in dictionarul NameForId direct din proprietate pentru usurinta
                if (NameForId.ContainsKey(Id)) 
                {
                    NameForId[Id] = value; 
                }
                else
                {
                    NameForId.Add(Id, value);
                }
                name = value;
            }
        }

        public uint Stoc
        {
            get
            {
                return stoc;
            }
            set
            {
                if (stoc == 0 && value > 0) //doar daca se schimba produsul
                {
                    OnStockChanged(stoc, value); //anunta cand se schimba stocul din proprietate
                }
                stoc = value;
            }
        }

        private string name;
        private uint stoc=0;

        public void OnStockChanged(uint stocVechi, uint stocNou) //asta cred ca ar trebui legata doar la produsele de care e interesat clientul
        {
            var del = StockChanged as EventHandler<StockChangedArgs>;
            if(del != null)
            {
                del(this, new StockChangedArgs{ StocNou = stocNou, StocVechi = stocVechi});
            }
        }

        public override string ToString()
        {
            string mesaj = "";

            mesaj += $"Nume: <{Name}>\n" +
                $"ID: <{Id.ToString()}>\n" +
                $"Producator: <{Producator.Name}>\n" +
                $"Stoc: <{Stoc}>\n";

            return mesaj;
        }

    }
}
