using Microsoft.EntityFrameworkCore;
using WebCourierApi.Model.Enums;
using WebCourierApi.Utils.Exceptions;

namespace WebCourierApi.Data.Repositories
{
    public class PersistentNationalRepository : INationalRepository
    {
        private readonly WebCourierApiDbContext _context;

        public PersistentNationalRepository(WebCourierApiDbContext context)
        {
            _context = context;
        }

        public async Task<Currency> CountryCurrencyById(Country id)
        {
            var country = await _context.Countries.FindAsync(id) ??
                throw new ResourceNotFoundException();

            return country.CurrencyId;
        }

        public async Task<Country> CountryIdByName(string countryName)
        {
            var country = await _context.Countries
                .Where(c => c.Name == countryName)
                .SingleOrDefaultAsync() ??
                throw new ResourceNotFoundException();

            return country.Id;
        }

        public async Task<string> CountryNameById(Country id)
        {
            var country = await _context.Countries.FindAsync(id) ??
                throw new ResourceNotFoundException();

            return country.Name!;
        }

        public async Task<string> CurrencyNameById(Currency id)
        {
            var currency = await _context.Currencies.FindAsync(id) ??
                throw new ResourceNotFoundException();

            return currency.ShortName!;
        }
    }
}
