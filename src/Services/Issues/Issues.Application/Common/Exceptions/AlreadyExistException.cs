using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Application.Common.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException()
            : base()
        {
        }

        public AlreadyExistException(string message)
            : base(message)
        {
        }

        public AlreadyExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public static AlreadyExistException EntityWithPropertyAlreadyExist(string entityName, string nameOfProperty, string value)
        {
            var message = new StringBuilder(entityName).Append(" with ").Append(nameOfProperty).Append(": ").Append(value).Append(" already exist");
            return new AlreadyExistException(message.ToString());
        }
    }
}
