using WebCourierApi.Model.Enums;

namespace WebCourierApi.Model.ValueObjects
{
    public readonly record struct Address
    {
        public required Country CountryId { get; init; }
        public required string Country { get; init; }
        public required string ZipCode { get; init; }
        public required string Town { get; init; }
        public required string Street { get; init; }
        public required int BuildingNumber { get; init; }
        public int? ApartmentNumber { get; init; }
    }
}
