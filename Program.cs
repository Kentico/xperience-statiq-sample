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
            // Connect to external database
            var connString = Environment.GetEnvironmentVariable("CMSConnectionString");
            IDataConnection conn = DataConnectionFactory.GetNativeConnection(connString);
            using (var scope = new CMSConnectionScope(conn).Open())
            {
                CMSApplication.Init();
                var result = await Bootstrapper
                    .Factory
                    .CreateDefault(args)
                    .AddPipeline<RatingPipeline>()
                    .AddPipeline<BookPipeline>()
                    .AddPipeline<AuthorPipeline>()
                    .AddPipeline<ContactPipeline>()
                    .AddPipeline("Assets", outputModules: new IModule[] { new CopyFiles("assets/**") })
                    .AddHostingCommands()
                    .RunAsync();
                scope.Close();
                return result;
            }

        }
    }
}
