using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions
{
    [Serializable]
    public class InputValidationException : Exception
    {
        public InputValidationException(string message) : base(message)
        {
        }
    }
}
