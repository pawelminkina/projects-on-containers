using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Domain.GroupsOfIssues;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class TypeOfGroupOfIssuesMap : ClassMap<TypeOfGroupOfIssues>
    {
        public TypeOfGroupOfIssuesMap()
        {
            Map(s => s.Name).Name("Name");
            Map(s => s.Id).Name("Id");
            Map(s => s.OrganizationId).Name("OrganizationId");
            Map(s => s.IsDefault).Name("IsDefault");
            Map(s => s.Groups).Ignore();
        }
    }
}
