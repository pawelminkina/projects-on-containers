namespace WebBff.Aggregator.Models.StatusFlow;

public class StatusFlowWithStatusesDto : StatusFlowDto
{
    public IEnumerable<StatusInFlowDto> Statuses { get; set; }
}