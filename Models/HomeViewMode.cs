using System.Collections.Generic;

namespace StatiqGenerator {
    public class HomeViewModel {
        public IEnumerable<BookWithReviews> TopThreeBooks { get; set; }
    }
}