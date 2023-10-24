using Microsoft.EntityFrameworkCore;
using OilOps.DataAccess;
using OilOps.Models;
using OilOps.Repository;
using OilOps.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// DBProjects
builder.Services.AddDbContext<ProjectsDbContext>
    (options => options.UseInMemoryDatabase("ProjectsDb"));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

// DBServices
builder.Services.AddDbContext<ServicesDbContext>
    (options => options.UseInMemoryDatabase("ServicesDB"));
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

// DBUser
builder.Services.AddDbContext<UsersDbContext>
    (options => options.UseInMemoryDatabase("UsersDB"));
builder.Services.AddScoped<IUserRepository, UserRepository>();

// DBWorks
builder.Services.AddDbContext<WorksDbContext>
    (options => options.UseInMemoryDatabase("WorksDB"));
builder.Services.AddScoped<IWorkRepository, WorkRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();