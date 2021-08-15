using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Application.Tasks.UpdateTaskStatus
{
    public class UpdateTaskStatusCommand : IRequest
    {
        public UpdateTaskStatusCommand(string taskId, string statusId, string userId)
        {
            TaskId = taskId;
            StatusId = statusId;
            UserId = userId;
        }

        public string TaskId { get; }
        public string StatusId { get; }
        public string UserId { get; }
    }
}
