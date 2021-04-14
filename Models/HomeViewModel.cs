using CMS.DocumentEngine.Types.Statiq;
using System.Collections.Generic;

namespace StatiqGenerator
{
    public class HomeViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}
