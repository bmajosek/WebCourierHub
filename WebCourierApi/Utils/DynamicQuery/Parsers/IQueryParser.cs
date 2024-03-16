using WebCourierApi.Utils.DynamicQuery.Queries;

namespace WebCourierApi.Utils.DynamicQuery.Parsers
{
    public interface IQueryParser<T>
    {
        public IQueryable<T> Apply(IDynamicQuery dynamicQuery, IQueryable<T> query);
    }
}
