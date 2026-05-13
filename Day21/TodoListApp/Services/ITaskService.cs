using System.Collections.Generic;
using TodoListApp.Models;

namespace TodoListApp.Services
{
    public interface ITaskService
    {
        void AddTask(TaskDto task);
        void CompleteTask(int id);
        List<TaskDto> GetTasks(string status = "all");
        TaskDto GetTaskById(int id);
    }
}