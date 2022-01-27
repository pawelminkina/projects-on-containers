using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Application.Common.Models.Files.Csv
{
    public class StatusInFlowCsvDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StatusFlowId { get; set; }
        public bool IsDefault { get; set; }

    }
}
