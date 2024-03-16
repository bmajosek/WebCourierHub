using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebCourierApi.Data.Repositories;
using WebCourierApi.Model.Entities;
using WebCourierApi.Model.ValueObjects;
using WebCourierApi.Model.POCO;
using WebCourierApi.Utils.ApiKey.ResourceGuard;
using WebCourierApi.Utils.Exceptions;
using WebCourierApi.Utils.DynamicQuery.Queries;
using WebCourierApi.Utils.DynamicQuery.Parsers;

namespace WebCourierApi.Data
{
    public class PersistentInquireRepository : IInquireRepository
    {
        private readonly WebCourierApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IQueryParser<InquirePOCO> _queryParser;
        private readonly IResourceGuard<InquirePOCO, string> _resourceGuard;
        public PersistentInquireRepository(WebCourierApiDbContext context, IMapper mapper, IQueryParser<InquirePOCO> queryParser, IResourceGuard<InquirePOCO, string> resourceGuard)
        {
            _context = context;
            _mapper = mapper;
            _queryParser = queryParser;
            _resourceGuard = resourceGuard;
        }

        public async Task<IEnumerable<Inquire>> FindAll(InquireDynamicQuery inquireDynamicQuery)
        {
            var query = _context.Inquiries.AsNoTracking();

            query = _resourceGuard.FilterInaccessibleOut(query, inquire => inquire.OwnerKey!);
            query = _queryParser.Apply(inquireDynamicQuery, query);

            var inquireList = await query.ToListAsync();
            return _mapper.Map<List<Inquire>>(inquireList);
        }

        public async Task<Inquire> FindById(int id)
        {
            var inquirePOCO = await _context.Inquiries
                .AsNoTracking()
                .Where(inquire => inquire.Id == id)
                .SingleOrDefaultAsync() ?? 
                throw new ResourceNotFoundException();

            if (!_resourceGuard.HasAccess(inquirePOCO.OwnerKey!)) 
                throw new AccessForbiddenException();

            return _mapper.Map<Inquire>(inquirePOCO);
        }

        public async Task<Inquire> Add(Inquire inquire)
        {
            var inquirePOCO = _mapper.Map<InquirePOCO>(inquire);
            inquirePOCO.OwnerKey = _resourceGuard.CurrentOwnerId;
            if (string.IsNullOrEmpty(inquirePOCO.OwnerKey)) 
                throw new UnexpectedErrorException();
            inquirePOCO.CreationDate = DateTime.Now;

            await _context.Inquiries.AddAsync(inquirePOCO);
            await _context.SaveChangesAsync();

            await _context.Entry(inquirePOCO).Reference(i => i.PickupCountry).LoadAsync();
            await _context.Entry(inquirePOCO).Reference(i => i.DeliveryCountry).LoadAsync();

            return _mapper.Map<Inquire>(inquirePOCO);
        }

        public async Task Delete(int id)
        {
            var inquire = await _context.Inquiries.FindAsync(id) ??
                throw new ResourceNotFoundException();

            if (!_resourceGuard.HasAccess(inquire.OwnerKey!))
                throw new AccessForbiddenException();

            _context.Inquiries.Remove(inquire);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Offer>> AddOffers(Inquire inquire, IEnumerable<Offer> offers)
        {
            var inquirePOCO = await _context.Inquiries.FindAsync(inquire.Id) ?? 
                throw new ResourceNotFoundException();

            if (!_resourceGuard.HasAccess(inquirePOCO.OwnerKey!))
                throw new AccessForbiddenException();

            inquirePOCO.Offers = _mapper.Map<List<OfferPOCO>>(offers);

            await _context.SaveChangesAsync();

            foreach (var offer in inquirePOCO.Offers)
                await _context.Entry(offer).Reference(offer => offer.PricingCurrency).LoadAsync();

            return _mapper.Map<List<Offer>>(inquirePOCO.Offers);
        }

        public async Task<IEnumerable<Offer>> GetOffers(int id)
        {
            var inquirePOCO = await _context.Inquiries
                .AsNoTracking()
                .Where(inquire => inquire.Id == id)
                .SingleOrDefaultAsync() ?? 
                throw new ResourceNotFoundException();

            if (!string.IsNullOrEmpty(inquirePOCO.OwnerKey) && !_resourceGuard.HasAccess(inquirePOCO.OwnerKey))
                throw new AccessForbiddenException();

            return _mapper.Map<List<Offer>>(inquirePOCO.Offers);
        }

        public async Task<Delivery> PickOffer(int id, int offerNumber, Client client)
        {
            var inquirePOCO = await _context.Inquiries.FindAsync(id) ??
                throw new ResourceNotFoundException();

            var picked = inquirePOCO.Offers.SingleOrDefault(offer => offer.OfferNumber == offerNumber) ??
                throw new ResourceNotFoundException();

            if (!_resourceGuard.HasAccess(inquirePOCO.OwnerKey!))
                throw new AccessForbiddenException();

            var hasDeliveryRequest = await _context.Deliveries
                .Where(delivery => delivery.InquireId == id)
                .AnyAsync();

            if (hasDeliveryRequest) 
                throw new InvalidActionException($"Inquire with id: {id} already has been used to create a delivery request.");

            var now = DateTime.Now;
            var requestPOCO = new DeliveryPOCO()
            {
                Inquire = inquirePOCO,
                CreationDate = now,
                ModificationDate = now,
                PricingBase = picked.PricingBase, 
                PricingTaxes = picked.PricingTaxes, 
                PricingFees = picked.PricingFees, 
                PricingCurrencyId = picked.PricingCurrencyId, 
                Client = client,
                IsPending = true, 
                Process = null
            };

            inquirePOCO.Offers.Clear();
            await _context.Deliveries.AddAsync(requestPOCO);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<Delivery>(requestPOCO);
        }
    }
}
