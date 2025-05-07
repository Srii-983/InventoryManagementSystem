using InventoryManagementSystem.Data;  // <- Namespace for your DbContext
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add services to the container.

builder.Services.AddControllers();

// ✅ Configure your SQL Server connection string here:
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Add CORS for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:3000")
                         .AllowAnyHeader()
                         .AllowAnyMethod();
        });
});

// ✅ Learn Swagger (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Use Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Use HTTPS Redirection
app.UseHttpsRedirection();

// ✅ Use CORS policy here before controllers
app.UseCors("AllowReactApp");

app.UseAuthorization();

// ✅ Map Controllers
app.MapControllers();

// ✅ Run the app
app.Run();
