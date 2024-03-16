using System.Linq.Expressions;
using WebCourierApi.Utils.ApiKey.Classifier;

namespace WebCourierApi.Utils.ApiKey.ResourceGuard
{
    public class ApiKeyResourceGuard<T> : IResourceGuard<T, string>
    {
        private readonly IApiKeyClassifier _checker;
        private readonly ApiKeyHttpContextReader _reader;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiKeyResourceGuard(IApiKeyClassifier checker, ApiKeyHttpContextReader reader, IHttpContextAccessor httpContextAccessor)
        {
            _checker = checker;
            _reader = reader;
            _httpContextAccessor = httpContextAccessor;
        }

        public string? CurrentOwnerId
        {
            get
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null) return string.Empty;

                if (_reader.TryReadValue(httpContext, out var apiKey)) return apiKey;

                return string.Empty;
            }
        }

        public IQueryable<T> FilterInaccessibleOut(IQueryable<T> query, Expression<Func<T, string>> resourceOwnerIdAccessor)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return query;

            if (_reader.TryReadValue(httpContext, out var apiKey) && _checker.IsKeyExternal(apiKey))
            {
                var equalityExpression = Expression.Lambda<Func<T, bool>>(
                    Expression.Equal(resourceOwnerIdAccessor.Body, Expression.Constant(apiKey)),
                    resourceOwnerIdAccessor.Parameters
                );
                query = query.Where(equalityExpression);
            }
            return query;
        }

        public bool HasAccess(string resourceOwnerId)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            return httpContext != null &&
                _reader.TryReadValue(httpContext, out var apiKey) &&
                (_checker.IsKeyInternal(apiKey) || apiKey == resourceOwnerId);
        }
    }
}
