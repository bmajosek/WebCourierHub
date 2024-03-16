
namespace WebCourierApi.Model.DTO
{
    public readonly record struct PricingDTO
    {
        public required decimal Base { get; init; }
        public required decimal Taxes { get; init; }
        public required decimal Fees { get; init; }
        public required string Currency { get; init; }
        public readonly decimal Total => Base + Taxes + Fees;
    }
}
