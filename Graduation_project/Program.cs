using Microsoft.EntityFrameworkCore;
using Repo_Core;
using Repo_Core.Interface;
using Repo_EF;
using Repo_EF.Repo_Method;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeafultConnection"),
    o => o.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
//builder.Services.AddTransient(typeof(IRegsiter<>), typeof(Regsiter_Method<>));
builder.Services.AddTransient<IUnitWork, UnitWork>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
