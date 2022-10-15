namespace CategoriesApi.Models.Categories
{
    public class CategoryDetail
    {
        public CategoryDetail(Category category, Category? parent)
        {
            Id = category.Id;
            Name = category.Name;
            Slug = category.Slug;
            Visible = category.Visible;
            Parent = parent;
        }

        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public bool Visible { get; set; }
        public Category? Parent { get; set; }
    }
}
