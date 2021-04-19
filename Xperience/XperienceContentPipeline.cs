using CMS.DocumentEngine;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Collections.Generic;

namespace StatiqGenerator
{
    /// <summary>
    /// A Pipeline which outputs multiple documents at the <see cref="DestinationPath"/> after processing
    /// the Razor view file at <see cref="ReadPath"/>
    /// </summary>
    /// <remarks>
    /// <para>
    /// For example, if you use <see cref="Query"/> to retrieve 2 Article pages, in the <see cref="DestinationPath"/>
    /// you can use <see cref="XperienceDocumentConverter.ToTreeNode{TPageType}(IDocument)"/> to convert each <see cref="IDocument"/> into the Xperience
    /// Article type, then set the <see cref="NormalizedPath"/> to "articles/{article.NodeAlias}.html."
    /// </para>
    /// <para>
    /// The model Article will be passed into the Razor view, and the result will be 2 HTML files:<list type="bullet">
    ///     <item>Article-1.html</item>
    ///     <item>Article-2.html</item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <typeparam name="TPageType">A strongly-typed class which extends <see cref="TreeNode"/>, or just <see cref="TreeNode"/>
    /// for generic pages</typeparam>
    class XperienceContentPipeline<TPageType> : IXperiencePipeline where TPageType : TreeNode, new()
    {
        public string ReadPath { get; set; }
        public DocumentQuery<TPageType> Query { get; set; }
        public Config<NormalizedPath> DestinationPath { get; set; }

        public XperienceContentPipeline()
        {

        }

        public ModuleList InputModules {
            get => new ModuleList {
                new XperienceContentModule<TPageType>(Query),
                new SetDestination(DestinationPath)
            };
        }

        public ModuleList ProcessModules {
            get => new ModuleList {
                new XperienceAttachmentDownloader(),
                new MergeContent(
                    new ReadFiles(patterns: ReadPath)
                ),
                new RenderRazor().WithModel(Config.FromDocument((doc, context) =>
                    XperienceDocumentConverter.ToTreeNode<TPageType>(doc)
                ))
            };
        }

        public ModuleList PostProcessModules { get; set; }

        public ModuleList OutputModules {
            get => new ModuleList {
                new WriteFiles()
            };
        }

        public HashSet<string> Dependencies { get; set; } = new HashSet<string>();

        public HashSet<string> DependencyOf { get; set; } = new HashSet<string>();

        public bool Isolated { get; set; }
        public bool Deployment { get; set; }
        public ExecutionPolicy ExecutionPolicy { get; set; }

        IReadOnlyCollection<string> IReadOnlyPipeline.Dependencies { get; }

        IReadOnlyCollection<string> IReadOnlyPipeline.DependencyOf { get; }

        bool IReadOnlyPipeline.Isolated { get; }

        bool IReadOnlyPipeline.Deployment { get; }

        ExecutionPolicy IReadOnlyPipeline.ExecutionPolicy { get; }
    }
}
