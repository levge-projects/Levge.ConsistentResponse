namespace Levge.ConsistentResponse.Models
{
    public class FilterOption
    {
        public string Field { get; set; } = null!;
        public string Operator { get; set; } = null!;
        public List<string> Values { get; set; } = new();
    }
}
