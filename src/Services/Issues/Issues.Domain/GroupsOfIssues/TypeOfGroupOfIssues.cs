using Architecture.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.GroupsOfIssues
{
    public class TypeOfGroupOfIssues : EntityBase
    {
        public TypeOfGroupOfIssues(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }

        public TypeOfGroupOfIssues()
        {

        }

        public string Name { get; }
    }
}
