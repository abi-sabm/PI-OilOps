using Microsoft.EntityFrameworkCore;
using OilOps.DataAccess;
using OilOps.Repository;
using OilOps.Repository.Interfaces;
using OilOps.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db connection
builder.Services.AddDbContext<OilOpsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OilOpsConnection"));
});

builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<IWorkService, WorkService>();
builder.Services.AddScoped<IWorkRepository, WorkRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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