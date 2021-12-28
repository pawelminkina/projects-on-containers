using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Domain.TypesOfIssues;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class TypesOfIssueInTypeOfGroupsMap : ClassMap<TypeOfIssueInTypeOfGroup>
    {
        public TypesOfIssueInTypeOfGroupsMap()
        {
            Map(s => s.Id).Name("Id");
            Map(s => s.ParentId).Name("ParentId");
            Map(s => s.StatusFlowId).Name("StatusFlowId");
            Map(s => s.TypeOfGroupOfIssuesId).Name("TypeOfGroupOfIssuesId");
            Map(s => s.IsArchived).Name("IsArchived");
        }
    }
}
