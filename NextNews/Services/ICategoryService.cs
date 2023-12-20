using NextNews.Models.Database;

namespace NextNews.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();

        Task CreateCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int id);

        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);



    }
}
