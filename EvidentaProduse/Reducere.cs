using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaProduse
{
    public class Reducere
    {
        public string Name { get; set; }
        public DateTime Data { get; set; }
        public delegate void Aplica<T>(Produs p);
    }
}
