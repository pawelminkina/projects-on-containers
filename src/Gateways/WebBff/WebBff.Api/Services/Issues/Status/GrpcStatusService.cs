using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.API.Protos;
using WebBff.Api.Models.Issuses.Status;
using CreateStatusRequest = Issues.API.Protos.CreateStatusRequest;

namespace WebBff.Api.Services.Issues.Statuses
{
    public class GrpcStatusService : IStatusService
    {
        private readonly StatusService.StatusServiceClient _client;

        public GrpcStatusService(StatusService.StatusServiceClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<StatusDto>> GetStatusesAsync()
        {
            var res = await _client.GetStatusesAsync(new GetStatusesRequest());
            return res.Statuses.Select(MapTDto);
        }

        public async Task<StatusDto> GetStatusAsync(string statusId)
        {
            var res = await _client.GetStatusAsync(new GetStatusRequest() { Id = statusId});
            return MapTDto(res.Status);
        }

        public async Task DeleteStatusAsync(string id)
        {
            throw new InvalidOperationException();
        }

        public async Task RenameStatusAsync(string id, string newName)
        {
            var res = await _client.RenameStatusAsync(new RenameStatusRequest() { Id = id, Name = newName});
        }

        public async Task<string> CreateStatusAsync(Models.Issuses.Status.CreateStatusRequest request)
        {
            var res = await _client.CreateStatusAsync(new CreateStatusRequest() { Name = request.Name});
            return res.Id;
        }

        private StatusDto MapTDto(Status status) => new StatusDto() { Id = status.Id, Name = status.Name };
    }
}