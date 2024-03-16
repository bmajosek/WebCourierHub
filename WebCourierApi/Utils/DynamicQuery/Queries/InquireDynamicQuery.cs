
namespace WebCourierApi.Utils.DynamicQuery.Queries
{
    public class InquireDynamicQuery : IDynamicQuery
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public string? SortBy { get; set; }
        public string? HighPriority { get; set; }
        public string? ForCompany { get; set; }
        public string? WeekendDelivery { get; set; }
        public IEnumerable<string> SortingExpressions => string.IsNullOrEmpty(SortBy) ? [] : SortBy.Split(',');
        public IEnumerable<string> FilteringExpressions => new List<(string, string?)>()
        {
            ("HighPriority", HighPriority),
            ("ForCompany", ForCompany), 
            ("WeekendDelivery", WeekendDelivery)
        }.Where(pair => !string.IsNullOrEmpty(pair.Item2))
            .Select(pair => $"{pair.Item1}={pair.Item2}");
    }
}
