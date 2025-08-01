using Domain.Mapper;
using Domain.Repository;
using Persistance;
using Persistance.RepositoryLogic;
using Services.Abstraction;
using Servicess;
using Servicess.FeeRuleServices;
using Microsoft.EntityFrameworkCore;
using TransactionApp.middleware;
using Domain.DTOS;
using FluentValidation;
using Domain.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITransaction, TransactionRepository>();
builder.Services.AddScoped<IFeeLogRepository, FeeLogRepository>();
builder.Services.AddScoped<IClientInfoRepository, ClientRepository>();
builder.Services.AddScoped<IRuleRepository, RuleRepository>();

builder.Services.AddScoped<IFeeCalculationService, FeeCalculationService>();
builder.Services.AddScoped<IFeeRule, CreditScoreRule>();
builder.Services.AddScoped<IFeeRule, EccommerceTransactionRule>(); 
builder.Services.AddScoped<IFeeRule, PosTransactionRule>();
builder.Services.AddScoped<IValidator<TransactionDTO>, TransactionValidator>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
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
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
