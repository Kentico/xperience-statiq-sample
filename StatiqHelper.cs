using CMS.DocumentEngine.Routing;
using CMS.DocumentEngine.Types.Statiq;

namespace StatiqGenerator
{
    public class StatiqHelper
    {
        public const string BookPath = "books";
        public const string AuthorPath = "authors";
        public const string AttachmentPath = "/assets/attachments";
        public const string RatingEndpoint = "http://statiqcms/rest/customtableitem.statiq.ratings?hashexpirationutc=2022-06-05T17:33:18.0000000Z&hash=46a9e5c47867580da4dea70621ac9cbe2412275a28386e4322013e4c34139566";

        public static string GetBookUrl(Book book)
        {
            var path = book.GetPageUrlPath("en-US").Slug;
            return $"{BookPath}/{path}.html".ToLower();
        }
    }
}
