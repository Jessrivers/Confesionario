using System.Text.Json.Serialization;
using ConcesionariaAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ConcesionariaDbContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"))
);

builder.Services.AddCors(opciones =>
    opciones.AddPolicy("ConcesionariaPolicy", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
    )
);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("ConcesionariaPolicy");

app.MapControllers();

app.Run();

//dotnet ef dbcontext scaffold "name=SqlServerConnection" Microsoft.EntityFrameworkCore.SqlServer -o Models --no-pluralize --force
