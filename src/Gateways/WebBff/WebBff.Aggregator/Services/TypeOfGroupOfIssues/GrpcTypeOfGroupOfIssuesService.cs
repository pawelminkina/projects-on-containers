using Issues.API.Protos;
using WebBff.Aggregator.Models.TypeOfGroupOfIssues;

namespace WebBff.Aggregator.Services.TypeOfGroupOfIssues;

public class GrpcTypeOfGroupOfIssuesService : ITypeOfGroupOfIssuesService
{
    private readonly TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceClient _grpcClient;

    public GrpcTypeOfGroupOfIssuesService(TypeOfGroupOfIssueService.TypeOfGroupOfIssueServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }
    public async Task<IEnumerable<TypeOfGroupOfIssuesDto>> GetTypes()
    {
        var response = await _grpcClient.GetTypesOfGroupsOfIssuesAsync(new GetTypesOfGroupsOfIssuesRequest());
        return response.TypesOfGroups.Select(MapToDto);
    }

    public async Task<TypeOfGroupOfIssuesDto> GetType(string id)
    {
        var response = await _grpcClient.GetTypeOfGroupOfIssuesAsync(new GetTypeOfGroupOfIssuesRequest() {Id = id});
        return MapToDto(response.TypeOfGroup);
    }

    public async Task<string> CreateTypeOfGroupOfIssues(TypeOfGroupOfIssuesForCreationDto dto)
    {
        var response = await _grpcClient.CreateTypeOfGroupOfIssuesAsync(new CreateTypeOfGroupOfIssuesRequest() {Name = dto.Name});
        return response.Id;
    }

    public async Task RenameTypeOfGroupOfIssues(RenameTypeOfGroupOfIssuesDto dto)
    {
        await _grpcClient.RenameTypeOfGroupOfIssuesAsync(new RenameTypeOfGroupOfIssuesRequest() {Id = dto.Id, NewName = dto.NewName});
    }

    public async Task DeleteTypeOfGroupOfIssues(string id)
    {
        await _grpcClient.DeleteTypeOfGroupOfIssuesAsync(new DeleteTypeOfGroupOfIssuesRequest() {Id = id});
    }

    public TypeOfGroupOfIssuesDto MapToDto(global::Issues.API.Protos.TypeOfGroupOfIssues typeOfGroupOfIssues)
    {
        return new TypeOfGroupOfIssuesDto()
        {
            Id = typeOfGroupOfIssues.Id,
            Name = typeOfGroupOfIssues.Name
        };
    }
}