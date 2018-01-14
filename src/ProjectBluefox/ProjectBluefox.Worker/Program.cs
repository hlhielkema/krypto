using Newtonsoft.Json;
using ProjectBluefox.Data.Models;
using ProjectBluefox.Database.Managers;
using ProjectBluefox.Database.Util;
using ProjectBluefox.Worker.CoinMarketCapSync;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBluefox.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the database connection string
            MSSqlConnection.DatabaseConnectionString = ConfigurationManager.AppSettings.Get("DatabaseConnectionString");

            // Update the value rates
            UpdateRates();
        }

        private static void UpdateRates()
        {
            string json = "";
            string url = @"https://api.coinmarketcap.com/v1/ticker/?convert=EUR";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            var rates = JsonConvert.DeserializeObject<List<CmcCoinRates>>(json);

            List<CurrencyValueRates> models = rates.Select(x => x.ToModel()).ToList();

            CurrenciesManager.UpdateRates(models);
        }
    }
}
