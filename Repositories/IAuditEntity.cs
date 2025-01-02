namespace App.Repositories
{
    public interface IAuditEntity
    {
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
    }
}
