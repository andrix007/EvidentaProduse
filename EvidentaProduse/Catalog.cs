using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidentaProduse.Auxiliare;
using EvidentaProduse.DelegateArgs;

namespace EvidentaProduse
{
    public class Catalog : List<Produs>
    {

        public DateTime? PerioadaStart { get; set; }
        public DateTime? PerioadaStop { get; set; }

        public List<Reducere> Reduceri { get; set; }

        public static Dictionary<Client, Dictionary<Guid, EventHandler<PriceChangedArgs>>> AbonamenteClient = new Dictionary<Client, Dictionary<Guid, EventHandler<PriceChangedArgs>>>();
        public static Dictionary<Client, Dictionary<Guid, EventHandler<StockChangedArgs>>> StocuriProduseFavoriteClient = new Dictionary<Client, Dictionary<Guid, EventHandler<StockChangedArgs>>>();

        public void AfiseazaCatalog()
        {
            Console.WriteLine("Produse: \n");

            foreach(var produs in this)
            {
                Console.WriteLine(produs.ToString());
            }

        }

        private void NotificaSchimbareStoc(Client client, Produs produs)
        {
            bool ok = client.Notifica($"Produsul <{produs.Name}> este din nou in stoc!");
        }

        public void Aboneaza(Client client)
        {
            Dictionary<Guid, EventHandler<PriceChangedArgs>> DictionarProduseActiuni = new Dictionary<Guid, EventHandler<PriceChangedArgs>>();
            Dictionary<Guid, EventHandler<StockChangedArgs>> DictionarStocuriActiuni = new Dictionary<Guid, EventHandler<StockChangedArgs>>();

            foreach (Produs produs in this)
            {
                foreach(Guid produsId in client.ProduseFavorite) 
                {
                    if(produsId == produs.Id)
                    {
                        Action<object, PriceChangedArgs> a = (s, e) =>
                        {
                            bool ok = client.Notifica($"Pretul produsului <{produs.Name}> s-a schimbat de la " +
                                $"<{e.PretVechi}> <{client.Moneda}> la <{e.PretNou}> <{client.Moneda}>");
                        };

                        Action<object, StockChangedArgs> sa = (s,e) => NotificaSchimbareStoc(client, produs);

                        EventHandler<PriceChangedArgs> ea = new EventHandler<PriceChangedArgs>(a);
                        EventHandler<StockChangedArgs> esa = new EventHandler<StockChangedArgs>(sa);

                        produs.Pret.PriceChanged += ea;
                        produs.StockChanged += esa;

                        DictionarProduseActiuni.Add(produsId, ea);
                        DictionarStocuriActiuni.Add(produsId, esa);

                        break;
                    }
                }
            }

            AbonamenteClient.Add(client, DictionarProduseActiuni);
        }

        public void Dezaboneaza(Client client) 
        {
            if(!AbonamenteClient.ContainsKey(client))
            {
                throw new ArgumentException("Clientul nu este abonat!");
            }

            Dictionary<Guid, EventHandler<PriceChangedArgs>> DictionarProduseActiuni = AbonamenteClient[client];
            Dictionary<Guid, EventHandler<StockChangedArgs>> DictionarStocuriActiuni = StocuriProduseFavoriteClient[client];

            foreach (Produs produs in this)
            {
                foreach (Guid produsId in client.ProduseFavorite.OrderBy(p => p)) 
                {
                    if (produsId == produs.Id)
                    {
                        EventHandler<PriceChangedArgs> a = DictionarProduseActiuni[produsId];
                        EventHandler<StockChangedArgs> sa = DictionarStocuriActiuni[produsId];

                        produs.Pret.PriceChanged -= new EventHandler<PriceChangedArgs>(a);
                        produs.StockChanged -= new EventHandler<StockChangedArgs>(sa);

                        break;
                    }
                }
            }
        }

        private IEnumerable<Produs> AplicaReduceriProducator(Producator producator)
        {
            foreach(Produs produs in this)
            {
                foreach(Reducere reducere in producator.Reduceri)
                {
                    if(reducere.PerioadaStart.InRange(this.PerioadaStart,this.PerioadaStop) && reducere.PerioadaStop.InRange(this.PerioadaStart,this.PerioadaStop))
                    {
                        reducere.Aplica(produs);
                    }
                }
                yield return produs;
            }
        }

        public void AplicaReduceri()
        {
            foreach (Reducere reducere in this.Reduceri)
            {
                foreach (Produs produs in this)
                {
                    reducere.Aplica(produs);
                }
            }

            for (int i = this.Count - 1; i >= 0; i--)
            {
                Produs produs = this[i];
                AplicaReduceriProducator(produs.Producator);
            }

            foreach(Produs produs in this)
            {
                if(produs.Stoc == 0 && produs.Pret.ConvertToEuro() < 10m)
                {
                    produs.Stoc += 100;
                }
            }

        }

        public void AplicaReduceri(Action<Produs> a)
        {
            foreach(Produs produs in this)
            {
                a(produs);
            }
        }

        public void InitializeazaCatalog(List<Producator> Producatori)
        {
            PerioadaStart = DateTime.MinValue;
            PerioadaStop = DateTime.MaxValue;

            Reduceri = new List<Reducere>
                {
                    Producatori[0].Reduceri[0],
                    Producatori[0].Reduceri[1],
                    Producatori[0].Reduceri[2],
                    Producatori[1].Reduceri[0],
                    Producatori[1].Reduceri[1],
                    Producatori[1].Reduceri[2],
                    Producatori[2].Reduceri[0],
                    Producatori[2].Reduceri[1],
                    Producatori[2].Reduceri[2]
                };
            Add(new Produs
            {
                Id = Guid.NewGuid(),
                Name = "Apple apple",
                Pret = new Pret
                {
                    Moneda = Moneda.EUR,
                    Valoare = 10m
                },
                Stoc = 0,
                Producator = Producatori[0]
            });
            Add(new Produs
            {
                Id = Guid.NewGuid(),
                Name = "McChicken cu Ciocolata",
                Pret = new Pret
                {
                    Moneda = Moneda.LEU,
                    Valoare = 15m
                },
                Stoc = 1000000,
                Producator = Producatori[1]
            });
            Add(new Produs
            {
                Id = Guid.NewGuid(),
                Name = "Pizza cu gropite",
                Pret = new Pret
                {
                    Moneda = Moneda.USD,
                    Valoare = 30m
                },
                Stoc = 1,
                Producator = Producatori[2]
            });
        }
    }
}
