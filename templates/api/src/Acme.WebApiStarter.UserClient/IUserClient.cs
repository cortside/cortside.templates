using System;
using System.Threading.Tasks;
using Acme.WebApiStarter.UserClient.Models.Responses;

namespace Acme.WebApiStarter.UserClient {
    public interface IUserClient {
        Task<UserInfoResponse> GetUserByIdAsync(Guid userId);
    }
}
