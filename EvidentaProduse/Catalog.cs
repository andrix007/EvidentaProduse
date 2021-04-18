using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaProduse
{
    public class Catalog : List<Produs>
    {
        public List<Reducere> Reduceri { get; set; }

        public void AfiseazaCatalog()
        {
            Console.WriteLine("Produse: \n");

            foreach(var produs in this)
            {
                Console.WriteLine(produs.ToString());
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
