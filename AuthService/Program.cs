using AuthService.Data;
using AuthService.Models;
using DataBaseService.Logger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

Configure(app);

app.MapControllers();

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddSingleton<ILoggerManager, LoggerManager>(); //TODO добавить отправку на почту
    services.AddScoped<IRepository, Repository>();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddDbContext<AuthServiceContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));

    });

}

void Configure(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();

    //app.UseAuthorization();

}