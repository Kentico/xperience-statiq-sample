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
    /// <typeparam name="TPageType">A strongly-typed class which extends <see cref="TreeNode"/>, or just <see cref="TreeNode"/>
    /// for generic pages</typeparam>
    class XperienceContentPipeline<TPageType> : IXperiencePipeline where TPageType : TreeNode, new()
    {
        public string ReadPath { get; set; }
        public DocumentQuery<TPageType> Query { get; set; }
        public Config<NormalizedPath> DestinationPath { get; set; }

        public Config<object> WithModel { get; set; } = Config.FromDocument((doc, context) => XperienceDocumentConverter.ToTreeNode<TPageType>(doc));

        public XperienceContentPipeline()
        {

        }

        public ModuleList InputModules
        {
            get => new ModuleList {
                new XperienceContentModule<TPageType>(Query),
                new SetDestination(DestinationPath)
            };
        }

        public ModuleList PostProcessModules
        {
            get => new ModuleList {
                new XperienceAttachmentDownloader(),
                new MergeContent(
                    new ReadFiles(patterns: ReadPath)
                ),
                new RenderRazor().WithModel(WithModel)
            };
        }

        public ModuleList ProcessModules { get; set; }

        public ModuleList OutputModules
        {
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
