using StudentManagementAPI.Data;
using StudentManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add controller services
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Student Repository
builder.Services.AddSingleton<StudentRepository>();

var app = builder.Build();

// Custom Middleware
app.UseMiddleware<RequestLoggingMiddleware>();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

// HTTPS redirection
app.UseHttpsRedirection();

// Map API controllers
app.MapControllers();

// Start application
app.Run();