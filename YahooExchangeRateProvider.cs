using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Globalization;

using NMoneys;
using NMoneys.Exchange;

namespace ExchangeRates
{
    public class YahooExchangeRateProvider : IExchangeRateProvider
    {
        private string MakeUrl(CurrencyIsoCode from, CurrencyIsoCode to)
        {
            return "http://finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s="
                + Currency.Get(from).IsoSymbol
                + Currency.Get(to).IsoSymbol + "=X";
        }

        public ExchangeRate Get(CurrencyIsoCode from, CurrencyIsoCode to)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(MakeUrl(from, to));
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
                    }

                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string csv = reader.ReadToEnd();
                    string[] values = Regex.Split(csv, ",");
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim(new[] { '\"', '\n', '\r' });
                    }

                    decimal rate = Decimal.Parse(values[1], CultureInfo.InvariantCulture);
                    //DateTime date = DateTime.ParseExact(values[2], "M/dd/yyyy", CultureInfo.InvariantCulture);

                    return new ExchangeRate(from, to, rate);
                }
            }
            catch (Exception e)
            {
                return new ExchangeRate(from, to, 0);
            }
        }

        public bool TryGet(CurrencyIsoCode from, CurrencyIsoCode to, out ExchangeRate rate)
        {
            throw new NotImplementedException();
        }
    }
}