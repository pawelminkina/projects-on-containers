using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebBff.Api.Models.Issuses.Status;

namespace WebBff.Api.Services.Issues.Status
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetStatusesAsync();
        Task<StatusDto> GetStatusAsync(string statusId);
        Task<string> CreateStatusAsync(StatusDto status);
        Task DeleteStatusAsync(string id);
        Task RenameStatusAsync(string id, string newName);
    }
}