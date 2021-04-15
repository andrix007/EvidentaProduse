using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaProduse
{
    public class Catalog : List<Produs>
    {
        public DateTime? PerioadaStart { get; set; }
        public DateTime? PerioadaStop { get; set; }
        public List<Reducere> Reduceri { get; set; }
    }
}
