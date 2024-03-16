using System.Linq.Expressions;

namespace WebCourierApi.Utils.ApiKey.ResourceGuard
{
    public interface IResourceGuard<T, ResourceOwnerIdType>
    {
        public IQueryable<T> FilterInaccessibleOut(IQueryable<T> query, Expression<Func<T, ResourceOwnerIdType>> resourceOwnerIdAccessor);
        public bool HasAccess(ResourceOwnerIdType resourceOwnerId);
        public ResourceOwnerIdType? CurrentOwnerId { get; }
    }
}
