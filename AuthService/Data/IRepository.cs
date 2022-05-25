using AuthService.Models;
using System;

namespace AuthService.Data
{
    public interface IRepository
    {
        Task<Auth?> GetUser(Guid guid);
        Task<Auth?> GetUser(string username);
        Task ChangePassword(string username, string newPass);
        Task ChangePassword(Guid guid, string newPass);
        Task<Auth> CreateUser(string username, string pass);
        Task DeleteUser(string username);
        Task DeleteUser(Guid guid);
        Task<int> SaveChangedAsync();
    }
}
