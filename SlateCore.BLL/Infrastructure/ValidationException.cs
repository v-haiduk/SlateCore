using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlateCore.BLL.Infrastructure
{
    class ValidationException : Exception
    {
        public string NameOfProperty { get; protected set; }
        public ValidationException(string message, string property) : base(message)
        {
            NameOfProperty = property;
        }
    }
}
