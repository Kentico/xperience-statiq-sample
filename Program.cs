using System.Threading.Tasks;
using Statiq.App;
using Statiq.Common;
using Statiq.Core;
using Statiq.Web;

[assembly: CMS.AssemblyDiscoverable]
namespace StatiqGenerator
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            CMS.DataEngine.CMSApplication.Init();
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
