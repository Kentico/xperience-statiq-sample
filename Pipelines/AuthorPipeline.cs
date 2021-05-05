using System.Linq;
using System.Threading.Tasks;
using CMS.CustomTables.Types.Statiq;
using CMS.DocumentEngine.Types.Statiq;
using Statiq.Common;

namespace StatiqGenerator
{
    class AuthorPipeline : XperienceContentPipeline<Author>
    {
        public AuthorPipeline()
        {
            Query = AuthorProvider.GetAuthors();
        }
    }
}
