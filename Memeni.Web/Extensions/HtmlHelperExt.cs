using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web
{
    public static class HtmlHelperExt
    {
        private static JsonSerializerSettings _settings = null;

        static HtmlHelperExt()
        {
            _settings = new JsonSerializerSettings();
            _settings.Converters.Add(new KeyValuePairConverter());

            _settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public static IHtmlString RawJson(this HtmlHelper helper, object model)
        {
            string rValue = null;

            rValue = JsonConvert.SerializeObject(model, _settings);
            return helper.Raw(rValue);;
        }

        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

    }
}
