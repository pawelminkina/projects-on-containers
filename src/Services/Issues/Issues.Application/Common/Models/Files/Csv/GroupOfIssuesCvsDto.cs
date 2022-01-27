using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Application.Common.Models.Files.Csv
{
    public class GroupOfIssuesCvsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string TypeOfGroupId { get; set; }
        public string ConnectedStatusFlowId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? TimeOfDeleteUtc { get; set; }
    }
}
