using WebCourierApi.Model.ValueObjects;

namespace WebCourierApi.Model.Entities
{
    public record Offer
    {
        public required int OfferNumber { get; init; }
        public required DateTime ValidTo { get; init; }
        public required Pricing Pricing { get; init; }
    }
}
