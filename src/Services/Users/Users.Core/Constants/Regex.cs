using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Core.Constants
{
    public class Regex
    {
        /// <summary>
        /// String contains minimum 8 character
        /// and consist of at least one upper or lower case
        /// (for example: ABCDEFGH - not ok, ABCDEFGh - ok, abcdefgh - not ok, abcdefgH - ok)
        /// and one number and one special character
        /// </summary>
        public const string PasswordRule = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$";
    }
}
