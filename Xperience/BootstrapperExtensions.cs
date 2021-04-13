using Statiq.App;
using Statiq.Common;
using System.Collections.Generic;

namespace StatiqGenerator
{
    public static class BootstrapperExtensions
    {
        public static Bootstrapper RegisterXperiencePipelines(this Bootstrapper bootstrapper, Dictionary<string, IPipeline> pipelines)
        {
            foreach (var pipeline in pipelines)
            {
                var p = pipeline.Value;
                bootstrapper.AddSerialPipeline(
                    name: pipeline.Key,
                    inputModules: p.InputModules,
                    processModules: p.ProcessModules,
                    outputModules: p.OutputModules
                );
            }

            return bootstrapper;
        }
    }
}
