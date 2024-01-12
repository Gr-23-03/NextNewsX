using NextNews.Data;

namespace NextNews.Services
{
    public class StatisticService:IStatisticService
    {
        public readonly IArticleService articleService;
        private readonly ApplicationDbContext _context;

        private readonly IUserService _userService;
        private readonly IArticleService _articleService;

        public StatisticService(IUserService userService, IArticleService articleService)
        {
            _userService = userService;
            _articleService = articleService;
        }

        public int GetUserCount()
        {
            return _userService.GetUsers().Count();
        }

        public int GetArticleCount()
        {
            return _articleService.GetArticles().Count();
        }


    }
}
