using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoListApp.Models;

namespace TodoListApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}