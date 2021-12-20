using System.Collections;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Http.View
{
    public static class Languages
    {
        private static readonly Hashtable english = new Hashtable() { { "es", "Spanish" }, { "en", "English" } };
        private static readonly Hashtable spanish = new Hashtable() { { "es", "Español" }, { "en", "Inglés" } };

        private static readonly ArrayList languages_en = new ArrayList();
        private static readonly ArrayList languages_es = new ArrayList();
        private static readonly Hashtable languages = new Hashtable();

        static Languages()
        {
            languages_en.Add(new ListItem((string)english["en"], "en"));
            languages_en.Add(new ListItem((string)english["es"], "es"));
            languages.Add("en", languages_en);

            languages_es.Add(new ListItem((string)spanish["en"], "en"));
            languages_es.Add(new ListItem((string)spanish["es"], "es"));
            languages.Add("es", languages_es);
        }

        public static string GetLanguage(string languageCode)
        {
            switch (languageCode)
            {
                case "en":
                    return english[languageCode] is null ? "English" : (string)english[languageCode];

                case "es":
                    return spanish[languageCode] is null ? "English" : (string)spanish[languageCode];

                default:
                    return "English";
            }
        }

        public static ArrayList GetLanguages(string languageCode)
        {
            ArrayList language = (ArrayList)languages[languageCode];

            return language is null ? (ArrayList)languages["en"] : language;
        }
    }
}