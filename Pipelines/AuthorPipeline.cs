using System.Linq;
using System.Threading.Tasks;
using CMS.CustomTables.Types.Statiq;
using CMS.DocumentEngine.Types.Statiq;
using Statiq.Common;

namespace StatiqGenerator
{
    class AuthorPipeline : XperienceContentPipeline<Author>
    {
        public AuthorPipeline()
        {
            Dependencies.AddRange(nameof(BookPipeline), nameof(RatingPipeline));
            Query = AuthorProvider.GetAuthors();
            ReadPath = "content/author.cshtml";
            DestinationPath = Config.FromDocument((doc, ctx) =>
            {
                var author = XperienceDocumentConverter.ToTreeNode<Author>(doc);
                return new NormalizedPath($"authors/{author.FirstName}_{author.LastName}.html".ToLower());
            });
            WithModel = Config.FromDocument((doc, context) => {
                var author = XperienceDocumentConverter.ToTreeNode<Author>(doc);
                var allBooks = context.Outputs.FromPipeline(nameof(BookPipeline)).ParallelSelectAsync(doc =>
                        Task.Run(() => XperienceDocumentConverter.ToTreeNode<Book>(doc))).Result;
                var allRatings = context.Outputs.FromPipeline(nameof(RatingPipeline)).ParallelSelectAsync(doc =>
                        Task.Run(() => XperienceDocumentConverter.ToCustomTableItem<RatingsItem>(doc, RatingsItem.CLASS_NAME)));
                var booksWithReviews = allBooks.Select(b => new BookWithReviews(b, allRatings.Result));
                
                return new AuthorWithBooks(author, booksWithReviews);
            });
        }
    }
}
