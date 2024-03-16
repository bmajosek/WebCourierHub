using WebCourierApi.Model.Enums;

namespace WebCourierApi.Model.ValueObjects
{
    public readonly record struct Pricing
    {
        public required decimal Base { get; init; }
        public required decimal Taxes { get; init; }
        public required decimal Fees { get; init; }
        public readonly decimal Total => Base + Taxes + Fees;
        public required Currency CurrencyId { get; init; }
        public required string Currency { get; init; }
    }
}
