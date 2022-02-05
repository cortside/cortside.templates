using System;
using System.Threading.Tasks;
using Acme.WebApiStarter.UserClient.Models.Responses;

namespace Acme.WebApiStarter.UserClient {
    public interface ICatalogClient {
        Task<CatalogItemResponse> GetUserByIdAsync(Guid userId);
    }
}
