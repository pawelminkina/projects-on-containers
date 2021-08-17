
namespace Issues.Domain.StatusesFlow
{
    public interface IStatusInFlowDeletePolicy
    {
        bool Delete(string id);
    }
}
