using LabIV_ToDoList.Models;
using LabIV_ToDoList.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabIV_ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemServices _todoItemServices;

        public TodoItemController(ITodoItemServices todoItemServices)
        {
            _todoItemServices = todoItemServices;
        }

        [HttpGet]
        public IActionResult GetTodoItems()
        {
            var todoItems = _todoItemServices.GetAllTodoItems();
            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public IActionResult GetTodoItem(int id)
        {
            var todoItem = _todoItemServices.GetTodoItemById(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTodoItem(int id, [FromBody] TodoItemDto itemDto)
        {
            var updatedItem = _todoItemServices.UpdateTodoItem(id, itemDto);
            if (updatedItem == null)
            {
                return NotFound();
            }
            return Ok(updatedItem);
        }
        [HttpPost]
        public IActionResult AddTodoItem(TodoItemDto item)
        {
            var newItemId = _todoItemServices.AddTodoItem(item);

            var todoItemResponse = new
            {
                id = newItemId
            };

            return CreatedAtAction(nameof(GetTodoItem), new { id = newItemId }, todoItemResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {
            var result = _todoItemServices.DeleteTodoItem(id);
            if (result == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
