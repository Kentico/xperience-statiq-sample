using System.Threading.Tasks;
using CMS.CustomTables.Types.Statiq;
using CMS.DocumentEngine.Types.Statiq;
using Statiq.Common;

namespace Kentico.Xperience.StatiqGenerator
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
                return new NormalizedPath(StatiqHelper.GetBookUrl(book));
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
