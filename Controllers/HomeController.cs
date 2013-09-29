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
        public ActionResult Rates(string isoCode, List<CurrencyIsoCode> currencies)
        {
            Currency to;
            if(!Currency.TryGet(isoCode.ToUpper(), out to))
            {
                return new HttpNotFoundResult();
            }

            IDictionary<Currency, Money> model = new Dictionary<Currency, Money>();

            if (currencies == null)
            {
                return View(model);
            }
            foreach (var currency in currencies)
            {
                model.Add(new KeyValuePair<Currency, Money>(
                    Currency.Get(currency),
                    new Money(1, currency).Convert().To(to)));
            }

            return View(model);
        }

        public ActionResult Add(string code, List<CurrencyIsoCode> currencies)
        {
            Currency currency;
            if (Currency.TryGet(code.ToUpper(), out currency) && !currencies.Contains(currency.IsoCode))
            {
                currencies.Add(currency.IsoCode);
            }
            
            return RedirectToAction("Rates", RouteData.Values["isocode"]);
        }

        public ActionResult Delete(string code, List<CurrencyIsoCode> currencies)
        {
            Currency currency;
            if (Currency.TryGet(code.ToUpper(), out currency))
            {
                currencies.Remove(currency.IsoCode);
            }

            return RedirectToAction("Rates", RouteData.Values["isocode"]);
        }
    }
}
