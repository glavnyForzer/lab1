using Entities.Models;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryBoatExtensions
    {

        public static IQueryable<Boat> FilterBoat(this IQueryable<Boat> boats, string firstBoatBrend, string lastBoatBrend) =>
                boats.Where(e => (e.Brend[0] >= firstBoatBrend[0] && e.Brend[0] <= lastBoatBrend[0]));
        public static IQueryable<Boat> Search(this IQueryable<Boat> boats, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return boats;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return boats.Where(e => e.Brend.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Boat> Sort(this IQueryable<Boat> boats, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return boats.OrderBy(e => e.Brend);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Boat>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return boats.OrderBy(e => e.Brend);
            return boats.OrderBy(orderQuery);
        }
    }
}