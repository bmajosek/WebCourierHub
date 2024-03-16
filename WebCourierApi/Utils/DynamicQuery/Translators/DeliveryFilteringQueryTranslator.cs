using WebCourierApi.Model.POCO;

namespace WebCourierApi.Utils.DynamicQuery.Translators
{
    public class DeliveryFilteringQueryTranslator : IQueryTranslator<DeliveryPOCO>
    {
        private readonly Dictionary<string, Func<IQueryable<DeliveryPOCO>, IQueryable<DeliveryPOCO>>> _filteringRules = new()
        {
            { "requeststatus=pending", query => query.Where(delivery => delivery.IsPending == true) },
            { "requeststatus=accepted", query => query.Where(delivery => delivery.IsPending == false && delivery.Process != null) },
            { "requeststatus=rejected", query => query.Where(delivery => delivery.IsPending == false && delivery.Process == null) },
            { "deliverystatus=started", query => query.Where(delivery => delivery.Process != null && delivery.Process.PickupTimestamp == null ) },
            { "deliverystatus=pickedup", query => query.Where(delivery => delivery.Process != null && delivery.Process.PickupTimestamp != null && delivery.Process.DeliveryTimestamp == null ) },
            { "deliverystatus=delivered", query => query.Where(delivery => delivery.Process != null && delivery.Process.PickupTimestamp != null && delivery.Process.DeliveryTimestamp != null && delivery.Process.IsDelivered ) },
            { "deliverystatus=cannotdeliver", query => query.Where(delivery => delivery.Process != null && delivery.Process.PickupTimestamp != null && delivery.Process.DeliveryTimestamp != null && !delivery.Process.IsDelivered ) },
        };

        public IQueryable<DeliveryPOCO> Apply(string expression, IQueryable<DeliveryPOCO> query)
        {
            if (_filteringRules.TryGetValue(expression, out var applyFunc))
            {
                return applyFunc(query);
            }
            return query;
        }
    }
}
