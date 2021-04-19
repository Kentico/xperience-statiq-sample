using System.Collections.Generic;
using System.Linq;
using CMS.CustomTables.Types.Statiq;
using CMS.DocumentEngine.Types.Statiq;

namespace StatiqGenerator
{
    public class BookWithReviews
    {
        public Book Book { get; private set; }
        public IEnumerable<RatingsItem> Ratings { get; private set; }

        public BookWithReviews(Book book, IEnumerable<RatingsItem> allRatings) {
            Book = book;
            Ratings = allRatings.Where(r => r.Book == book.NodeID);
        }
    }
}