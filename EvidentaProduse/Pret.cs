using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaProduse
{
    public enum Moneda { LEU, EUR, USD }; 

    public class Pret
    {
        public delegate void PriceChangedDelegate(object sender, EventArgs args);
        public event PriceChangedDelegate PriceChanged;

        public static Dictionary<Moneda, decimal> Curs = new Dictionary<Moneda, decimal>();
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
                PriceChanged(this, new EventArgs());
                valoare = value;
            }
        }

        public static void InitializeazaCurs()
        {
            Auxiliare.CursLive.GetHtmlAsync();
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
