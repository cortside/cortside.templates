﻿using System.Threading.Tasks;
using Acme.WebApiStarter.Data;

namespace Acme.WebApiStarter.WebApi.IntegrationTests.Helpers {
    public static class DatabaseFixture {
        public static async Task SeedInMemoryDb(DatabaseContext dbContext) {
            await dbContext.Subjects.SeedFromFileAsync(".\\SeedData\\Subject.csv");
            await dbContext.Widgets.SeedFromFileAsync(".\\SeedData\\Widget.csv");

            // cast to get base implementation
            //((DbContext)dbContext).SaveChanges(true);
            await dbContext.SaveChangesAsync();

            //const int goodmanId = 1234500;
            //Random random = new Random();

            //var contractor = new Contractor {
            //    ContractorId = 1, //random.Next(1, 9999),
            //    ContractorName = random.Next(1, 9999).ToString(),
            //    ContractorNumber = random.Next(1, 9999),
            //    SponsorId = random.Next(1, 9999),
            //    SponsorNumber = random.Next(1, 9999)
            //};
            //await dbContext.Contractors.AddAsync(contractor);

            //var provider = new RebateProvider() {
            //    RebateProviderId = goodmanId,
            //    RebateProviderResourceId = Guid.NewGuid(),
            //    Name = "Goodman"
            //};
            //await dbContext.RebateProviders.AddAsync(provider);

            //var request = new RebateRequest {
            //    RebateRequestId = goodmanId,
            //    ModelNumber = "12345",
            //    SerialNumber = "67890",
            //    InvoiceNumber = "1a2b3c4d",
            //    RebateProvider = provider,
            //    LoanId = Guid.NewGuid(),
            //    Status = RebateRequestStatus.Created,
            //    Contractor = contractor,
            //    CreatedSubject = subjects.First()
            //};
            //await dbContext.RebateRequests.AddAsync(request);

            //await dbContext.SaveChangesAsync();
        }
    }
}
