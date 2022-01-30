using Cortside.Common.Security;
using Cortside.DomainEvent.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Acme.WebApiStarter.Data {
    public class DatabaseContext : AuditableDatabaseContext {

        public DatabaseContext(DbContextOptions<DatabaseContext> options, ISubjectPrincipal subjectPrincipal) : base(options, subjectPrincipal) {
        }

        public DbSet<Domain.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Domain.Customer>()
                .HasOne(p => p.CreatedSubject);
            modelBuilder.Entity<Domain.Customer>()
                .HasOne(p => p.LastModifiedSubject);
            modelBuilder.HasDefaultSchema("dbo");

            //var rebateStatusConverter = new EnumToStringConverter<RebateRequestStatus>();
            //modelBuilder
            //    .Entity<RebateRequest>()
            //    .Property(e => e.Status)
            //    .HasConversion(rebateStatusConverter);

            modelBuilder.AddDomainEventOutbox();

            SetDateTime(modelBuilder);
            SetCascadeDelete(modelBuilder);
        }
    }
}
