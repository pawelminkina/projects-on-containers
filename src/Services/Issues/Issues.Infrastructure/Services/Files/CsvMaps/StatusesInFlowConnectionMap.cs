using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Application.Common.Models.Files.Csv;
using Issues.Domain.StatusesFlow;
using Issues.Infrastructure.Services.Files.CsvMaps.TypeConverters;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class StatusesInFlowConnectionMap : ClassMap<StatusInFlowConnectionCsvDto>
    {
        public StatusesInFlowConnectionMap()
        {
            Map(s => s.ParentStatusInFlowId).Name("ParentStatusInFlowId");
            Map(s => s.ConnectedStatusInFlowId).Name("ConnectedStatusInFlowId");
            Map(s => s.Id).Name("Id");
            Map(s => s.Direction).Name("Direction").TypeConverter<StatusInFlowDirectionTypeConverter>();
        }
    }
}
