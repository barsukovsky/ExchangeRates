using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

using NMoneys;
using NMoneys.Exchange;

using ExchangeRates;

namespace ExchangeRates.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Rates(string isoCode)
        {
            Currency to;
            if(!Currency.TryGet(isoCode.ToUpper(), out to))
            {
                return new HttpNotFoundResult();
            }

            IDictionary<Currency, Money> model = new Dictionary<Currency, Money>();
            
            model.Add(new KeyValuePair<Currency, Money>(
                    Currency.Get(CurrencyIsoCode.EUR),
                    new Money(1, CurrencyIsoCode.EUR).Convert().To(to)));
            model.Add(new KeyValuePair<Currency, Money>(
                    Currency.Get(CurrencyIsoCode.USD),
                    new Money(1, CurrencyIsoCode.USD).Convert().To(to)));
            model.Add(new KeyValuePair<Currency, Money>(
                    Currency.Get(CurrencyIsoCode.KGS),
                    new Money(1, CurrencyIsoCode.KGS).Convert().To(to)));

            return View(model);
        }
    }
}
