using WebCourierApi.Model.Entities;
using WebCourierApi.Model.ValueObjects;
using WebCourierApi.Utils.DynamicQuery.Queries;

namespace WebCourierApi.Data.Repositories
{
    public interface IInquireRepository
    {
        public Task<IEnumerable<Inquire>> FindAll(InquireDynamicQuery query);
        public Task<Inquire> FindById(int id);
        public Task<Inquire> Add(Inquire inquire);
        public Task Delete(int id);
        public Task<IEnumerable<Offer>> AddOffers(Inquire inquire, IEnumerable<Offer> offers);
        public Task<IEnumerable<Offer>> GetOffers(int id);
        public Task<Delivery> PickOffer(int id, int offerNumber, Client client);
    }
}
