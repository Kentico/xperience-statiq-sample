using System.Threading.Tasks;
using CMS.CustomTables.Types.Statiq;
using CMS.DocumentEngine.Types.Statiq;
using Statiq.Common;

namespace StatiqGenerator
{
    class BookPipeline : XperienceContentPipeline<Book>
    {
        public BookPipeline()
        {
            Dependencies.Add(nameof(RatingPipeline));
            Query = BookProvider.GetBooks();
            ReadPath = "content/book.cshtml";
            DestinationPath = Config.FromDocument((doc, ctx) =>
            {
                var book = XperienceDocumentConverter.ToTreeNode<Book>(doc);
                return new NormalizedPath($"books/{book.NodeAlias}.html".ToLower());
            });
            WithModel = Config.FromDocument((doc, context) => {
                var book = XperienceDocumentConverter.ToTreeNode<Book>(doc);
                var allRatings = context.Outputs.FromPipeline(nameof(RatingPipeline)).ParallelSelectAsync(doc =>
                    Task.Run(() => XperienceDocumentConverter.ToCustomTableItem<RatingsItem>(doc, RatingsItem.CLASS_NAME)));
                return new BookWithReviews(book, allRatings.Result);
            });
        }
    }
}
