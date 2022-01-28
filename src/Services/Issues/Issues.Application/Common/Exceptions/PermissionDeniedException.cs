using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Application.Common.Exceptions
{
    public class PermissionDeniedException : Exception
    {
        public PermissionDeniedException()
            : base()
        {
        }

        public PermissionDeniedException(string message)
            : base(message)
        {
        }

        public PermissionDeniedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public static PermissionDeniedException ResourceFoundAndNotAccessibleInOrganization(string resourceId, string organizationId)
        {
            return new PermissionDeniedException($"Requested resource with id: {resourceId} was found and is not accessible for organization with id: {organizationId}");
        }
    }
}
