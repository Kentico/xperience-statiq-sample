using CMS.DocumentEngine.Routing;
using CMS.DocumentEngine.Types.Statiq;

namespace StatiqGenerator
{
    public class StatiqHelper
    {
        public const string BookPath = "books";
        public const string AuthorPath = "authors";
        public const string AttachmentPath = "books/attachments";
        public const string AdministrationURL = "https://ericd-statiq.azurewebsites.net";

        public static string GetBookUrl(Book book)
        {
            var path = book.GetPageUrlPath("en-US").Slug;
            return $"{BookPath}/{path}.html".ToLower();
        }
    }
}
