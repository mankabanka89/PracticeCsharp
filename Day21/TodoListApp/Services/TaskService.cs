using System;
using System.Collections.Generic;
using System.Linq;
using TodoListApp.Models;

namespace TodoListApp.Services
{
    public class TaskService : ITaskService
    {
        private static List<TaskDto> _tasks = new List<TaskDto>();
        private static int _nextId = 1;

        public void AddTask(TaskDto task)
        {
            task.Id = _nextId++;
            task.CreatedAt = DateTime.Now;
            _tasks.Add(task);
        }

        public void CompleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                task.IsCompleted = true;
        }

        public List<TaskDto> GetTasks(string status = "all")
        {
            if (status == "completed")
                return _tasks.Where(t => t.IsCompleted).ToList();
            else if (status == "active")
                return _tasks.Where(t => !t.IsCompleted).ToList();
            else
                return _tasks.OrderByDescending(t => t.CreatedAt).ToList();
        }

        public TaskDto GetTaskById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id) ?? new TaskDto();
        }
    }
}