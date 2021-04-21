using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidentaProduse.DelegateArgs;

namespace EvidentaProduse
{
    public enum Moneda { LEU, EUR, USD }; // Enumeratia de monede

    public class Pret
    {
        public event EventHandler<PriceChangedArgs> PriceChanged; //Event pentru cand se schimba 

        public static Dictionary<Moneda, decimal> Curs = new Dictionary<Moneda, decimal>(); //cursul valutar
        public Moneda Moneda { get; set; }

        private decimal valoare;
        public decimal Valoare
        {
            get
            {
                return valoare;
            }
            set
            {
                OnPriceChanged(valoare,value); //aici activez eventul cand se schimba valoarea in proprietate
                valoare = value;
            }
        }

        private void OnPriceChanged(decimal pretVechi, decimal pretNou) //functie care aplica delegatul cand trebuie
        {
            var del = PriceChanged as EventHandler<PriceChangedArgs>;
            if(del != null)
            {
                del(this, new PriceChangedArgs { PretNou = pretNou, PretVechi = pretVechi });
            }
        }

        public static void InitializeazaCurs()
        {
            Auxiliare.CursLive.GetHtmlAsync(); //functie async care face rost de cursul valutar
        }

        public static void AfiseazaCurs()
        {
            Console.WriteLine($"Afisam Cursul BNR din data de {DateTime.Now}");
            Console.WriteLine($"1 LEU = {Curs[Moneda.LEU]} LEU");
            Console.WriteLine($"1 EUR = {Curs[Moneda.EUR]} LEU");
            Console.WriteLine($"1 USD = {Curs[Moneda.USD]} LEU\n");
        }

        public decimal ValoareCurs(Moneda moneda) // intoarce valoare in functie de curs pentru moneda primita
        {
            return Curs[moneda];
        }
    }
}
