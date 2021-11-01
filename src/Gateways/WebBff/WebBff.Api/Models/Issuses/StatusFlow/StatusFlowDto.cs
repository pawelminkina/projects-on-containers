using System.Collections.Generic;

namespace WebBff.Api.Models.Issuses.StatusFlow
{
    public class StatusFlowDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<StatusInFlow> Statuses { get; set; }
    }

    public class StatusInFlow
    {
        public string ParentStatusId { get; set; }
        public int IndexInFlow { get; set; }
        public IEnumerable<string> ChildStatusIds { get; set; }
    }
}