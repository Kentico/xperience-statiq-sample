using CMS.DocumentEngine.Types.Statiq;
using Statiq.Common;

namespace StatiqGenerator
{
    class AuthorPipeline : XperienceContentPipeline<Author>
    {
        public AuthorPipeline()
        {
            Dependencies.Add(nameof(BookPipeline));
            Query = AuthorProvider.GetAuthors();
            ReadPath = "content/author.cshtml";
            DestinationPath = Config.FromDocument((doc, ctx) =>
            {
                var author = XperienceDocumentConverter.ToTreeNode<Author>(doc);
                return new NormalizedPath($"authors/{author.FirstName}_{author.LastName}.html".ToLower());
            });
        }
    }
}
