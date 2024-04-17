using LabIV_ToDoList.Data;
using LabIV_ToDoList.DBContext;
using LabIV_ToDoList.Models;
using LabIV_ToDoList.Services.Interfaces;

namespace LabIV_ToDoList.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly ToDoListContext _context;

        public UserServices(ToDoListContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public List<TodoItem> GetTodoItemsByUserId(int userId)
        {
            return _context.TodoItems.Where(t => t.UserId == userId).ToList();
        }
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.id == id);
        }

        public User AddUser(UserDto user)
        {
            var newUser = new User
            {
                name = user.name,
                email = user.email,
                address = user.address
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }
        public User UpdateUser(int id, UserDto user)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
                return null;

            existingUser.name = user.name;
            existingUser.email = user.email;
            existingUser.address = user.address;

            _context.SaveChanges();

            return existingUser;
        }
        public int DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.id == id);
            if (user == null)
                return 0;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return 1;
        }
    }
}
