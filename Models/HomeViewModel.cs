using System.Collections.Generic;

namespace StatiqGenerator {
    public class HomeViewModel {
        public IEnumerable<BookWithReviews> TopThreeBooks { get; set; }
        public IEnumerable<AuthorWithBooks> Authors { get; set; }
        public IEnumerable<BookWithReviews> Books { get; set; }
    }
}