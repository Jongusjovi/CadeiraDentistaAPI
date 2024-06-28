using DentistaCadeirasAPI.Data;
using DentistaCadeirasAPI.Data.Interfaces;
using DentistaCadeirasAPI.Data.Repositories;
using DentistaCadeirasAPI.Middleware;
using DentistaCadeirasAPI.Services;
using DentistaCadeirasAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DentistaCadeirasContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddScoped<ICadeiraRepository, CadeiraRepository>();
builder.Services.AddScoped<ICadeiraService, CadeiraService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DentistaCadeirasAPI", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DentistaCadeirasContext>();

    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DentistaCadeirasAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
