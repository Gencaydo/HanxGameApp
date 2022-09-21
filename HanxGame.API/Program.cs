using FluentValidation.AspNetCore;
using HanxGame.API.Filters;
using HanxGame.Core.Repositories;
using HanxGame.Core.Services;
using HanxGame.Repository;
using HanxGame.Repository.Repositories;
using HanxGame.Service.Mapping;
using HanxGame.Service.Services;
using HanxGame.Service.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<GameDtoValidator>());
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
        options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<AppDbContext>());
builder.Services.AddScoped<IApplicationExecuteQueryDb, ApplicationExecuteQueryDb>();
builder.Services.AddScoped<IApplicationExecuteQueryDbService, ApplicationExecuteQueryDbService>();
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
