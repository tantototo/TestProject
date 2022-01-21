using Microsoft.EntityFrameworkCore;
using TestProjectWebApi;
using TestProjectWebApi.Data;
using TestProjectWebApi.Services;
using Autofac;
using TestProjectWebApi.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddTransient<IMailServices, MailServices>();
builder.Services.AddSingleton<IPersonServices>(x =>
    new PersonServices(x.CreateScope().ServiceProvider.GetService<AppDBContext>()));
builder.Services.AddSingleton<IAccountServices>(x =>
    new AccountServices(x.CreateScope().ServiceProvider.GetService<AppDBContext>()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

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
