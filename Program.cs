using CMS.DataEngine;
using System;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Common;
using Statiq.Core;
using Statiq.Web;
using CMS.Core;

[assembly: CMS.AssemblyDiscoverable]
namespace StatiqGenerator
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Service.InitializeContainer();
            ConnectionHelper.ConnectionString = Environment.GetEnvironmentVariable("CMSConnectionString");
            CMSApplication.Init();
            return await Bootstrapper
                .Factory
                .CreateDefault(args)
                .AddPipeline<RatingPipeline>()
                .AddPipeline<BookPipeline>()
                .AddPipeline<AuthorPipeline>()
                .AddPipeline<ContactPipeline>()
                .AddPipeline("Assets", outputModules: new IModule[] { new CopyFiles("assets/**") })
                .AddHostingCommands()
                .RunAsync();
        }
    }
}
