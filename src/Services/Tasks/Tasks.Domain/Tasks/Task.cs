using Architecture.DDD;
using Architecture.DDD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Domain.Tasks
{
    public class Task : EntityBase, IAggregateRoot 
    {
        public Task()
        {

        }
#warning todo there should be status added
        public Task(DateTimeOffset timeOfCreation, string name, string content)
        {
            TimeOfCreation = timeOfCreation;
            Name = name;
            Content = content;
        }

        public DateTimeOffset TimeOfCreation { get; }
        public string Name { get; }
        public string Content { get; }
    }
}
