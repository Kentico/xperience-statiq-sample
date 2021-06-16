using CMS.DocumentEngine.Types.Statiq;

namespace Kentico.Xperience.StatiqGenerator
{
    class AuthorPipeline : XperienceContentPipeline<Author>
    {
        public AuthorPipeline()
        {
            Query = AuthorProvider.GetAuthors();
        }
    }
}
