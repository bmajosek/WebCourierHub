namespace WebCourierApi.Model.ValueObjects
{
    public readonly record struct Client
    {
        public required string EmailAddress { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? CompanyName { get; init; }
    }
}
