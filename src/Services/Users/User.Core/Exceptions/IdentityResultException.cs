using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace User.Core.Exceptions
{
    public class IdentityResultException : Exception
    {
        public IdentityResultException()
            : base()
        {
        }

        public IdentityResultException(string message)
            : base(message)
        {
        }

        public IdentityResultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public static IdentityResultException IdentityResultFailed(IdentityResult identityResult) =>
            new IdentityResultException($"IdentityResult has failed, result object: {identityResult.ToString()}");
    }
}
