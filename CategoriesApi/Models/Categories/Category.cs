namespace CategoriesApi.Models.Categories
{
    public class Category
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? ParentId { get; set; }
        public bool Visible { get; set; }
    }
}
