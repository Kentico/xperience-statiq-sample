using System.Collections.Generic;
using CMS.DocumentEngine.Types.Statiq;

namespace Kentico.Xperience.StatiqGenerator
{
    public class HomeViewModel
    {
        public IEnumerable<AuthorWithBooks> Authors { get; set; }
        public IEnumerable<BookWithReviews> Books { get; set; }
        public ContactUs ContactInfo { get; set; }
    }
}