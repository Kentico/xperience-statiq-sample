using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using Statiq.Yaml;

namespace StatiqGenerator
{
    public class HomePipeline : Pipeline
    {
        public HomePipeline()
        {
            InputModules = new ModuleList
            {
                new ReadFiles("index.cshtml")
            };

            ProcessModules = new ModuleList {
                new ExtractFrontMatter(
                    new ParseYaml()
                ),

                new RenderRazor().WithModel(new { }),

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
