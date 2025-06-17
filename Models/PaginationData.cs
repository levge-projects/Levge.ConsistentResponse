namespace Levge.ConsistentResponse.Models
{
    public class PaginationData<T>
    {
        public List<T> Items { get; set; } = new();
        public PaginationMeta Meta { get; set; } = new();
    }
}
