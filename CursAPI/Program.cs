using CursAPI.Middlewares;
using CursAPI.Models;
using CursAPI.RegistrationServices;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//��������� ������ �������
builder.Services.AddTransient<ILogger>(s => s.GetRequiredService<ILogger<Program>>());

//���� �������
builder.Services.AddAuth();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//��������� ��� Middleware
app.UseMiddleware<StartMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseRouting();
app.Map("/user", () => new UserModel { Name = "1231" });

app.Run();
