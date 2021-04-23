using CMS.CustomTables.Types.Statiq;
using CMS.DocumentEngine.Types.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatiqGenerator
{
    public class HomePipeline : Pipeline
    {
        public HomePipeline()
        {
            Dependencies.AddRange(nameof(BookPipeline), nameof(RatingPipeline));
            InputModules = new ModuleList
            {
                new ReadFiles("index.cshtml")
            };

            ProcessModules = new ModuleList {
                new RenderRazor().WithModel(Config.FromDocument((doc, context) =>
                {
                    var allBooks = context.Outputs.FromPipeline(nameof(BookPipeline)).ParallelSelectAsync(doc =>
                        Task.Run(() => XperienceDocumentConverter.ToTreeNode<Book>(doc))).Result;
                    var allRatings = context.Outputs.FromPipeline(nameof(RatingPipeline)).ParallelSelectAsync(doc =>
                        Task.Run(() => XperienceDocumentConverter.ToCustomTableItem<RatingsItem>(doc, RatingsItem.CLASS_NAME)));
                    var booksWithReviews = allBooks.Select(b => new BookWithReviews(b, allRatings.Result));

                    return new HomeViewModel() {
                        TopThreeBooks = booksWithReviews.OrderByDescending(b => b.AverageRating).Take(3)
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
