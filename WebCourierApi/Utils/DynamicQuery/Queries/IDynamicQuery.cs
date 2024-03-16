namespace WebCourierApi.Utils.DynamicQuery.Queries
{
    public interface IDynamicQuery
    {
        public int? Offset { get; }
        public int? Limit { get; }
        public IEnumerable<string> SortingExpressions { get; }
        public IEnumerable<string> FilteringExpressions { get; }
    }
}
