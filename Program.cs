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
            CMSApplication.PreInit(true);

            // Connect to external database
            var connString = Environment.GetEnvironmentVariable("CMSConnectionString");
            ConnectionHelper.ConnectionString = connString;

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
                .DeployToNetlify(
                    Environment.GetEnvironmentVariable("NETLIFY_SITE"),
                    Environment.GetEnvironmentVariable("NETLIFY_KEY")
                )
                .RunAsync();
        }
    }
}
