using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Models.Mapping;
using Models.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<AbstractValidator<City>, CityValidator>();
builder.Services.AddScoped<AbstractValidator<Doctor>, DoctorValidator>();
builder.Services.AddScoped<AbstractValidator<Specialization>, SpecializationValidator>();
builder.Services.AddScoped<AbstractValidator<Polyclinic>, PolyclinicValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
