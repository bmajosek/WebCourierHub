using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebCourierApi.Model.Entities;
using WebCourierApi.Model.POCO;
using WebCourierApi.Utils.ApiKey.ResourceGuard;
using WebCourierApi.Utils.DynamicQuery.Parsers;
using WebCourierApi.Utils.DynamicQuery.Queries;
using WebCourierApi.Utils.Exceptions;

namespace WebCourierApi.Data.Repositories
{
    public class PersistentDeliveryRepository : IDeliveryRepository
    {
        private readonly WebCourierApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IQueryParser<DeliveryPOCO> _queryParser;
        private readonly IResourceGuard<DeliveryPOCO, string> _resourceGuard;

        public PersistentDeliveryRepository(WebCourierApiDbContext context, IMapper mapper, IQueryParser<DeliveryPOCO> queryParser, IResourceGuard<DeliveryPOCO, string> resourceGuard)
        {
            _context = context;
            _mapper = mapper;
            _queryParser = queryParser;
            _resourceGuard = resourceGuard;
        }

        public async Task<IEnumerable<Delivery>> FindAll(DeliveryDynamicQuery deliveryDynamicQuery)
        {
            var query = _context.Deliveries
                .Include(delivery => delivery.Inquire)
                .AsNoTracking();
            query = _resourceGuard.FilterInaccessibleOut(query, delivery => delivery.Inquire!.OwnerKey!);
            query = _queryParser.Apply(deliveryDynamicQuery, query);
            var deliveriesPOCO = await query.ToListAsync();

            return _mapper.Map<List<Delivery>>(deliveriesPOCO);
        }

        public async Task<Delivery> FindById(int id)
        {
            var deliveryPOCO = await _context.Deliveries
                .Include(delivery => delivery.Inquire)
                .AsNoTracking()
                .Where(delivery => delivery.Id == id)
                .SingleOrDefaultAsync() ??
                throw new ResourceNotFoundException();

            if (!_resourceGuard.HasAccess(deliveryPOCO.Inquire!.OwnerKey!))
                throw new AccessForbiddenException();

            return _mapper.Map<Delivery>(deliveryPOCO);
        }

        public async Task Delete(int id)
        {
            var deliveryPOCO = await _context.Deliveries
                .Include(delivery => delivery.Inquire)
                .Where(delivery => delivery.Id == id)
                .SingleOrDefaultAsync() ??
                throw new ResourceNotFoundException();

            if (!_resourceGuard.HasAccess(deliveryPOCO.Inquire!.OwnerKey!))
                throw new AccessForbiddenException();

            _context.Deliveries.Remove(deliveryPOCO);

            await _context.SaveChangesAsync();
        }

        public async Task Save(Delivery delivery)
        {
            var storedDeliveryPOCO = await _context.Deliveries
                .Include(delivery => delivery.Inquire)
                .AsNoTracking()
                .Where(d => d.Id == delivery.Id)
                .SingleOrDefaultAsync() ??
                throw new ResourceNotFoundException();

            if (!_resourceGuard.HasAccess(storedDeliveryPOCO.Inquire!.OwnerKey!))
                throw new AccessForbiddenException();

            var deliveryPOCO = _mapper.Map<DeliveryPOCO>(delivery);

            var deliveryEntry = _context.Entry(deliveryPOCO);
            var processEntry = deliveryEntry.Reference(d => d.Process).TargetEntry;
            deliveryEntry.State = EntityState.Modified;
            deliveryPOCO.ModificationDate = DateTime.Now;
            if (processEntry != null && deliveryPOCO.Process != null)
            {
                processEntry.State = storedDeliveryPOCO.Process == null ? EntityState.Added : EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            await deliveryEntry.Reference(d => d.PricingCurrency).LoadAsync();
            var inquireEntry = deliveryEntry.Reference(d => d.Inquire).TargetEntry;
            if (inquireEntry != null)
            {
                await inquireEntry.Reference(inquire => inquire.PickupCountry).LoadAsync();
                await inquireEntry.Reference(inquire => inquire.DeliveryCountry).LoadAsync();
            }

            _mapper.Map(deliveryPOCO, delivery);
        }
    }
}
