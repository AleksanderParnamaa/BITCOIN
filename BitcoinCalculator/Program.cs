using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitcoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinRates currentBitcoin = GetRates();
            Console.WriteLine($"current rate: {currentBitcoin.bpi.EUR.code}{currentBitcoin.bpi.EUR.rate_float}");
            //programm küsib, mitu bitcoini kasutajal on
            //programm küsib, mis valuutas (EUR/USD/)ta arvutab tulemuse
            //programm kuvab tuleuse konsoolis
            Console.WriteLine("Mitu bitcoini sul on?");
           string amount = Console.ReadLine();
            Console.WriteLine("EUR/USD/GBP");
            string userchoice = Console.ReadLine();
            float usercoins = float.Parse(Console.ReadLine());
            float currentRate = 0;
            if(userchoice == "EUR")
            {
                currentRate = currentBitcoin.bpi.EUR.rate_float;

            }
            if (userchoice == "USD")
            {
                currentRate = currentBitcoin.bpi.USD.rate_float;

            }
            if (userchoice == "GBP")
            {
                currentRate = currentBitcoin.bpi.GBP.rate_float;

            }

            float result = currentRate * usercoins;
            Console.WriteLine($"your bitcoins are worth {result} {userchoice}");
      



        }
        public static BitcoinRates GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();
            BitcoinRates bitcoindata;
            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoindata = JsonConvert.DeserializeObject<BitcoinRates>(response);
            }
            return bitcoindata;
        }
    }
}
