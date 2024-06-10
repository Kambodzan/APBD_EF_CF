using APBD_EF_CF.Entities;
using APBD_EF_CF.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IPrescriptionsRepository, PrescriptionsRepository>();

builder.Services.AddDbContext<PrescriptionsDBContext>(opt =>
{
    var connectionString = builder
        .Configuration
        .GetConnectionString("Default");
    opt.UseSqlServer(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();