using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyLinq
{
    public static class ComicAnalyzer
    {
        private static PriceRange CalculatePriceRange(Comic comic, IReadOnlyDictionary<int, decimal> prices)
        {
            if (prices[comic.Issue] < 100)
                return PriceRange.Cheap;
            else return PriceRange.Expansive;
        }

        public static IEnumerable<IGrouping<PriceRange,Comic>> GroupComicsByPrice(IEnumerable<Comic> comics, IReadOnlyDictionary<int,decimal> prices)
        {
            IEnumerable<IGrouping<PriceRange, Comic>> grouped =
                from comic in comics
                orderby prices[comic.Issue]
                group comic by CalculatePriceRange(comic, prices) into pricegroup
                select pricegroup;

            return grouped;
        }

        public static IEnumerable<string> GetReviews(IEnumerable<Comic> comics, IEnumerable<Review> reviews)
        {
            var joined =
                from comic in comics
                orderby comic.Issue
                join review in reviews on comic.Issue equals review.Issue
                select $"{review.Critic} ratedd #{comic.Issue} '{comic.Name}' {review.Score:0.00}";

            return joined;
        }
    }
}
