using Microsoft.EntityFrameworkCore;
using TestProjectWebApi.Data;
using TestProjectWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IPersonServices, PersonServices>();
//builder.Services.AddSingleton<IAccountServices, AccountServices>();

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

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
//app.UseStaticFiles();
//app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
