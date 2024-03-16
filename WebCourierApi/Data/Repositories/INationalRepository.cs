using WebCourierApi.Model.Enums;

namespace WebCourierApi.Data
{
    public interface INationalRepository
    {
        public Task<string> CountryNameById(Country id);
        public Task<Country> CountryIdByName(string countryName);
        public Task<Currency> CountryCurrencyById(Country id);
        public Task<string> CurrencyNameById(Currency id);
    }
}
