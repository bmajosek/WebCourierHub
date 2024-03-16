namespace WebCourierApi.Utils.DynamicQuery.Translators
{
    public interface IQueryTranslator<T>
    {
        public IQueryable<T> Apply(string expression, IQueryable<T> query);
    }
}
