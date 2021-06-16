using CMS.DocumentEngine.Types.Statiq;

namespace Kentico.Xperience.StatiqGenerator
{
    class ContactPipeline : XperienceContentPipeline<ContactUs>
    {
        public ContactPipeline()
        {
            Query = ContactUsProvider.GetContactUses().TopN(1);
        }
    }
}
