using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Tasks.GetTask
{
    public class GetTaskQuery : IRequest<Task>
    {
        public GetTaskQuery(string taskId, string userId)
        {
            TaskId = taskId;
            UserId = userId;
        }

        public string TaskId { get; }
        public string UserId { get; }
    }
}
