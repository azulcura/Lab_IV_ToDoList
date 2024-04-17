using LabIV_ToDoList.Data;
using Microsoft.EntityFrameworkCore;

namespace LabIV_ToDoList.DBContext
{
   
        public class ToDoListContext : DbContext
        {
            public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options)
            {
            }

            public DbSet<User> Users { get; set; }
            public DbSet<TodoItem> TodoItems { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>().HasData(new User
                {
                    id = 1,
                    name = "Azul",
                    email = "azulcura1@gmail.com",
                    address = "Buchanan 679"
                });

                modelBuilder.Entity<TodoItem>().HasData(new TodoItem
                {
                    id = 2,
                    title = "Diseño del Login",
                    description = "diseño del css del Login",
                    UserId = 1
                });

                modelBuilder.Entity<TodoItem>()
                    .HasOne(t => t.User)
                    .WithMany(u => u.TodoItems)
                    .HasForeignKey(t => t.UserId);

                base.OnModelCreating(modelBuilder);
            }
        }
 }
