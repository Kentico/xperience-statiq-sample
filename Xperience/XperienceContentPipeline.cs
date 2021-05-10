using CMS.DocumentEngine;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Collections.Generic;

namespace StatiqGenerator
{
    /// <summary>
    /// A Pipeline which retrieves pages from the content tree. Optionally, if other properties are
    /// provided, it outputs multiple documents at the <see cref="DestinationPath"/> after processing
    /// the Razor view file at <see cref="ReadPath"/> with the model provided in <see cref="WithModel">
    /// </summary>
    /// <typeparam name="TPageType">A strongly-typed class which extends <see cref="TreeNode"/>, or just <see cref="TreeNode"/>
    /// for generic pages</typeparam>
    class XperienceContentPipeline<TPageType> : IPipeline where TPageType : TreeNode, new()
    {
        /// <summary>
        /// A path relative to the /input folder which contains the Razor view for processing
        /// </summary>
        public string ReadPath { get; set; }

        public DocumentQuery<TPageType> Query { get; set; }

        /// <summary>
        /// A full path to the desired output of the pages in the /output folder. Use
        /// <see cref="Config.FromDocument(IDocument, IExecutionContext)"> to provide dynamic paths.
        /// </summary>
        public Config<NormalizedPath> DestinationPath { get; set; }

        /// <summary>
        /// Optional property to customize the model passed to the input Razor file. Use
        /// <see cref="Config.FromDocument(IDocument, IExecutionContext)"> to access the document.
        /// If not provided, the strongly-typed document is passed.
        /// </summary>
        public Config<object> WithModel { get; set; } = Config.FromDocument((doc, context) => XperienceDocumentConverter.ToTreeNode<TPageType>(doc));

        public XperienceContentPipeline()
        {

        }

        public ModuleList InputModules
        {
            get
            {
                var list = new ModuleList {
                    new XperienceContentModule<TPageType>(Query)
                };
                if (DestinationPath != null)
                {
                    list.Add(new SetDestination(DestinationPath));
                }

                return list;
            }
        }

        public ModuleList ProcessModules
        {
            get
            {
                return new ModuleList {
                    new XperienceAttachmentDownloader()
                };
            }
        }

        public ModuleList PostProcessModules
        {
            get
            {
                var list = new ModuleList();
                if (ReadPath != null) list.Add(new MergeContent(
                         new ReadFiles(patterns: ReadPath)
                     ));
                if (WithModel != null) list.Add(new RenderRazor().WithModel(WithModel));

                return list;
            }
        }

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
