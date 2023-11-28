using WebAPI.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<SmartStudyContext>(opt => opt.UseMySql(builder.Configuration.GetConnectionString("TestDB"), 
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("TestDB"))));

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Index}/{id?}");
app.Run();
