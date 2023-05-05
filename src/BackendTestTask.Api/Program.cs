using BackendTestTask.Api.Mappers;
using BackendTestTask.Api.Middleware;
using BackendTestTask.Business.Interfaces;
using BackendTestTask.Business.Models;
using BackendTestTask.Business.Services;
using BackendTestTask.Core;
using BackendTestTask.Core.Interfaces;
using BackendTestTask.Core.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(options =>
{
    options.AddMaps(typeof(AutoMapperProfile));
}, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<INodeRepository, NodeRepository>();
builder.Services.AddScoped<IJournalsRepository, JournalsRepository>();
builder.Services.AddScoped<INodeService, NodeService>();
builder.Services.AddScoped<IJournalService, JournalService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Swagger",
            Version = "v1",
            Description = "BackendTestTask.Api"
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>(); 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();