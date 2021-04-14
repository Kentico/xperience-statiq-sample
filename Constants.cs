using CMS.DocumentEngine.Types.Statiq;
using Statiq.App;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatiqGenerator
{
    public class Constants
    {
        public const string AttachmentPath = "/assets/attachments/";
        public const string AuthorPipeline = "Authors";
        public const string BookPipeline = "Books";

        public static Dictionary<string, IPipeline> Pipelines = new Dictionary<string, IPipeline>() {
            { AuthorPipeline,
                new XperienceContentPipeline<Author>()
                {
                    Query = AuthorProvider.GetAuthors(),
                    ReadPath = "content/author.cshtml",
                    DestinationPath = Config.FromDocument((doc, ctx) =>
                    {
                        var author = XperienceDocumentConverter.ToTreeNode<Author>(doc);
                        return new NormalizedPath($"authors/{author.FirstName}_{author.LastName}.html");
                    })
                }
            },
            { BookPipeline,
                new XperienceContentPipeline<Book>()
                {
                    Query = BookProvider.GetBooks(),
                    ReadPath = "content/book.cshtml",
                    DestinationPath = Config.FromDocument((doc, ctx) =>
                    {
                        var book = XperienceDocumentConverter.ToTreeNode<Book>(doc);
                        return new NormalizedPath($"books/{book.Title}.html");
                    })
                }
            }
        };
    }
}
