using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.IServices;
using Infrastructure.DATA;
using Microsoft.EntityFrameworkCore;
using ProxyAPILeval.DTOExample;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using UseCases;
using UseCases.Profiles;
using UseCases.Repositoties;
using UseCases.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<LoginDtoExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<RegisterTgDtoExample>();



builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMailBotService, MailBotService>(); 
builder.Services.AddScoped<INeiroGenService, NeiroGenService>();
builder.Services.AddScoped<IPasswordRecoveryService, PasswordRecoveryService>();
builder.Services.AddScoped<ITgBotService, TgBotService>();
builder.Services.AddScoped<IRegistrService, RegistrService>();
builder.Services.AddScoped<ITgSubscriptionService, TgSubscriptionService>();


var mapperConfig = new MapperConfiguration(cfg =>
{
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



builder.Services.AddControllers();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapControllers();
}

app.UseHttpsRedirection();


app.Run();
