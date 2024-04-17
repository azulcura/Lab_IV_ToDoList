using LabIV_ToDoList.Data;
using LabIV_ToDoList.Models;

namespace LabIV_ToDoList.Services.Interfaces
{
    public interface ITodoItemServices
    {
        List<TodoItem> GetAllTodoItems();
        TodoItem GetTodoItemById(int id);
        int AddTodoItem(TodoItemDto item);
        int DeleteTodoItem(int id);
        Task UpdateTodoItem(int id, TodoItemDto todoItemDto);
    }
}
