using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Issues.Domain.StatusesFlow;

namespace Issues.Application.Common.Models.Files.Csv
{
    public class StatusInFlowConnectionCsvDto
    {
        public string Id { get; set; }
        public string ParentStatusInFlowId { get; set; }
        public string ConnectedStatusInFlowId { get; set; }
        public StatusInFlowDirection Direction { get; set; }
    }
}
