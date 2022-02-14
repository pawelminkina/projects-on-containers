using System.Threading.Tasks;

namespace Issues.Application.Common.Services.DataCleaners;

public interface IOrganizationDataCleaner
{
    Task CleanAsync(string organizationId);
}