using CMS.DocumentEngine;

namespace StatiqGenerator
{
    public static class DocumentAttachmentExtensions
    {
        /// <summary>
        /// Returns the full path of an attachment in the output folder
        /// </summary>
        public static string GetStatiqPath(this DocumentAttachment attachment)
        {
            return $"{StatiqHelper.AttachmentPath}/{attachment.AttachmentName}";
        }
    }
}
