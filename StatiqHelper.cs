using CMS.DocumentEngine.Routing;
using CMS.DocumentEngine.Types.Statiq;

namespace StatiqGenerator
{
    public class StatiqHelper
    {
        public const string BookPath = "/books";
        public const string AttachmentPath = "/assets/attachments";

        public static string GetBookUrl(Book book)
        {
            var path = book.GetPageUrlPath("en-US").Slug;
            return $"{BookPath}/{path}.html".ToLower();
        }
    }
}
