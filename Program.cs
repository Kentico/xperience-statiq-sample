using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.DocumentEngine.Types.Statiq;
using Statiq.App;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using Statiq.Web;

namespace StatiqGenerator
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            CMS.DataEngine.CMSApplication.Init();
            return await Bootstrapper
                .Factory
                .CreateDefault(args)
                .AddPipeline<BookPipeline>()
                .AddPipeline<AuthorPipeline>()
                //.RegisterXperiencePipelines(Constants.Pipelines)
                /*.AddSerialPipeline("Home",
                    inputModules: new IModule[] { new ReadFiles(patterns: "index.cshtml") },
                    processModules: new IModule[] {
                        new RenderRazor().WithModel(Config.FromDocument((doc, context) =>
                            new HomeViewModel()
                            {
                                Books = context.Outputs.FromPipeline(Constants.BookPipeline).ParallelSelectAsync(doc =>
                                    Task.Run(() => XperienceDocumentConverter.ToTreeNode<Book>(doc))).Result,
                                Authors = context.Outputs.FromPipeline(Constants.AuthorPipeline).ParallelSelectAsync(doc =>
                                    Task.Run(() => XperienceDocumentConverter.ToTreeNode<Author>(doc))).Result
                            }
                        )),
                        new SetDestination(Config.FromDocument((doc, ctx) => {
                            return new NormalizedPath("index.html");
                        }))
                    },
                    outputModules: new IModule[] { new WriteFiles() }
                )*/
                .AddPipeline("Assets", outputModules: new IModule[] { new CopyFiles("assets/**") })
                .AddHostingCommands()
                .RunAsync();
        }
    }
}
