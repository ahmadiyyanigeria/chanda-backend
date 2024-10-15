using Application.Repositories;
using Domain.Entities;
using Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Repositories
{
    public class AppDbContext : DbContext
    {
        private ICurrentUser _currentUser;
        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUser currentUser)
        : base(options)
        { 
            _currentUser = currentUser;
        }

        public DbSet<Member> Members => Set<Member>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<MemberRole> MemberRoles => Set<MemberRole>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<ChandaType> ChandaTypes => Set<ChandaType>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
        public DbSet<Ledger> Ledgers => Set<Ledger>();
        public DbSet<Jamaat> Jamaats => Set<Jamaat>();
        public DbSet<JamaatLedger> JamaatLedgers => Set<JamaatLedger>();
        public DbSet<MemberLedger> MemberLedgers => Set<MemberLedger>();
        public DbSet<ChandaItem> ChandaItems => Set<ChandaItem>();
        public DbSet<Circuit> Circuits => Set<Circuit>();
        public DbSet<Reminder> Reminders => Set<Reminder>();
        public DbSet<AuditEntry> AuditEntries => Set<AuditEntry>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasCollation("case_insensitive", locale: "en-u-ks-primary", provider: "icu", deterministic: false);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<string>().UseCollation("case_insensitive");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new AuditInterceptor(_currentUser));
        }
    }
}
