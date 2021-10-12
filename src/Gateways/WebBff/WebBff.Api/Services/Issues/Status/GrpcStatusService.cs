using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Issues.API.Protos;
using WebBff.Api.Models.Issuses.Status;
using CreateStatusRequest = Issues.API.Protos.CreateStatusRequest;

namespace WebBff.Api.Services.Issues.Status
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
            return null;
        }

        public async Task<StatusDto> GetStatusAsync(string statusId)
        {
            var res = await _client.GetStatusAsync(new GetStatusRequest());
            return null;
        }

        public async Task<string> CreateStatusAsync(StatusDto status)
        {
            var res = await _client.CreateStatusAsync(new CreateStatusRequest());
            return string.Empty;
        }

        public async Task DeleteStatusAsync(string id)
        {
            throw new InvalidOperationException();
        }

        public async Task RenameStatusAsync(string id, string newName)
        {
            var res = await _client.RenameStatusAsync(new RenameStatusRequest());
        }
    }
}