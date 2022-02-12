namespace WebBff.Aggregator.Models.StatusFlow
{
    public class StatusInFlowDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public IEnumerable<string> ConnectedStatusesIds { get; set; }
    }
}
