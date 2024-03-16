using AutoMapper;
using WebCourierApi.Model.Entities;
using WebCourierApi.Model.ValueObjects;
using WebCourierApi.Model.Enums;
using WebCourierApi.Model.POCO;

namespace WebCourierApi.Model
{
    public class ApiPOCOMapper : Profile
    {
        public ApiPOCOMapper()
        {
            CreateMap<Inquire, InquirePOCO>()
                .ForMember(inquirePOCO => inquirePOCO.PickupCountryId, opt => opt.MapFrom(inquire => inquire.PickupAddress.CountryId))
                .ForMember(inquirePOCO => inquirePOCO.PickupZipCode, opt => opt.MapFrom(inquire => inquire.PickupAddress.ZipCode))
                .ForMember(inquirePOCO => inquirePOCO.PickupTown, opt => opt.MapFrom(inquire => inquire.PickupAddress.Town))
                .ForMember(inquirePOCO => inquirePOCO.PickupStreet, opt => opt.MapFrom(inquire => inquire.PickupAddress.Street))
                .ForMember(inquirePOCO => inquirePOCO.PickupBuildingNumber, opt => opt.MapFrom(inquire => inquire.PickupAddress.BuildingNumber))
                .ForMember(inquirePOCO => inquirePOCO.PickupApartmentNumber, opt => opt.MapFrom(inquire => inquire.PickupAddress.ApartmentNumber))
                .ForMember(inquirePOCO => inquirePOCO.DeliveryCountryId, opt => opt.MapFrom(inquire => inquire.DeliveryAddress.CountryId))
                .ForMember(inquirePOCO => inquirePOCO.DeliveryZipCode, opt => opt.MapFrom(inquire => inquire.DeliveryAddress.ZipCode))
                .ForMember(inquirePOCO => inquirePOCO.DeliveryTown, opt => opt.MapFrom(inquire => inquire.DeliveryAddress.Town))
                .ForMember(inquirePOCO => inquirePOCO.DeliveryStreet, opt => opt.MapFrom(inquire => inquire.DeliveryAddress.Street))
                .ForMember(inquirePOCO => inquirePOCO.DeliveryBuildingNumber, opt => opt.MapFrom(inquire => inquire.DeliveryAddress.BuildingNumber))
                .ForMember(inquirePOCO => inquirePOCO.DeliveryApartmentNumber, opt => opt.MapFrom(inquire => inquire.DeliveryAddress.ApartmentNumber));
            CreateMap<InquirePOCO, Inquire>()
                .ForMember(inquire => inquire.PickupAddress, opt => opt.MapFrom(src => new Address()
                {
                    CountryId = src.PickupCountryId, 
                    Country = src.PickupCountry!.Name!, 
                    ZipCode = src.PickupZipCode!,
                    Town = src.PickupTown!,
                    Street = src.PickupStreet!,
                    BuildingNumber = src.PickupBuildingNumber,
                    ApartmentNumber = src.PickupApartmentNumber
                }))
                .ForMember(inquire => inquire.DeliveryAddress, opt => opt.MapFrom(src => new Address()
                {
                    CountryId = src.DeliveryCountryId, 
                    Country = src.DeliveryCountry!.Name!, 
                    ZipCode = src.DeliveryZipCode!,
                    Town = src.DeliveryTown!,
                    Street = src.DeliveryStreet!,
                    BuildingNumber = src.DeliveryBuildingNumber,
                    ApartmentNumber = src.DeliveryApartmentNumber
                }));
            CreateMap<Offer, OfferPOCO>()
                .ForMember(offerPOCO => offerPOCO.PricingBase, opt => opt.MapFrom(offer => offer.Pricing.Base))
                .ForMember(offerPOCO => offerPOCO.PricingTaxes, opt => opt.MapFrom(offer => offer.Pricing.Taxes))
                .ForMember(offerPOCO => offerPOCO.PricingFees, opt => opt.MapFrom(offer => offer.Pricing.Fees))
                .ForMember(offerPOCO => offerPOCO.PricingCurrencyId, opt => opt.MapFrom(offer => offer.Pricing.CurrencyId))
                .ForMember(offerPOCO => offerPOCO.PricingCurrency, opt => opt.Ignore());
            CreateMap<OfferPOCO, Offer>()
                .ForMember(offer => offer.Pricing, opt => opt.MapFrom(offerPOCO => new Pricing()
                {
                    Base = offerPOCO.PricingBase,
                    Taxes = offerPOCO.PricingTaxes,
                    Fees = offerPOCO.PricingFees,
                    CurrencyId = offerPOCO.PricingCurrencyId, 
                    Currency = offerPOCO.PricingCurrency!.ShortName!
                }));
            CreateMap<Delivery, DeliveryPOCO>()
                .ForMember(delivery => delivery.IsPending, opt => opt.MapFrom(delivery => delivery.RequestStatus == RequestStatus.Pending))
                .ForMember(delivery => delivery.PricingBase, opt => opt.MapFrom(delivery => delivery.Pricing.Base))
                .ForMember(delivery => delivery.PricingTaxes, opt => opt.MapFrom(delivery => delivery.Pricing.Taxes))
                .ForMember(delivery => delivery.PricingFees, opt => opt.MapFrom(delivery => delivery.Pricing.Fees))
                .ForMember(delivery => delivery.PricingCurrencyId, opt => opt.MapFrom(delivery => delivery.Pricing.CurrencyId))
                .ForMember(delivery => delivery.PricingCurrency, opt => opt.Ignore());
            CreateMap<DeliveryPOCO, Delivery>()
                .ForMember(delivery => delivery.RequestStatus, opt => opt.MapFrom(
                    delivery => delivery.IsPending
                        ? RequestStatus.Pending
                        : delivery.Process == null
                            ? RequestStatus.Rejected
                            : RequestStatus.Accepted
                ))
                .ForMember(delivery => delivery.Pricing, opt => opt.MapFrom(src => new Pricing()
                {
                    Base = src.PricingBase,
                    Taxes = src.PricingTaxes,
                    Fees = src.PricingFees,
                    CurrencyId = src.PricingCurrencyId,
                    Currency = src.PricingCurrency!.ShortName!
                }));
            CreateMap<DeliveryProcess, DeliveryProcessPOCO>()
                .ForMember(deliveryPOCO => deliveryPOCO.DeliveryRequestId, opt => opt.MapFrom(delivery => delivery.Id));
            CreateMap<DeliveryProcessPOCO, DeliveryProcess>()
                .ForMember(delivery => delivery.Id, opt => opt.MapFrom(delivery => delivery.DeliveryRequestId));
        }
    }
}
