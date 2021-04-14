using CMS.DocumentEngine.Types.Statiq;
using Statiq.Common;

namespace StatiqGenerator
{
    class BookPipeline : XperienceContentPipeline<Book>
    {
        public BookPipeline()
        {
            Query = BookProvider.GetBooks();
            ReadPath = "content/book.cshtml";
            DestinationPath = Config.FromDocument((doc, ctx) =>
            {
                var book = XperienceDocumentConverter.ToTreeNode<Book>(doc);
                return new NormalizedPath($"books/{book.Title}.html");
            });
        }
    }
}
