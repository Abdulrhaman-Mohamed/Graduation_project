using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repo_Core;
using Repo_EF;
using Repo_EF.Repo_Method;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using Repo_Core.Services;
using Repo_Core.Identity_Models;
using Repo_Core.Helper;
using Graduation_project.ViewModel;
using Repo_Core.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeafultConnection"),
    o => o.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

var conString = builder.Configuration.GetConnectionString("DeafultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conString));



builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();





builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
//builder.Services.AddTransient(typeof(IRegsiter<>), typeof(Regsiter_Method<>));
builder.Services.AddTransient<IUnitWork, UnitWork>();
builder.Services.AddSingleton<ISocketHandler, SocketHandler>();
builder.Services.AddTransient<ISocketBuilder, SocketBuilder>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEditting, EdittingServices>();
builder.Services.AddTransient<IBlogService, BlogService>();
builder.Services.AddHostedService<ScheduledJobService>();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("Grud", p => p.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Grud");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseWebSockets();

app.Run();
