using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Domain.GroupsOfIssues;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class GroupsOfIssuesMap : ClassMap<GroupOfIssues>
    {
        public GroupsOfIssuesMap()
        {
            Map(s => s.Id).Name("Id");
            Map(s => s.Name).Name("Name");
            Map(s => s.ShortName).Name("ShortName");
            Map(s => s.TypeOfGroupId).Name("TypeOfGroupId");
            Map(s => s.IsArchived).Name("IsArchived");
            Map(s => s.TypeOfGroup).Ignore();
            Map(s => s.Issues).Ignore();
        }
    }
}
