using BusinessDomain.Contracts;
using BusinessDomain;
using CommonDataModels.DataModels;
using CommonDataModels.MongoDataModels;
using DataAccess.DbContexts;
using DataAccess.MongoDataAccess;
using DataAccess.Repositories.Contracts;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Tests.Utils
{
    internal static class MyServiceProvider
    {
        private const string slDatabaseString = "Server=DESKTOP-QSOECB8;Database=MyBankDB;User Id=sqlserveruser;Password=megustan;TrustServerCertificate=True";
        private const string mongoDbName = "MyBankDB";
        private const string mongoDatabaseString = "mongodb://localhost:27017";
        private static IServiceProvider Provider()
        {
           var services = new ServiceCollection();

            //Base de datos
            var dbContextOptions = new DbContextOptionsBuilder<MyBankSQLDBContext>()
                .UseSqlServer(slDatabaseString, sqlServerOptionsAction =>
                {
                    // Habilitar la resiliencia a errores transitorios
                    sqlServerOptionsAction.EnableRetryOnFailure();
                }).Options;

            services.AddSingleton(dbContextOptions); //<---------------
            services.AddScoped<MyBankSQLDBContext>();

            //base de datos mongo
            services.AddSingleton<IMongoDataAccess>(prov => {
                return new MongoDataAccess(mongoDbName, mongoDatabaseString);
            });

            //repositorios
            services.AddScoped<IMongoRepository<TransactionLog>, MongoGenericRepository<TransactionLog>>();
            services.AddScoped<IRepository<TypeProduct>, RepositoryGeneric<TypeProduct>>();
            services.AddScoped<IRepository<TypeStatus>, RepositoryGeneric<TypeStatus>>();
            services.AddScoped<IRepository<TypeTransaction>, RepositoryGeneric<TypeTransaction>>();
            services.AddScoped<IRepository<Client>, RepositoryGeneric<Client>>();
            services.AddScoped<IRepository<Person>, RepositoryGeneric<Person>>();
            services.AddScoped<IRepository<Product>, RepositoryGeneric<Product>>();
            services.AddScoped<IRepository<MoneyAccount>, RepositoryGeneric<MoneyAccount>>();
            services.AddScoped<IRepository<NoveltyTransaction>, RepositoryGeneric<NoveltyTransaction>>();
            services.AddScoped<IRepository<NoveltyTransactionDetail>, RepositoryGeneric<NoveltyTransactionDetail>>();

            //domain
            services.AddScoped<ITransactionDomain, CheckingAccountDomain>();

            return services.BuildServiceProvider();
        }

        internal static T GetRequiredService<T>()
        {
            var provider = Provider();
            return provider.GetRequiredService<T>();
        }
    }
    
}
