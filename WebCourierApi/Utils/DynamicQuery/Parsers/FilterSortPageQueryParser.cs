using WebCourierApi.Utils.DynamicQuery.Queries;
using WebCourierApi.Utils.DynamicQuery.Translators;

namespace WebCourierApi.Utils.DynamicQuery.Parsers
{
    public class FilterSortPageQueryParser<T> : IQueryParser<T>
    {
        private readonly IQueryTranslator<T> _filteringTranslator, _sortingTranslator;

        public FilterSortPageQueryParser(
            [FromKeyedServices("filtering")] IQueryTranslator<T> filteringTranslator,
            [FromKeyedServices("sorting")] IQueryTranslator<T> sortingTranslator
        )
        {
            _filteringTranslator = filteringTranslator;
            _sortingTranslator = sortingTranslator;
        }

        public IQueryable<T> Apply(IDynamicQuery dynamicQuery, IQueryable<T> query)
        {
            foreach (var filteringExpression in dynamicQuery.FilteringExpressions)
            {
                query = _filteringTranslator.Apply(filteringExpression.ToLower(), query);
            }
            foreach (var sortingExpression in dynamicQuery.SortingExpressions.Reverse())
            {
                query = _sortingTranslator.Apply(sortingExpression.ToLower(), query);
            }
            if (dynamicQuery.Offset.HasValue) query = query.Skip(dynamicQuery.Offset.Value);
            if (dynamicQuery.Limit.HasValue) query = query.Take(dynamicQuery.Limit.Value);

            return query;
        }
    }
}
