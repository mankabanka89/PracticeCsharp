using Microsoft.AspNetCore.Mvc;
using TodoListApp.Models;
using TodoListApp.Services;

namespace TodoListApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITaskService _taskService;

        // Внедрение сервиса через конструктор
        public TodoController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // Список задач с фильтрацией
        public IActionResult Index(string status = "all")
        {
            var tasks = _taskService.GetTasks(status);
            ViewBag.CurrentStatus = status;
            return View(tasks);
        }

        // Форма добавления задачи
        public IActionResult Add()
        {
            return View();
        }

        // Добавление задачи (POST)
        [HttpPost]
        public IActionResult Add(TaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var task = new TaskDto
            {
                Title = model.Title,
                DueDate = model.DueDate,
                IsCompleted = false
            };

            _taskService.AddTask(task);
            TempData["Message"] = "Задача успешно добавлена!";
            return RedirectToAction("Index");
        }

        // Отметить задачу как выполненную
        public IActionResult Complete(int id)
        {
            _taskService.CompleteTask(id);
            ViewBag.Message = "Задача отмечена как выполненная!";
            return RedirectToAction("Index");
        }
    }
}