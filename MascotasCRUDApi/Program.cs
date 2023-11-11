using MascotasCRUDApi.Models;
using MascotasCRUDApi.Models.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

//add Dbcontext 
builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("conexionDB"));
});

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//Add sercices
builder.Services.AddScoped<IMascotaRepository, MascotaRepository>();

//cors
var misreglasCors = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misreglasCors, builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(misreglasCors);
app.UseAuthorization();

app.MapControllers();

app.Run();
