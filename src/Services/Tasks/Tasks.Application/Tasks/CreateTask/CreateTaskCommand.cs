using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Application.Tasks.CreateTask
{
    public class CreateTaskCommand : IRequest<string>
    {
        public CreateTaskCommand(string name, string content, string userId)
        {
            Name = name;
            Content = content;
            UserId = userId;
        }

        public string Name { get; }
        public string Content { get; }
        public string UserId { get; }
    }
}
