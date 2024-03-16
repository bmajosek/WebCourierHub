using AutoMapper;
using WebCourierApi.Model.Entities;
using WebCourierApi.Model.ValueObjects;
using WebCourierApi.Model.DTO;
using WebCourierApi.Model.Enums;

namespace WebCourierApi.Model
{
    public class ApiDTOMapper : Profile
    {
        public const string PickupCountryIdKey = "PickupCountryId";
        public const string DeliveryCountryIdKey = "DeliveryCountryId";

        public ApiDTOMapper()
        {
            CreateMap<InquireCreationDTO, Inquire>()
                .ForMember(inquire => inquire.PickupAddress, opt => opt.MapFrom((src, dest, destMember, context) => new Address()
                {
                    CountryId = (Country)context.Items[PickupCountryIdKey],
                    Country = src.PickupAddress.Country,
                    ZipCode = src.PickupAddress.ZipCode,
                    Town = src.PickupAddress.Town,
                    Street = src.PickupAddress.Street,
                    BuildingNumber = src.PickupAddress.BuildingNumber,
                    ApartmentNumber = src.PickupAddress.ApartmentNumber
                }))
                .ForMember(inquire => inquire.DeliveryAddress, opt => opt.MapFrom((src, dest, destMember, context) => new Address()
                {
                    CountryId = (Country)context.Items[DeliveryCountryIdKey],
                    Country = src.DeliveryAddress.Country,
                    ZipCode = src.DeliveryAddress.ZipCode,
                    Town = src.DeliveryAddress.Town,
                    Street = src.DeliveryAddress.Street,
                    BuildingNumber = src.DeliveryAddress.BuildingNumber,
                    ApartmentNumber = src.DeliveryAddress.ApartmentNumber
                }));
            CreateMap<Address, AddressDTO>();
            CreateMap<Inquire, InquireDetailsDTO>();
            CreateMap<Offer, OfferDTO>();
            CreateMap<Pricing, PricingDTO>();
            CreateMap<Delivery, DeliveryDTO>();
            CreateMap<DeliveryProcess, DeliveryProcessDTO>();
        }
    }
}