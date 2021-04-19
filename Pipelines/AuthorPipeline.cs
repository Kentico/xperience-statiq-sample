using System.Threading.Tasks;
using CMS.DocumentEngine.Types.Statiq;
using Statiq.Common;

namespace StatiqGenerator
{
    class AuthorPipeline : XperienceContentPipeline<Author>
    {
        public AuthorPipeline()
        {
            Dependencies.Add(nameof(BookPipeline));
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
                    Task.Run(() => XperienceDocumentConverter.ToTreeNode<Book>(doc)));
                return new AuthorWithBooks(author, allBooks.Result);
            });
        }
    }
}
