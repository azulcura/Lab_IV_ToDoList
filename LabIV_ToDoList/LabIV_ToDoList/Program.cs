using Microsoft.EntityFrameworkCore;
using LabIV_ToDoList.DBContext;
using LabIV_ToDoList.Services.Interfaces;
using LabIV_ToDoList.Services;
using LabIV_ToDoList.DBContext;
using LabIV_ToDoList.Services.Implementations;
using LabIV_ToDoList.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<ToDoListContext>(options =>
    options.UseSqlite(builder.Configuration["DB:ConnectionString"]));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ITodoItemServices, TodoItemServices>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.IgnoreNullValues = true;
});



builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
