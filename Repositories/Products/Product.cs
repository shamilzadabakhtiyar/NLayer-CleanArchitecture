using App.Repositories.Categories;

namespace App.Repositories.Products
{
    public class Product: BaseEntity<int>, IAuditEntity
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
    }
}
