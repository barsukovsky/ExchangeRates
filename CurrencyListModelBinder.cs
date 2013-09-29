using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NMoneys;
using NMoneys.Exchange;

namespace ExchangeRates
{
    public class CurrencyListModelBinder : IModelBinder
    {
        private const string sessionKey = "CurrencyList";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = (List<CurrencyIsoCode>)controllerContext.HttpContext.Session[sessionKey];
            
            if (model == null)
            {
                model = new List<CurrencyIsoCode>();

                model.Add(CurrencyIsoCode.EUR);
                model.Add(CurrencyIsoCode.USD);
                model.Add(CurrencyIsoCode.KGS);

                controllerContext.HttpContext.Session[sessionKey] = model;
            }

            return model;
        }
    }
}