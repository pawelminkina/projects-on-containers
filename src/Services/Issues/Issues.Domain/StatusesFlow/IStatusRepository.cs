using System.Threading.Tasks;

namespace Issues.Domain.StatusesFlow
{
    public interface IStatusRepository
    {
        Task<Status> AddNewStatusAsync(string name, string organizationId);
        Task<StatusFlow> AddNewStatusFlowAsync(string name, string organizationId);
    }
}