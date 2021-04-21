using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaProduse.DelegateArgs
{
    public class PriceChangedArgs : EventArgs
    {
        public decimal PretVechi { get; set; }
        public decimal PretNou { get; set; }
    }
}
