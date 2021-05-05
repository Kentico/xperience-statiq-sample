using CMS.DocumentEngine.Types.Statiq;

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
