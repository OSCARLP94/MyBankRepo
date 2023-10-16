using CommonDataModels.DataModels;
using DataAccess.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbContexts
{
    public class MyBankSQLDBContext : DbContext
    {
        private readonly DbContextOptions<MyBankSQLDBContext> _dbContextOptions;

        #region DbSets
        public DbSet<TypeProduct> TypeProducts { get; set; }
        public DbSet<TypeStatus> TypeStatuses { get; set; }
        public DbSet<TypeTransaction> TypeTransactions { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<MoneyAccount> MoneyAccounts { get; set; }
        public DbSet<NoveltyTransaction> NoveltyTransactions { get; set; }
        public DbSet<NoveltyTransactionDetail> NoveltyTransactionDetails { get; set; }
        #endregion

        public MyBankSQLDBContext(DbContextOptions<MyBankSQLDBContext> options) : base(options)
        {
            _dbContextOptions = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TypeProductMapper());
            modelBuilder.ApplyConfiguration(new TypeStatusMapper());
            modelBuilder.ApplyConfiguration(new TypeTransactionMapper());
            modelBuilder.ApplyConfiguration(new PersonMapper());
            modelBuilder.ApplyConfiguration(new ClientMapper());
            modelBuilder.ApplyConfiguration(new ProductMapper());
            modelBuilder.ApplyConfiguration(new MoneyAccountMapper());
            modelBuilder.ApplyConfiguration(new NoveltyTransactionMapper());
            modelBuilder.ApplyConfiguration(new NoveltyTransactionDetailMapper());
        }
    }
}
