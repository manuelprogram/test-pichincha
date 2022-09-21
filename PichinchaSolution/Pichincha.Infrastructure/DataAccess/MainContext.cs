namespace Pichincha.Infrastructure.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Pichincha.Infrastructure.DataAccess.Interfaces;
    using Pichincha.Infrastructure.Models;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class MainContext : DbContext, ISqlDataContext
    {
        private readonly IConfiguration configuration;

        public MainContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountType> AccountType { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Movement> Movement { get; set; }
        public virtual DbSet<MovementType> MovementType { get; set; }
        public virtual DbSet<Person> Person { get; set; }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public DbSet<TEntity> Query<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration["ConnectionStrings:SqlServer"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_AccountType");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__ClientI__48CFD27E");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_Client_Table_Person");
            });

            modelBuilder.Entity<Movement>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Movement)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Account");

                entity.HasOne(d => d.MovementType)
                    .WithMany(p => p.Movement)
                    .HasForeignKey(d => d.MovementTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_MovementType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
