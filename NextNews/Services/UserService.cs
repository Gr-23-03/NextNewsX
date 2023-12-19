using Microsoft.CodeAnalysis.CSharp.Syntax;
using NextNews.Data;
using NextNews.Models.Database;

namespace NextNews.Services
{
    public class UserService : IUserService
    {

        private readonly ApplicationDbContext _context;


        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }


    }
}
