using CMS.DocumentEngine;

namespace StatiqGenerator
{
    public static class DocumentAttachmentExtensions
    {
        public static string GetStatiqPath(this DocumentAttachment attachment)
        {
            return $"{StatiqHelper.AttachmentPath}/{attachment.AttachmentName}";
        }
    }
}
