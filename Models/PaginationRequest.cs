namespace Levge.ConsistentResponse.Models
{
    public class PaginationRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchText { get; set; }
        public List<SortOption> Sorts { get; set; } = new();
        public List<FilterOption> Filters { get; set; } = new();
    }
}
