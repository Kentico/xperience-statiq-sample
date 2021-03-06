using CMS.CustomTables;
using CMS.CustomTables.Types.Statiq;

namespace Kentico.Xperience.StatiqGenerator
{
    class RatingPipeline : XperienceObjectPipeline<RatingsItem>
    {
        public RatingPipeline()
        {
            Query = CustomTableItemProvider.GetItems<RatingsItem>();
        }
    }
}
