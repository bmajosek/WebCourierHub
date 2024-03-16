using WebCourierApi.Model.POCO;

namespace WebCourierApi.Utils.DynamicQuery.Translators
{
    public class DeliverySortingQueryTranslator : IQueryTranslator<DeliveryPOCO>
    {
        private readonly Dictionary<string, Func<IQueryable<DeliveryPOCO>, IQueryable<DeliveryPOCO>>> _sortingRules = new()
        {
            { "+totalprice", query => query.OrderBy(delivery => (double)(delivery.PricingBase + delivery.PricingTaxes + delivery.PricingFees)) },
            { "-totalprice", query => query.OrderByDescending(delivery => (double)(delivery.PricingBase + delivery.PricingTaxes + delivery.PricingFees)) },
            { "+creationdate", query => query.OrderBy(delivery => delivery.CreationDate) },
            { "-creationdate", query => query.OrderByDescending(delivery => delivery.CreationDate) },
        };

        public IQueryable<DeliveryPOCO> Apply(string expression, IQueryable<DeliveryPOCO> query)
        {
            if (_sortingRules.TryGetValue(expression, out var applyFunc))
            {
                return applyFunc(query);
            }
            return query;
        }
    }
}
