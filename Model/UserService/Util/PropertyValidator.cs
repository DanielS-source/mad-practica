using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService.Utils
{
    public static class PropertyValidator
    {
       
        #region UserProfile Properties

        public static bool IsValidLogin(string login)
        {
            return !string.IsNullOrWhiteSpace(login)
                && login.Length >= 4 && login.Length <= 24; // It must be between 4 and 24 characters
        }

        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password)
                && Regex.IsMatch(password, @".{8,24}")  // It must be between 8 and 24 characters
                && Regex.IsMatch(password, @"[0-9]+")   // It must contain at least a number
                && Regex.IsMatch(password, @"[A-Z]+");  // It must contain one upper case letter
        }

        public static bool IsValidFirstName(string firstName)
        {
            return !string.IsNullOrWhiteSpace(firstName)
                && firstName.Length >= 1 && firstName.Length <= 16; // It must be between 1 and 16 characters
        }

        public static bool IsValidLastName(string lastName)
        {
            return !string.IsNullOrWhiteSpace(lastName)
                && lastName.Length >= 1 && lastName.Length <= 24; // It must be between 1 and 24 characters
        }

        public static bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email)
                && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+${1,40}");   // It must contain an '@' before a '.' and be between 1 and 40 characters
        }

        public static bool IsValidCulture(string language, string country)
        {
            if (!string.IsNullOrWhiteSpace(language) && !string.IsNullOrWhiteSpace(country))
            {
                return language.Length == 2 && language.All(char.IsLower) && country.Length == 2 && country.All(char.IsUpper);
            }
            else
            {
                return false;
            }
        }

        #endregion UserProfile Properties
    }
}