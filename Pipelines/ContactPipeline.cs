using CMS.DocumentEngine.Types.Statiq;

namespace StatiqGenerator
{
    class ContactPipeline : XperienceContentPipeline<ContactUs>
    {
        public ContactPipeline()
        {
            Query = ContactUsProvider.GetContactUses().TopN(1);
        }
    }
}
