namespace WebCourierApi.Model.ValueObjects
{
    public readonly record struct DeliveryOptions
    {
        public bool IsForCompany { get; init; }
        public bool HighPriority {  get; init; }
        public bool WeekendDelivery { get; init; }
    }
}
