using lar_tech.Domain.Enums;

namespace lar_tech.Data.Handlers
{
    public static class OrderingHandler
    {

        public static IEnumerable<T> OrderList<T>(this IEnumerable<T> list, string orderByProperty, OrderBy? order = OrderBy.Asc)
        {
            //Checks if the order is descending or ascending
            if (order == OrderBy.Desc) list = list.OrderByDescending(p => p.GetType().GetProperty(orderByProperty)?.GetValue(p, null));
            else list = list.OrderBy(p => p.GetType().GetProperty(orderByProperty)?.GetValue(p, null));

            //Returns the result ordered
            return list;
        }
    }
}
