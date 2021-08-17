namespace Issues.Domain.GroupsOfIssues
{
    /// <summary>
    /// Used for adding archive policy for ex. when all projects with this type should also be archived
    /// </summary>
    public interface ITypeGroupOfIssuesArchivePolicy
    {
        bool Archive();
    }
}