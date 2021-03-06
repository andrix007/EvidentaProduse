using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using HtmlAgilityPack; //Trebuie instalat pachetul HtmlAgilityPack ca sa mearga (am cautat sa fac si cu selenium / alte metode doar ca nu stiu inca destul de bine 
//async-ul si alte tehnologi de requesturi / api-uri ca aparent e mai explicit si mai greu (dar mai interesant) decat in python
using System.Net.Http;
using System.Threading.Tasks;

namespace EvidentaProduse.Auxiliare
{
    public static class CursLive
    {
        public static decimal GetBNRCurs(string s, Moneda moneda) //parseaza continutul tabelului care arata cursul valutar pentru euro si dolari
        {
            decimal Curs;

            string temp = (moneda == Moneda.EUR) ? "EUR" : "USD";

            string nr = "";
            int cnt = 0;

            for(int i = s.IndexOf(temp)+3; ++cnt <= 5 ; i++)
            {
                nr += s[i];
            }

            Curs = Decimal.Parse(nr);

            return Curs;
        }

        public static async void GetHtmlAsync() // initializeaza la cursul BNR din momentul rularii programului
        {
            Console.WriteLine("Se initializeaza cursul valutar...");

            string url = @"https://www.bnr.ro/Home.aspx";

            HttpClient httpClient = new HttpClient();
            string html = await httpClient.GetStringAsync(url);

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            string InformatiiTabelCursValutarBNR = htmlDocument.DocumentNode.Descendants("table").ToList()[0].InnerText;

            decimal EUR_TO_LEI = 4.9m; //am valori normale aici in caz ca nu merge sa iau cursul de pe site
            decimal USD_TO_LEI = 4.1m;

            Pret.Curs[Moneda.LEU] = 1m;

            try
            {
                Pret.Curs[Moneda.EUR] = GetBNRCurs(InformatiiTabelCursValutarBNR, Moneda.EUR);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Pret.Curs[Moneda.EUR] = EUR_TO_LEI;
            }

            try
            {
                Pret.Curs[Moneda.USD] = GetBNRCurs(InformatiiTabelCursValutarBNR, Moneda.USD);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Pret.Curs[Moneda.USD] = USD_TO_LEI;
            }

            Console.WriteLine("Cursul a fost initializat!\n Apasa orice tasta pentru a continua");

        }
    }
}
