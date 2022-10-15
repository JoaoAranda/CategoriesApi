using CategoriesApi.Configurations.Data;
using CategoriesApi.Models.Categories;
using CategoriesApi.Models.Filters;
using CategoriesApi.Services.Categories.ICategories;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CategoriesApi.Services.Categories
{
    public class CategoryService : ICategoryService
    {

        private readonly IDbContext _dbContext;

        public CategoryService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Category> Get(CategoryFilter filter)
        {
            try
            {
                using (var conexao = _dbContext.CreateConnection())
                {
                    var categorias = conexao.Query<Category>(
                    sql: "SELECT Id, Name, Slug, ParentId, Visible FROM Categories WITH (NOLOCK) WHERE (@Id IS NULL OR Id LIKE @Id) AND (@Slug IS NULL OR Slug LIKE @Slug)",
                    param: new
                    {
                        filter.Id,
                        filter.Slug
                    }).ToList();

                    return categorias;
                }
            }
            catch
            {
                return new List<Category>();
            }
        }

        public CategoryDetail Detail(CategoryFilter filter)
        {
            try
            {

                using (var connection = _dbContext.CreateConnection())
                {
                    var category = connection.Query<Category>(
                    sql: "SELECT Id, Name, Slug, ParentId, Visible FROM Categories WITH (NOLOCK) WHERE (Id LIKE @Id) OR (Slug LIKE @Slug)",
                    param: new
                    {
                        filter.Id,
                        filter.Slug
                    }).FirstOrDefault();

                    if (!string.IsNullOrEmpty(category?.ParentId))
                    {
                        var parent = connection.Query<Category>(
                        sql: "SELECT Id, Name, Slug, ParentId, Visible FROM Categories WITH (NOLOCK) WHERE (Id LIKE @Id)",
                        param: new
                        {
                            Id = category.ParentId,
                        }).FirstOrDefault();

                        return new CategoryDetail(category, parent);
                    }

                    return new CategoryDetail(category, null);
                }

            }
            catch
            {
                return null;
            }
        }

        public IList<Category> CategoryChildren(string id)
        {
            try
            {
                using (var conexao = _dbContext.CreateConnection())
                {
                    var categorias = conexao.Query<Category>(
                    sql: "SELECT Id, Name, Slug, ParentId, Visible FROM Categories WITH (NOLOCK) WHERE ParentId LIKE @ParentId",
                    param: new
                    {
                        ParentId = id
                    }).ToList();

                    return categorias;
                }
            }
            catch
            {
                return new List<Category>();
            }

        }

        public bool Create(Category category)
        {
            try
            {
                var sucess = 0;
                using (var conexao = _dbContext.CreateConnection())
                {
                    sucess = conexao.Execute(
                    sql: "INSERT INTO Categories (Id, [Name], Slug, ParentId, Visible) VALUES (@Id, @Name, @Slug, @ParentId, @Visible)",
                    param: new
                    {
                        category.Id,
                        category.Name,
                        category.Slug,
                        category.ParentId,
                        category.Visible
                    });

                }

                return sucess == 1;
            }
            catch
            {
                return false;
            }
        }

        public Boolean Delete(string id)
        {
            try
            {
                var sucess = 0;
                using (var conexao = _dbContext.CreateConnection())
                {
                    sucess = conexao.Execute(
                    sql: "DELETE FROM Categories WHERE Id LIKE @Id",
                    param: new
                    {
                        Id = id
                    });
                }

                return sucess == 1;
            }
            catch
            {
                return false;
            }
        }

        public bool Visible(string id, bool visible)
        {
            try
            {
                var sucess = 0;
                using (var conexao = _dbContext.CreateConnection())
                {
                    sucess = conexao.Execute(
                    sql: "UPDATE Categories SET Visible = @Visible WHERE Id LIKE @Id",
                    param: new
                    {
                        Id = id,
                        Visible = visible
                    });
                }

                return sucess == 1;
            }
            catch
            {
                return false;
            }
        }
    }
}
