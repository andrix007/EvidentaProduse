using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaProduse
{
    public class Produs
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Pret Pret { get; set; }
        public uint Stoc { get; set; }
        public Producator Producator { get; set; }
}
