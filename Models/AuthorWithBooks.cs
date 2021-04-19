using CMS.DocumentEngine.Types.Statiq;
using System.Collections.Generic;
using System.Linq;

namespace StatiqGenerator
{
    public class AuthorWithBooks
    {
        public Author Author { get; set; }
        public IEnumerable<Book> Books { get; set; }

        public AuthorWithBooks(Author author, IEnumerable<Book> allBooks)
        {
            Author = author;
            Books = allBooks.Where(b => b.Fields.Author.FirstOrDefault().NodeID == author.NodeID);
        }
    }
}
