using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Core.Exceptions
{
    public static class ApplicationErrorMessages
    {
        public const string PasswordIsNotPassingRequiredCriteria =
            "Password should contains 8 characters and consist of at least one upper case, one number and one special";

    }
}
