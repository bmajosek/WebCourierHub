namespace WebCourierApi.Data.Seeding
{
    public interface ISeeder<T>
    {
        public IEnumerable<T> Seeds { get; }
    }
}
