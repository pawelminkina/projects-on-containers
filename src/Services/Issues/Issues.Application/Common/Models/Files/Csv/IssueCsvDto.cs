using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Application.Common.Models.Files.Csv
{
    public class IssueCsvDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CreatingUserId { get; set; }
        public string TextContent { get; set; }
        public string GroupOfIssueId { get; set; }
        public DateTimeOffset TimeOfCreation { get; set; }
        public bool IsDeleted { get; set; }
        public string StatusInFlowId { get; set; }

    }
}
