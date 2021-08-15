using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Application.Tasks.DeleteTask
{
    public class DeleteTaskCommand : IRequest
    {
        public DeleteTaskCommand(string taskId, string userId)
        {
            TaskId = taskId;
            UserId = userId;
        }

        public string TaskId { get; }
        public string UserId { get; }
    }
}
