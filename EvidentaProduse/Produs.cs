using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaProduse
{
    public class Produs
    {
        public static Dictionary<Guid, string> NameForId = new Dictionary<Guid, string>();

        public Guid Id { get; set; }
        public Pret Pret { get; set; }
        public uint Stoc { get; set; }
        public Producator Producator { get; set; }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (NameForId.ContainsKey(Id))
                {
                    NameForId[Id] = value; ;
                }
                else
                {
                    NameForId.Add(Id, value);
                }
                name = value;
            }
        }

        private string name;

    }
}
