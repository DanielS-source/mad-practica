using System.Globalization;

namespace Es.Udc.DotNet.PracticaMaD.Web.Http.View
{
    public class Locale
    {
        private const string DefaultLanguage = "en";
        private const string DefaultCountry = "US";

        public Locale(string language, string country)
        {
            if (language is null)
            {
                Language = DefaultLanguage;
            }
            else
            {
                Language = language;
            }
            if (country is null)
            {
                Country = DefaultCountry;
            }
            else
            {
                Country = country;
            }
            Culture = new CultureInfo(Language + "-" + Country);
        }

        public Locale()
        {
            Language = DefaultLanguage;
            Country = DefaultCountry;
            Culture = new CultureInfo(Language + "-" + Country);
        }

        #region Properties

        public string Language { get; private set; }
        public string Country { get; private set; }
        public CultureInfo Culture { get; private set; }

        #endregion Properties

        public override int GetHashCode()
        {
            unchecked
            {
                int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + (Language == null ? 0 : Language.GetHashCode());
                hash = hash * multiplier + (Country == null ? 0 : Country.GetHashCode());
                hash = hash * multiplier + Culture.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type?

            Locale target = (Locale)obj;

            return (this.Language == target.Language)
                && (this.Country == target.Country)
                && (this.Culture == target.Culture);
        }

        public override string ToString()
        {
            return Culture.Name;
        }
    }
}