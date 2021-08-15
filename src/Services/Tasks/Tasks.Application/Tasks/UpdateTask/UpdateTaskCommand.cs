using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Application.Tasks.UpdateTask
{
    public class UpdateTaskCommand : IRequest
    {
        public UpdateTaskCommand(string id, string name, string content)
        {
            Id = id;
            Name = name;
            Content = content;
        }

        public string Id { get; }
        public string Name { get; }
        public string Content { get; }
    }
}
