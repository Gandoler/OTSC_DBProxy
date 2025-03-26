using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;
using ProxyAPILeval.DTOExample;
using ProxyAPILeval.DTOExample.ForgotPassword;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using UseCases;
using UseCases.Profiles;
using UseCases.Repositoties;
using UseCases.Services;

var builder = WebApplication.CreateBuilder(args);

string connectionString = String.Empty;

if (builder.Environment.IsDevelopment())
{
    // тут настройки для дефолтного запуска без докера
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(5010);  // Указываем порт 5000
    });
 builder.Services.AddDbContext<ApplicationContext>(options => 
     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
    var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
    var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "app";
    var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
    var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "password";

    connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";

    builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseNpgsql(connectionString));
}

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();


#region swagger swagerovich
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<LoginDtoExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<RegisterTgDtoExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<FriendDtoExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<DeleteFriendDtoExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<AddIntAndPozhDtoExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<FriendUpdateExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<SetPozdrIdExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<GetPozdIdInTgExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<GetCongrStringExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<PozdrikstringExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<RegisterInAppExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<CheckExistByMailExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ADDMaiExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ChangePasswordExample>();
#endregion


builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMailBotService, MailBotService>(); 
builder.Services.AddScoped<INeiroGenService, NeiroGenService>();
builder.Services.AddScoped<IPasswordRecoveryService, PasswordRecoveryService>();
builder.Services.AddScoped<ITgBotService, TgBotService>();
builder.Services.AddScoped<IRegistrService, RegistrService>();
builder.Services.AddScoped<ITgSubscriptionService, TgSubscriptionService>();


var mapperConfig = new MapperConfiguration(cfg =>
{//
    cfg.AddProfile(new FriendProfile()); 
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() 
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddScoped<IPozdrikRepository, PozdrikRepository>();
builder.Services.AddScoped<ITgComprRepository, TgComprRepository>();
builder.Services.AddScoped<IMailComprRepository, MailComprRepository>();


Log.Information(connectionString);

builder.Services.AddControllers();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapControllers();
}
app.MapOpenApi();
app.MapControllers();
app.UseHttpsRedirection();


app.Run();
