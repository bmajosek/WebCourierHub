using WebCourierApi.Model.Entities;
using WebCourierApi.Utils.DynamicQuery.Queries;

namespace WebCourierApi.Data.Repositories
{
    public interface IDeliveryRepository
    {
        public Task<IEnumerable<Delivery>> FindAll(DeliveryDynamicQuery query);
        public Task<Delivery> FindById(int id);
        public Task Delete(int id);
        public Task Save(Delivery deliveryRequest);
    }
}
