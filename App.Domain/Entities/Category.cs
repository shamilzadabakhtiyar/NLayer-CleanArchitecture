using App.Domain.Entities.Common;

namespace App.Domain.Entities
{
    public class Category : BaseEntity<int>, IAuditEntity
    {
        public string Name { get; set; } = default!;
        public List<Product>? Products { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
    }
}
