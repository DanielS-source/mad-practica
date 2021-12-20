using System;

namespace Es.Udc.DotNet.PracticaMaD.Web.Http.Exceptions
{
    [Serializable]
    public class AuthenticationException : Exception
    {
        public AuthenticationException() : base("No authenticated user found in this session")
        {
        }
    }
}