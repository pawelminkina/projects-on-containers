using System.Collections.Generic;
using System.Threading.Tasks;

namespace Issues.Domain.StatusesFlow
{
    public interface IStatusRepository
    {
        Task<Status> AddNewStatusAsync(string name, string organizationId);
        Task<Status> GetStatusById(string id); //comment hot fix but psycha is sitting
        Task<IEnumerable<Status>> GetStatusesForOrganization(string organizationId);
        Task RemoveStatusById(string id);
    }
}