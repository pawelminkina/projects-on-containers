using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Application.Common.Models.Files.Csv;
using Issues.Domain.Issues;
using Issues.Infrastructure.Services.Files.CsvMaps.TypeConverters;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class IssuesMap : ClassMap<IssueCsvDto>
    {
        public IssuesMap()
        {
            Map(s => s.TextContent).Name("IssueTextContent");
            Map(s => s.Id).Name("Id");
            Map(s => s.Name).Name("Name");
            Map(s => s.StatusInFlowId).Name("StatusInFlowId");
            Map(s => s.CreatingUserId).Name("CreatingUserId");
            Map(s => s.GroupOfIssueId).Name("GroupOfIssueId");
            Map(s => s.IsDeleted).Name("IsDeleted");
            Map(s => s.TimeOfCreation).Name("TimeOfCreation");
        }
    }
}
