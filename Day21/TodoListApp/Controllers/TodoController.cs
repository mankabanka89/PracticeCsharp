using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Data;
using TodoListApp.Models;

namespace TodoListApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

         public async Task<IActionResult> Index()
        {
            var tasks = await _context.ToDoItems.ToListAsync();
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoItem task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedAt = DateTime.Now;
                task.IsCompleted = false;
                _context.Add(task);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Задача успешно добавлена!";
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        public async Task<IActionResult> Complete(int id)
        {
            var task = await _context.ToDoItems.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            task.IsCompleted = true;
            _context.Update(task);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Задача отмечена как выполненная!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.ToDoItems.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.ToDoItems.FindAsync(id);
            if (task != null)
            {
                _context.ToDoItems.Remove(task);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Задача удалена!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}