using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaProduse
{
    class Program
    {
        static void Main(string[] args)
        {
            var Producatori = new List<Producator>
            {
                new Producator
                {
                    Name = "Apple",
                    Reduceri = new List<Reducere>
                    {
                        new Reducere
                        {
                            Name = "Black Friday",
                            
                        },
                    }
                },
                new Producator
                {

                },
                new Producator
                {

                },

            };
        }
    }
}
