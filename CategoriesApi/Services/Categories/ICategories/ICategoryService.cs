using CategoriesApi.Models.Categories;
using CategoriesApi.Models.Filters;

namespace CategoriesApi.Services.Categories.ICategories
{
    public interface ICategoryService
    {
        IList<Category> Get(CategoryFilter filter);
        CategoryDetail Detail(CategoryFilter filter);
        IList<Category> CategoryChildren(string id);
        bool Create(Category category);
        bool Visible(string id, bool visible);
        bool Delete(string id);

    }
}
