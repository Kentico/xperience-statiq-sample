using CMS.DocumentEngine;

namespace StatiqGenerator
{
    public static class DocumentAttachmentExtensions
    {
        public static string GetStatiqPath(this DocumentAttachment attachment)
        {
            return $"{Constants.AttachmentPath}{attachment.AttachmentName}";
        }
    }
}
