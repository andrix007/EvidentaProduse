using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EvidentaProduse.Auxiliare
{
    public class CursLive
    {
            public static void WriteSpans()
            {
                string fullUrl = "https://www.google.com/search?q=1+ron+to+usd&rlz=1C1CHWL_roRO937RO937&oq=1+ron+to+usd&aqs=chrome.0.0l2j0i22i30l2j0i10i22i30j0i22i30j69i60l2.4018j1j1&sourceid=chrome&ie=UTF-8";
                List<string> programmerLinks = new List<string>();

                var options = new ChromeOptions()
                {
                    BinaryLocation = @"D:\Internship\EvidentaProduse\EvidentaProduse\Resurse\chromedriver.exe"
                };

                options.AddArguments(new List<string>() { "headless", "disable-gpu" });
                var browser = new ChromeDriver(options);
                browser.Navigate().GoToUrl(fullUrl);
                var spans = browser.FindElementsByTagName("span");

                foreach (var span in spans)
                {
                    Console.WriteLine(span.Text);
                }

            }

    }
}
