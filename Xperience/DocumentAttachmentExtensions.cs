using CMS.DocumentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
