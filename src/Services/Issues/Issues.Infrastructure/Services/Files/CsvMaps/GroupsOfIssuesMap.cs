using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Application.Common.Models.Files.Csv;
using Issues.Domain.GroupsOfIssues;
using Issues.Infrastructure.Services.Files.CsvMaps.TypeConverters;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class GroupsOfIssuesMap : ClassMap<GroupOfIssuesCvsDto>
    {
        public GroupsOfIssuesMap()
        {
            Map(s => s.Id).Name("Id");
            Map(s => s.Name).Name("Name");
            Map(s => s.ShortName).Name("ShortName");
            Map(s => s.TypeOfGroupId).Name("TypeOfGroupId");
            Map(s => s.ConnectedStatusFlowId).Name("ConnectedStatusFlowId");
            Map(s => s.IsDeleted).Name("IsDeleted");
            Map(s => s.TimeOfDeleteUtc).Name("TimeOfDeleteUtc").TypeConverter<NullableDateTimeOffsetConverter>();
        }
    }
}
