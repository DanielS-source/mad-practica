using System.Collections;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Http.View
{
    public static class Countries
    {
        private static readonly Hashtable english = new Hashtable() { { "ES", "Spain" }, { "US", "United States" } };
        private static readonly Hashtable spanish = new Hashtable() { { "ES", "España" }, { "US", "Estados Unidos" } };

        private static readonly ArrayList countries_en = new ArrayList();
        private static readonly ArrayList countries_es = new ArrayList();
        private static readonly Hashtable countries = new Hashtable();

        static Countries()
        {
            countries_en.Add(new ListItem((string)english["ES"], "ES"));
            countries_en.Add(new ListItem((string)english["US"], "US"));
            countries.Add("en", countries_en);

            countries_es.Add(new ListItem((string)spanish["ES"], "ES"));
            countries_es.Add(new ListItem((string)spanish["US"], "US"));
            countries.Add("es", countries_es);
        }

        public static string GetCountry(string languageCode, string countryCode)
        {
            switch (languageCode)
            {
                case "en":
                    return english[countryCode] is null ? "United States" : (string)english[countryCode];

                case "es":
                    return spanish[countryCode] is null ? "United States" : (string)spanish[countryCode];

                default:
                    return "United States";
            }
        }

        public static ArrayList GetCountries(string languageCode)
        {
            ArrayList language = (ArrayList)countries[languageCode];

            return language is null ? (ArrayList)countries["en"] : language;
        }
    }
}