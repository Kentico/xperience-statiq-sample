using CMS.CustomTables.Types.Statiq;
using CMS.DocumentEngine.Types.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System.Linq;

namespace Kentico.Xperience.StatiqGenerator
{
    public class HomePipeline : Pipeline
    {
        public HomePipeline()
        {
            Dependencies.AddRange(nameof(BookPipeline), nameof(RatingPipeline), nameof(AuthorPipeline), nameof(ContactPipeline));
            InputModules = new ModuleList
            {
                new ReadFiles("index.cshtml")
            };

            ProcessModules = new ModuleList {
                new RenderRazor().WithModel(Config.FromDocument((doc, context) =>
                {
                    var allBooks = XperienceDocumentConverter.ToTreeNodes<Book>(context.Outputs.FromPipeline(nameof(BookPipeline)));
                    var allAuthors = XperienceDocumentConverter.ToTreeNodes<Author>(context.Outputs.FromPipeline(nameof(AuthorPipeline)));
                    var allRatings = XperienceDocumentConverter.ToCustomTableItems<RatingsItem>(context.Outputs.FromPipeline(nameof(RatingPipeline)), RatingsItem.CLASS_NAME);
                    
                    var contactInfo = XperienceDocumentConverter.ToTreeNodes<ContactUs>(context.Outputs.FromPipeline(nameof(ContactPipeline))).FirstOrDefault();
                    var booksWithReviews = allBooks.Select(b => new BookWithReviews(b, allRatings));
                    var authorsWithBooks = allAuthors.Select(a => new AuthorWithBooks(a, booksWithReviews));

                    return new HomeViewModel() {
                        Authors = authorsWithBooks,
                        Books = booksWithReviews,
                        ContactInfo = contactInfo
                    };
                })),
                new SetDestination(Config.FromDocument((doc, ctx) => {
                    return new NormalizedPath("index.html");
                }))
            };

            OutputModules = new ModuleList {
                new WriteFiles()
            };
        }
    }
}
