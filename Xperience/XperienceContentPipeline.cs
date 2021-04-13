using CMS.DocumentEngine;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;

namespace StatiqGenerator
{
    class XperienceContentPipeline<TPageType> : Pipeline where TPageType : TreeNode, new()
    {
        public XperienceContentPipeline(DocumentQuery<TPageType> query, string readPath, Config<NormalizedPath> destinationPath)
        {
            InputModules = new ModuleList {
                new XperienceContentModule<TPageType>(query),
                new SetDestination(destinationPath)
            };

            ProcessModules = new ModuleList {
                new MergeContent(
                    new ReadFiles(patterns: readPath)
                ),

                new RenderRazor().WithModel(Config.FromDocument((doc, context) =>
                    XperienceDocumentConverter.ToPageType<TPageType>(doc)
                )),
            };

            OutputModules = new ModuleList {
                new WriteFiles()
            };
        }
    }
}
