using Microsoft.AspNetCore.Mvc;
using NextNews.Models.Database;

namespace NextNews.Services
{
    public interface IUserService
    {

        List<User> GetUsers();
      

        Task<List<User>> GetUsersAsync();

    }
}
