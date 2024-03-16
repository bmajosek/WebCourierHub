
namespace WebCourierApi.Utils.DynamicQuery.Queries
{
    public class DeliveryDynamicQuery : IDynamicQuery
    {
        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public string? SortBy { get; set; }
        public string? RequestStatus { get; set; }
        public string? DeliveryStatus { get; set; }
        public IEnumerable<string> SortingExpressions => string.IsNullOrEmpty(SortBy) ? [] : SortBy.Split(',');
        public IEnumerable<string> FilteringExpressions => new List<(string, string?)>()
        {
            ("RequestStatus", RequestStatus),
            ("DeliveryStatus", DeliveryStatus)
        }.Where(pair => !string.IsNullOrEmpty(pair.Item2))
            .Select(pair => $"{pair.Item1}={pair.Item2}");
    }
}
