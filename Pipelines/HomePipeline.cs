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
                    new HomeViewModel() {
                        Books = context.Outputs.FromPipeline(nameof(BookPipeline)).ParallelSelectAsync(doc =>
                            Task.Run(() => XperienceDocumentConverter.ToTreeNode<Book>(doc))).Result,
                        Authors = context.Outputs.FromPipeline(nameof(AuthorPipeline)).ParallelSelectAsync(doc =>
                            Task.Run(() => XperienceDocumentConverter.ToTreeNode<Author>(doc))).Result
                    }
                )),
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
