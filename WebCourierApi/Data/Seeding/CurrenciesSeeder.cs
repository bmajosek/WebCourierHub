using WebCourierApi.Model.Enums;
using WebCourierApi.Model.POCO;

namespace WebCourierApi.Data.Seeding
{
    public class CurrenciesSeeder : ISeeder<CurrencyPOCO>
    {
        public IEnumerable<CurrencyPOCO> Seeds =>
        [
            new CurrencyPOCO { Id = Currency.ALL, ShortName = "ALL" },
            new CurrencyPOCO { Id = Currency.AMD, ShortName = "AMD" },
            new CurrencyPOCO { Id = Currency.AZN, ShortName = "AZN" },
            new CurrencyPOCO { Id = Currency.BAM, ShortName = "BAM" },
            new CurrencyPOCO { Id = Currency.BGN, ShortName = "BGN" },
            new CurrencyPOCO { Id = Currency.BYN, ShortName = "BYN" },
            new CurrencyPOCO { Id = Currency.CHF, ShortName = "CHF" },
            new CurrencyPOCO { Id = Currency.CZK, ShortName = "CZK" },
            new CurrencyPOCO { Id = Currency.DKK, ShortName = "DKK" },
            new CurrencyPOCO { Id = Currency.EUR, ShortName = "EUR" },
            new CurrencyPOCO { Id = Currency.GBP, ShortName = "GBP" },
            new CurrencyPOCO { Id = Currency.GEL, ShortName = "GEL" },
            new CurrencyPOCO { Id = Currency.HUF, ShortName = "HUF" },
            new CurrencyPOCO { Id = Currency.ISK, ShortName = "ISK" },
            new CurrencyPOCO { Id = Currency.MDL, ShortName = "MDL" },
            new CurrencyPOCO { Id = Currency.MKD, ShortName = "MKD" },
            new CurrencyPOCO { Id = Currency.NOK, ShortName = "NOK" },
            new CurrencyPOCO { Id = Currency.PLN, ShortName = "PLN" },
            new CurrencyPOCO { Id = Currency.RON, ShortName = "RON" },
            new CurrencyPOCO { Id = Currency.RSD, ShortName = "RSD" },
            new CurrencyPOCO { Id = Currency.RUB, ShortName = "RUB" },
            new CurrencyPOCO { Id = Currency.SEK, ShortName = "SEK" },
            new CurrencyPOCO { Id = Currency.TRY, ShortName = "TRY" },
            new CurrencyPOCO { Id = Currency.UAH, ShortName = "UAH" },
            new CurrencyPOCO { Id = Currency.USD, ShortName = "USD" }
        ];
    }
}
