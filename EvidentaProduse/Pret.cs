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
        public static Dictionary<Moneda, decimal> Curs = new Dictionary<Moneda, decimal>()
        {
            /*{Moneda.LEU, 1m},
            {Moneda.EUR, },
            {Moneda.USD, }*/
        };
        public Moneda Moneda { get; set; }
        
        public decimal ValoareCurs(Moneda moneda) // intoarce valoare in functie de curs pentru moneda primita
        {
            return Curs[moneda];
        }
    }
}
