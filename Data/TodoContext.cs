using aspnetwebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace aspnetwebapi.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }
        
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}