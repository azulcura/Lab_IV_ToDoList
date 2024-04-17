using LabIV_ToDoList.Data;
using LabIV_ToDoList.DBContext;
using LabIV_ToDoList.Models;
using LabIV_ToDoList.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabIV_ToDoList.Services.Implementations
{
    
       public class TodoItemServices : ITodoItemServices
        {
            private readonly ToDoListContext _context;

            public TodoItemServices(ToDoListContext context)
            {
                _context = context;
            }

            public List<TodoItem> GetAllTodoItems()
            {
                return _context.TodoItems.ToList();
            }

            public TodoItem GetTodoItemById(int id)
            {
                return _context.TodoItems.FirstOrDefault(t => t.id == id);
            }

            public int AddTodoItem(TodoItemDto item)
            {
                var newTodoItem = new TodoItem
                {
                    title = item.title,
                    description = item.description,
                    UserId = item.UserId
                };

                _context.TodoItems.Add(newTodoItem);
                _context.SaveChanges();
                return newTodoItem.id;
            }

            public int DeleteTodoItem(int id)
            {
                var todoItem = _context.TodoItems.FirstOrDefault(t => t.id == id);
                if (todoItem == null)
                    return 0;

                _context.TodoItems.Remove(todoItem);
                _context.SaveChanges();
                return 1;
            }
            public async Task UpdateTodoItem(int id, TodoItemDto todoItemDto)
            {
                try
                {
                    var existingTodoItem = await _context.TodoItems.FindAsync(id);
                    if (existingTodoItem == null)
                    {
                        throw new Exception($"TodoItem con ID {id} no encontrado.");
                    }

                    existingTodoItem.title = todoItemDto.title;
                    existingTodoItem.description = todoItemDto.description;
                    existingTodoItem.UserId = todoItemDto.UserId;

                    _context.Entry(existingTodoItem).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException;
                    while (innerException != null)
                    {
                        Console.WriteLine(innerException.Message);
                        innerException = innerException.InnerException;
                    }
                    throw;
                }
            }

        }
   }

