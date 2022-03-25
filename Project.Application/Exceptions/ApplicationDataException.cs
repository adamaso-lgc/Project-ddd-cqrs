using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Exceptions
{
    public class ApplicationDataException : Exception
    {
        public ApplicationDataException(string message) : base(message) { }
    }
}
