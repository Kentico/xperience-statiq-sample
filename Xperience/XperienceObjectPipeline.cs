
using System.Collections.Generic;
using CMS.DataEngine;
using Statiq.Common;

namespace Kentico.Xperience.StatiqGenerator
{
    ///<summary>
    /// A pipeline which retrieves objects from the database and performs no additional processing.
    ///</summary>
    class XperienceObjectPipeline<TObjectType> : IPipeline where TObjectType : BaseInfo, new()
    {
        public ObjectQuery<TObjectType> Query { get; set; }

        public XperienceObjectPipeline()
        {

        }

        public ModuleList InputModules
        {
            get => new ModuleList {
                new XperienceObjectModule<TObjectType>(Query)
            };
        }

        public ModuleList ProcessModules { get; set; }

        public ModuleList PostProcessModules { get; set; }

        public ModuleList OutputModules { get; set; }

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