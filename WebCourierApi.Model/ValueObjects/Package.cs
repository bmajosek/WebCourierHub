namespace WebCourierApi.Model.ValueObjects
{
    public readonly record struct Package
    {
        public required float LengthCM { get; init; }
        public required float WidthCM {  get; init; }
        public required float HeightCM {  get; init; }
        public required float WeightKG {  get; init; }
    }
}
