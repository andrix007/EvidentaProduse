using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaProduse.DelegateArgs
{
    public class StockChangedArgs : EventArgs
    {
        public uint StocVechi { get; set; }
        public uint StocNou { get; set; }
    }
}
