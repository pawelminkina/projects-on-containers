using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Tasks.GetTasks
{
    public class GetTasksQuery : IRequest<IEnumerable<Task>>
    {
        public GetTasksQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
