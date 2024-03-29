﻿using WebBff.Aggregator.Models.GroupOfIssues;

namespace WebBff.Aggregator.Models.TypeOfGroupOfIssues
{
    public class TypeOfGroupOfIssuesWithGroupsDto : TypeOfGroupOfIssuesDto
    {
        public TypeOfGroupOfIssuesWithGroupsDto(TypeOfGroupOfIssuesDto typeOfGroupOfIssues)
        {
            Id = typeOfGroupOfIssues.Id;
            Name = typeOfGroupOfIssues.Name;
            IsDefault = typeOfGroupOfIssues.IsDefault;
        }
        public IEnumerable<GroupOfIssuesDto> GroupsOfIssues { get; set; }
    }
}
