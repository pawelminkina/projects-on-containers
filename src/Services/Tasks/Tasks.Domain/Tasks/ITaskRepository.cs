using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks.Domain.Tasks
{
    public interface ITaskRepository
    {
        System.Threading.Tasks.Task<IEnumerable<Task>> GetTasksForUserAsync(string userId);
        System.Threading.Tasks.Task<Task> GetTaskByIdAsync(string id);
    }
}
