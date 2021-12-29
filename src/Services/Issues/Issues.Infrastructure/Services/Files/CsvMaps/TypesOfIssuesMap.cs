using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Domain.TypesOfIssues;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class TypesOfIssuesMap : ClassMap<TypeOfIssue>
    {
        public TypesOfIssuesMap()
        {
            Map(s => s.Id).Name("Id");
            Map(s => s.Name).Name("Name");
            Map(s => s.OrganizationId).Name("OrganizationId");
            Map(s => s.IsArchived).Name("IsArchived");
            Map(s => s.TypesInGroups).Ignore();
        }
    }
}
