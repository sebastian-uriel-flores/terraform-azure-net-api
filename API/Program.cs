using Microsoft.EntityFrameworkCore;
using DemoAPIAzure;
using DemoAPIAzure.Middlewares;
using DemoAPIAzure.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IHealthService, HealthService>();
builder.Services.AddDbContext<ToDoContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cnToDos"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);    
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IToDoService, ToDoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ToDoContext>();
    dataContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseLoggingMiddleware();

app.MapControllers();

app.Run();