using WebCourierApi.Model.Enums;
using WebCourierApi.Model.POCO;

namespace WebCourierApi.Data.Seeding
{
    public class CountriesSeeder : ISeeder<CountryPOCO>
    {
        public IEnumerable<CountryPOCO> Seeds =>
        [
            new CountryPOCO { Id = Country.Albania, Name = "Albania", CurrencyId = Currency.ALL },
            new CountryPOCO { Id = Country.Andorra, Name = "Andorra", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Armenia, Name = "Armenia", CurrencyId = Currency.AMD },
            new CountryPOCO { Id = Country.Austria, Name = "Austria", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Azerbaijan, Name = "Azerbaijan", CurrencyId = Currency.AZN },
            new CountryPOCO { Id = Country.Belarus, Name = "Belarus", CurrencyId = Currency.BYN },
            new CountryPOCO { Id = Country.Belgium, Name = "Belgium", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.BosniaAndHerzegovina, Name = "Bosnia and Herzegovina", CurrencyId = Currency.BAM },
            new CountryPOCO { Id = Country.Bulgaria, Name = "Bulgaria", CurrencyId = Currency.BGN },
            new CountryPOCO { Id = Country.Croatia, Name = "Croatia", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Cyprus, Name = "Cyprus", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.CzechRepublic, Name = "Czech Republic", CurrencyId = Currency.CZK },
            new CountryPOCO { Id = Country.Denmark, Name = "Denmark", CurrencyId = Currency.DKK },
            new CountryPOCO { Id = Country.Estonia, Name = "Estonia", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Finland, Name = "Finland", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.France, Name = "France", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Georgia, Name = "Georgia", CurrencyId = Currency.GEL },
            new CountryPOCO { Id = Country.Germany, Name = "Germany", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Greece, Name = "Greece", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Hungary, Name = "Hungary", CurrencyId = Currency.HUF },
            new CountryPOCO { Id = Country.Iceland, Name = "Iceland", CurrencyId = Currency.ISK },
            new CountryPOCO { Id = Country.Ireland, Name = "Ireland", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Italy, Name = "Italy", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Latvia, Name = "Latvia", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Liechtenstein, Name = "Liechtenstein", CurrencyId = Currency.CHF },
            new CountryPOCO { Id = Country.Lithuania, Name = "Lithuania", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Luxembourg, Name = "Luxembourg", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Malta, Name = "Malta", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Moldova, Name = "Moldova", CurrencyId = Currency.MDL },
            new CountryPOCO { Id = Country.Monaco, Name = "Monaco", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Montenegro, Name = "Montenegro", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Netherlands, Name = "Netherlands", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.NorthMacedonia, Name = "North Macedonia", CurrencyId = Currency.MKD },
            new CountryPOCO { Id = Country.Norway, Name = "Norway", CurrencyId = Currency.NOK },
            new CountryPOCO { Id = Country.Poland, Name = "Poland", CurrencyId = Currency.PLN },
            new CountryPOCO { Id = Country.Portugal, Name = "Portugal", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Romania, Name = "Romania", CurrencyId = Currency.RON },
            new CountryPOCO { Id = Country.Russia, Name = "Russia", CurrencyId = Currency.RUB },
            new CountryPOCO { Id = Country.SanMarino, Name = "San Marino", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Serbia, Name = "Serbia", CurrencyId = Currency.RSD },
            new CountryPOCO { Id = Country.Slovakia, Name = "Slovakia", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Slovenia, Name = "Slovenia", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Spain, Name = "Spain", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.Sweden, Name = "Sweden", CurrencyId = Currency.SEK },
            new CountryPOCO { Id = Country.Switzerland, Name = "Switzerland", CurrencyId = Currency.CHF },
            new CountryPOCO { Id = Country.Turkey, Name = "Turkey", CurrencyId = Currency.TRY },
            new CountryPOCO { Id = Country.Ukraine, Name = "Ukraine", CurrencyId = Currency.UAH },
            new CountryPOCO { Id = Country.UnitedKingdom, Name = "United Kingdom", CurrencyId = Currency.GBP },
            new CountryPOCO { Id = Country.VaticanCity, Name = "Vatican City", CurrencyId = Currency.EUR },
            new CountryPOCO { Id = Country.UnitedStates, Name = "United States", CurrencyId = Currency.USD }
        ];
    }
}
