using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace EvidentaProduse
{
    class Program
    {
        static void Main(string[] args)
        {
            //Prin "set" mai incolo in comentarii ma refer la colectia SortedSet

            Pret.InitializeazaCurs(); //functie care initializeaza cursul monetar dupa site-ul BNR
            // de specificat ca este un proces async, chestie pe care nu am invatat-o temeinic si ar trebui lasata 0.5 secunde ca sa le ia de pe site

            Console.ReadKey();
            Console.Clear();

            Pret.AfiseazaCurs(); //afisez cursul valutar sa vad sa fie in regula

            var Producatori = new List<Producator>
            {
                new Producator
                {
                    Name = "Apple",
                    Reduceri = new List<Reducere>
                    {
                        new Reducere
                        {
                            Name = "iBlack Friday",
                            PerioadaStart = new DateTime(2021, 11, 5,0,0,0,DateTimeKind.Local).ToUniversalTime(),
                            PerioadaStop = new DateTime(2021, 11, 6,0,0,0,DateTimeKind.Local).ToUniversalTime()
                        },
                        new Reducere
                        {
                            Name = "iValentines Day",
                            PerioadaStart = new DateTime(2022, 2, 11,0,0,0,DateTimeKind.Local).ToUniversalTime()
                        },
                        new Reducere
                        {
                            Name = "iHelloween",
                            PerioadaStop = new DateTime(2021, 10, 28,0,0,0,DateTimeKind.Local).ToUniversalTime()
                        }
                    }
                },
                new Producator
                {
                    Name = "McDonalds",
                    Reduceri = new List<Reducere>
                    {
                        new Reducere
                        {
                            Name = "McBlack",
                            PerioadaStart = new DateTime(2021, 11, 5,0,0,0,DateTimeKind.Local).ToUniversalTime(),
                            PerioadaStop = new DateTime(2021, 11, 6,0,0,0,DateTimeKind.Local).ToUniversalTime()
                        },
                        new Reducere
                        {
                            Name = "McLove",
                            PerioadaStart = new DateTime(2022, 2, 11,0,0,0,DateTimeKind.Local).ToUniversalTime()
                        },
                        new Reducere
                        {
                            Name = "McWeen",
                            PerioadaStop = new DateTime(2021, 10, 28,0,0,0,DateTimeKind.Local).ToUniversalTime()
                        }
                    }
                },
                new Producator
                {
                    Name = "Pizza Hut",
                    Reduceri = new List<Reducere>
                    {
                        new Reducere
                        {
                            Name = "Never try to outpizza the hut again!",
                            PerioadaStart = new DateTime(2021, 11, 5,0,0,0,DateTimeKind.Local).ToUniversalTime(),
                            PerioadaStop = new DateTime(2021, 11, 6,0,0,0,DateTimeKind.Local).ToUniversalTime()
                        },
                        new Reducere
                        {
                            Name = "Double pizza for the single people out there",
                            PerioadaStart = new DateTime(2022, 2, 11,0,0,0,DateTimeKind.Local).ToUniversalTime()
                        },
                        new Reducere
                        {
                            Name = "Spoooky pizza with pumpkin on sale",
                            PerioadaStop = new DateTime(2021, 10, 28,0,0,0,DateTimeKind.Local).ToUniversalTime()
                        }
                    }
                },

            };

            Catalog catalog = new Catalog();

            catalog.InitializeazaCatalog(Producatori); //Initializez catalogul (am facut o functie ca sa fie mai normal codul si am
            //nevoie de lista de producatori pentru ca nu vreau sa mai creez altii noi

            catalog.AfiseazaCatalog(); //afisez sa vad ca e in regula

            var Clienti = new List<Client>()
            {
                new Client
                {
                    Email = "ardut2004@gmail.com",
                    Moneda = Moneda.LEU,
                    ProduseFavorite = new List<Guid>()
                    {
                        catalog[0].Id,
                        catalog[2].Id
                    }
                },
                new Client
                {
                    Email = "andreirbancila@gmail.com",
                    Moneda = Moneda.EUR,
                    ProduseFavorite = new List<Guid>()
                    {
                        catalog[1].Id,
                        catalog[2].Id
                    }
                },
                new Client
                {
                    Email = "bigbirdsmurderer@yahoo.com",
                    Moneda = Moneda.USD,
                    ProduseFavorite = new List<Guid>()
                    {
                        catalog[0].Id,
                        catalog[1].Id
                    }
                }
            }; //instantiez o lista de clienti

            foreach(Client client in Clienti)
            {
                Console.WriteLine(client.ToString()); //afisez clientii dupa descrierea de la final din pdf cu functie suprascrisa ToString()
            }

        }
    }
}
