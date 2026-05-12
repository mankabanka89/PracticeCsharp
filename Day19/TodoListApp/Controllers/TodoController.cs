using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoListApp.Models;

namespace TodoListApp.Controllers
{
    public class TodoController : Controller
    {
        private static List<TodoItem> tasks = new List<TodoItem>();
        private static int nextId = 1;

        public IActionResult Index(string status = "all")
        {
            List<TodoItem> filteredTasks;

            if (status == "completed")
                filteredTasks = tasks.Where(t => t.IsCompleted).ToList();
            else if (status == "active")
                filteredTasks = tasks.Where(t => !t.IsCompleted).ToList();
            else
                filteredTasks = tasks;

            ViewBag.CurrentStatus = status;
            return View(filteredTasks);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(TodoItem task)
        {
            task.Id = nextId++;
            task.IsCompleted = false;
            tasks.Add(task);
            return RedirectToAction("Index");
        }

        public IActionResult Complete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                task.IsCompleted = true;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                tasks.Remove(task);
            return RedirectToAction("Index");
        }
    }
}