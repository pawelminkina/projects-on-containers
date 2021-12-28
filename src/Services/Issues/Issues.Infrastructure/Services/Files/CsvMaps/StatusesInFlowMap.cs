using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Issues.Domain.StatusesFlow;

namespace Issues.Infrastructure.Services.Files.CsvMaps
{
    public class StatusesInFlowMap : ClassMap<StatusInFlow>
    {
        public StatusesInFlowMap()
        {
            Map(s => s.Id).Name("Id");
            Map(s => s.ParentStatusId).Name("ParentStatusId");
            Map(s => s.StatusFlowId).Name("StatusFlowId");
            Map(s => s.IndexInFlow).Name("IndexInFlow");
            Map(s => s.IsArchived).Name("IsArchived");

        }
    }
}
