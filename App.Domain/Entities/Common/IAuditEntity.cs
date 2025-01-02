namespace App.Domain.Entities.Common
{
    public interface IAuditEntity
    {
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
    }
}
