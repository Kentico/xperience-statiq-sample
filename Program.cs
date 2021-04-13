using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.DocumentEngine.Types.Statiq;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

namespace StatiqGenerator
{
    class Program
    {
        private static Dictionary<string, IPipeline> pipelines = new Dictionary<string, IPipeline>() {
            { "Authors",
                new XperienceContentPipeline<Author>(
                AuthorProvider.GetAuthors(),
                "content/author.cshtml",
                Config.FromDocument((doc, ctx) =>
                {
                    var author = XperienceDocumentConverter.ToPageType<Author>(doc);
                    return new NormalizedPath($"authors/{author.FirstName}_{author.LastName}.html");
                }))
            },
            { "Books",
                new XperienceContentPipeline<Book>(
                BookProvider.GetBooks(),
                "content/book.cshtml",
                Config.FromDocument((doc, ctx) =>
                {
                    var book = XperienceDocumentConverter.ToPageType<Book>(doc);
                    return new NormalizedPath($"books/{book.Title}.html");
                }))
            }
        };

        public static async Task<int> Main(string[] args)
        {
            CMS.DataEngine.CMSApplication.Init();
            return await Bootstrapper
                .Factory
                .CreateDefault(args)
                .RegisterXperiencePipelines(pipelines)
                .AddHostingCommands()
                .RunAsync();
        }
            
    }
}
