namespace WebBff.Aggregator.Models.StatusFlow;

public class StatusFlowWithStatusesDto : StatusFlowDto
{
    public StatusFlowWithStatusesDto(StatusFlowDto statusFlow)
    {
        Id = statusFlow.Id;
        IsDefault = statusFlow.IsDefault;
        IsDeleted = statusFlow.IsDeleted;
        Name = statusFlow.Name;
    }

    public StatusFlowWithStatusesDto()
    {
        
    }
    public IEnumerable<StatusInFlowDto> Statuses { get; set; }
}