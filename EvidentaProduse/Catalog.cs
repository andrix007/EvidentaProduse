using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvidentaProduse.Auxiliare;

namespace EvidentaProduse
{
    public class Catalog : List<Produs>
    {
        //aici mai trebuie (si o sa o fac) adaugate doua DateTime uri, deoarce m-am prins acum care este scopul lor
        public DateTime? PerioadaStart { get; set; }
        public DateTime? PerioadaStop { get; set; }

        public List<Reducere> Reduceri { get; set; } //nu ar fi mai bine/eficient sa fie o singura lista/ un singur set static
        //in clasa Produs cu reduceri si apoi in fiecare loc unde e necesar sa fie un set/ o lista de indici care sa specifice ce reduceri
        // ar trebui aplicate?

        public void AfiseazaCatalog()
        {
            Console.WriteLine("Produse: \n");

            foreach(var produs in this)
            {
                Console.WriteLine(produs.ToString());
            }

        }

        public void Aboneaza(Client client) // metoda de aici cred (si sper ca e corecta) ca merge la abonare si foloseste si expresii lambda
                                            //problemele vin la dezabonare:
                                            /*
                                             * 1. Nu mai am functia in sine ca sa pot sa dau -= sa dezabonez de la produsele respective
                                             * 2. Nu stiu suficient de bine cum functioneaza dezabonarea la evenimente ca sa stiu ca
                                             * merge 100% sa sterg aceeasi functie, deoarece clientii sunt diferiti
                                             */
        {
            foreach(Produs produs in this)
            {
                foreach(Guid produsId in client.ProduseFavorite) //daca ar fi fost set acest foreach ar fi putut fi transformat intr-un 
                {
                    if(produsId == produs.Id)
                    {
                        produs.Pret.PriceChanged += (s, e) => 
                        { 
                            client.Notifica($"Pretul produsului <{produs.Name}> s-a schimbat de la" +
                                $"<{e.PretVechi}> <{client.Moneda}> la <{e.PretNou}><{client.Moneda}>"); 
                        };
                        break;
                    }
                }
            }
        }

        public void Dezaboneaza(Client client) //cum ar fi cel mai bine sa dezabonez? daca fac abonarea intr-un alt mod dezabonarea ar deveni mai usor de facut?
        {
            foreach (Produs produs in this)
            {
                foreach (Guid produsId in client.ProduseFavorite.OrderBy(p => p)) //aici am dat orderby pentru ca mai tarziu in proiect (posibil la final) sa implementez o cautare binara ca sa faca
                    //operatia in logn in loc de n (asta daca nu ajung sa folosesc colectia SortedSet)
                {
                    if (produsId == produs.Id)
                    {   
                        produs.Pret.PriceChanged -= (s, e) =>
                        {
                            client.Notifica($"Pretul produsului <{produs.Name}> s-a schimbat de la" +
                                $"<{e.PretVechi}> <{client.Moneda}> la <{e.PretNou}><{client.Moneda}>");
                        };
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
                        reducere.Aplica<Produs>(ref produs);
                    }
                }
                yield return produs;
            }
        }

        public void InitializeazaCatalog(List<Producator> Producatori)
        {
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
                Stoc = 3,
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
