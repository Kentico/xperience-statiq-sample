using CMS.DocumentEngine.Types.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Threading.Tasks;

namespace StatiqGenerator
{
    public class HomePipeline : Pipeline
    {
        public HomePipeline()
        {
            Dependencies.AddRange(nameof(AuthorPipeline), nameof(BookPipeline));
            InputModules = new ModuleList
            {
                new ReadFiles(patterns: "index.cshtml")
            };

            ProcessModules = new ModuleList {
                new RenderRazor().WithModel(Config.FromDocument((doc, context) =>
                {
                    var allBooks = context.Outputs.FromPipeline(nameof(BookPipeline)).ParallelSelectAsync(doc =>
                        Task.Run(() => XperienceDocumentConverter.ToTreeNode<Book>(doc))).Result;
                    return new HomeViewModel() {
                        Authors = context.Outputs.FromPipeline(nameof(AuthorPipeline)).ParallelSelectAsync(doc =>
                        {
                            var author = XperienceDocumentConverter.ToTreeNode<Author>(doc);
                            return Task.Run(() => new AuthorWithBooks(author, allBooks));
                        })
                    };
                })),
                new SetDestination(Config.FromDocument((doc, ctx) => {
                    return new NormalizedPath("index.html");
                }))
            };

            OutputModules = new ModuleList {
                new WriteFiles()
            };
        }
    }
}
