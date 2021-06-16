using CMS.DocumentEngine.Types.Statiq;
using System.Collections.Generic;
using System.Linq;

namespace Kentico.Xperience.StatiqGenerator
{
    public class AuthorWithBooks
    {
        public Author Author { get; private set; }
        public IEnumerable<BookWithReviews> Books { get; private set; }

        public AuthorWithBooks(Author author, IEnumerable<BookWithReviews> allBooks)
        {
            Author = author;
            Books = allBooks.Where(b => b.Book.Fields.Author.FirstOrDefault().NodeID == author.NodeID);
        }
    }
}
