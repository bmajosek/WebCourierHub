using WebCourierApi.Model.POCO;

namespace WebCourierApi.Utils.DynamicQuery.Translators
{
    public class InquireSortingQueryTranslator : IQueryTranslator<InquirePOCO>
    {
        private readonly Dictionary<string, Func<IQueryable<InquirePOCO>, IQueryable<InquirePOCO>>> _sortingRules = new()
        {
            { "+creationdate", query => query.OrderBy(inquire => inquire.CreationDate) },
            { "-creationdate", query => query.OrderByDescending(inquire => inquire.CreationDate) },
            { "+pickupdate", query => query.OrderBy(inquire => inquire.PickupDate) },
            { "-pickupdate", query => query.OrderByDescending(inquire => inquire.PickupDate) },
            { "+deliverydate", query => query.OrderBy(inquire => inquire.DeliveryDate) },
            { "-deliverydate", query => query.OrderByDescending(inquire => inquire.DeliveryDate) }
        };

        public IQueryable<InquirePOCO> Apply(string expression, IQueryable<InquirePOCO> query)
        {
            if (_sortingRules.TryGetValue(expression, out var applyFunc))
            {
                return applyFunc(query);
            }
            return query;
        }
    }
}
