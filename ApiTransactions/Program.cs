using BusinessDomain;
using BusinessDomain.Contracts;
using CommonDataModels.DataModels;
using CommonDataModels.MongoDataModels;
using DataAccess.DbContexts;
using DataAccess.MongoDataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Injeccion de dependencias
//Base de datos
var dbContextOptions = new DbContextOptionsBuilder<MyBankSQLDBContext>()
    .UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"], sqlServerOptionsAction =>
    {
        // Habilitar la resiliencia a errores transitorios
        sqlServerOptionsAction.EnableRetryOnFailure();
    }).Options;

builder.Services.AddSingleton(dbContextOptions); //<---------------
builder.Services.AddScoped<MyBankSQLDBContext>();

//base de datos mongo
builder.Services.AddSingleton<IMongoDataAccess>(prov => {
    return new MongoDataAccess(builder.Configuration["MongoLogsDatabase:DatabaseName"], builder.Configuration["MongoLogsDatabase:ConnectionString"]);
});


//repositorios
builder.Services.AddScoped<IMongoRepository<TransactionLog>, MongoGenericRepository<TransactionLog>>();

builder.Services.AddScoped<IRepository<TypeProduct>, RepositoryGeneric<TypeProduct>>();
builder.Services.AddScoped<IRepository<TypeStatus>, RepositoryGeneric<TypeStatus>>();
builder.Services.AddScoped<IRepository<TypeTransaction>, RepositoryGeneric<TypeTransaction>>();
builder.Services.AddScoped<IRepository<Client>, RepositoryGeneric<Client>>();
builder.Services.AddScoped<IRepository<Person>, RepositoryGeneric<Person>>();
builder.Services.AddScoped<IRepository<Product>, RepositoryGeneric<Product>>();
builder.Services.AddScoped<IRepository<MoneyAccount>, RepositoryGeneric<MoneyAccount>>();
builder.Services.AddScoped<IRepository<NoveltyTransaction>, RepositoryGeneric<NoveltyTransaction>>();
builder.Services.AddScoped<IRepository<NoveltyTransactionDetail>, RepositoryGeneric<NoveltyTransactionDetail>>();

//domain
builder.Services.AddScoped<ITransactionDomain, CheckingAccountDomain>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

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
