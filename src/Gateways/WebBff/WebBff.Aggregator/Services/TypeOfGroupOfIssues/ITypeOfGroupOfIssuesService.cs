using WebBff.Aggregator.Models.TypeOfGroupOfIssues;

namespace WebBff.Aggregator.Services.TypeOfGroupOfIssues
{
    public interface ITypeOfGroupOfIssuesService
    {
        Task<IEnumerable<TypeOfGroupOfIssuesDto>> GetTypes();
        Task<TypeOfGroupOfIssuesDto> GetTypeWithGroups();
        Task<string> CreateTypeOfGroupOfIssues(TypeOfGroupOfIssuesForCreationDto dto);
        Task RenameTypeOfGroupOfIssues(RenameTypeOfGroupOfIssuesDto dto);
        Task DeleteTypeOfGroupOfIssues(string id);
    }
}
