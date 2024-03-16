using WebCourierApi.Model.POCO;

namespace WebCourierApi.Utils.DynamicQuery.Translators
{
    public class InquireFilteringQueryTranslator : IQueryTranslator<InquirePOCO>
    {
        private readonly Dictionary<string, Func<IQueryable<InquirePOCO>, IQueryable<InquirePOCO>>> _filteringRules = new() 
        {
            { "highpriority=true", query => query.Where(inquire => inquire.DeliveryOptions.HighPriority == true) },
            { "highpriority=false", query => query.Where(inquire => inquire.DeliveryOptions.HighPriority == false) },
            { "forcompany=true", query => query.Where(inquire => inquire.DeliveryOptions.IsForCompany == true) },
            { "forcompany=false", query => query.Where(inquire => inquire.DeliveryOptions.IsForCompany == false) },
            { "weekenddelivery=true", query => query.Where(inquire => inquire.DeliveryOptions.WeekendDelivery == true) },
            { "weekenddelivery=false", query => query.Where(inquire => inquire.DeliveryOptions.WeekendDelivery == false) }
        };

        public IQueryable<InquirePOCO> Apply(string expression, IQueryable<InquirePOCO> query)
        {
            if (_filteringRules.TryGetValue(expression, out var applyFunc))
            {
                return applyFunc(query);
            }
            return query;
        }
    }
}
