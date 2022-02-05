using System.Threading;
using System.Threading.Tasks;
using Acme.WebApiStarter.Domain;
using Cortside.Common.Security;
using Cortside.DomainEvent.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Acme.WebApiStarter.Data {
    public class DatabaseContext : AuditableDatabaseContext, IUnitOfWork {

        public DatabaseContext(DbContextOptions<DatabaseContext> options, ISubjectPrincipal subjectPrincipal) : base(options, subjectPrincipal) {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) {
            throw new System.NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("dbo");

            //var types = modelBuilder.Model.GetEntityTypes().Where(x=>x.ClrType.IsAssignableFrom(typeof(AuditableEntity)));
            //foreach (var t in types) {
            //    t.BaseType.HasOne()
            //}

            //// TODO: foreach AuditableEntity
            //modelBuilder.Entity<Customer>()
            //    .HasOne(p => p.CreatedSubject);
            //modelBuilder.Entity<Customer>()
            //    .HasOne(p => p.LastModifiedSubject);
            //modelBuilder.Entity<Order>()
            //    .HasOne(p => p.CreatedSubject);
            //modelBuilder.Entity<Order>()
            //    .HasOne(p => p.LastModifiedSubject);
            //modelBuilder.Entity<OrderItem>()
            //    .HasOne(p => p.CreatedSubject);
            //modelBuilder.Entity<OrderItem>()
            //    .HasOne(p => p.LastModifiedSubject);

            // TODO: foreach to do these
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
