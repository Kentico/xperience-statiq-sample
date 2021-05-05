using CMS.Base;
using CMS.DocumentEngine;
using Statiq.Common;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StatiqGenerator
{
    public class XperienceAttachmentDownloader : Module
    {
        public XperienceAttachmentDownloader()
        {

        }

        #pragma warning disable 1998
        protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            var node = XperienceDocumentConverter.ToTreeNode<TreeNode>(input);
            foreach (var attachment in node.AllAttachments)
            {
                DownloadAttachment(attachment);
            }

            return input.Yield();
        }
        #pragma warning restore 1998

        private void DownloadAttachment(DocumentAttachment attachment)
        {
            var fileName = $"input{StatiqHelper.AttachmentPath}/{attachment.AttachmentName}";
            if (!File.Exists(fileName))
            {
                var thread = new CMSThread(() => {
                    var binary = AttachmentBinaryHelper.GetAttachmentBinary(attachment);
                    BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.Create));
                    writer.Write(binary);
                    writer.Flush();
                    writer.Close();
                });
                thread.Start();
            }
        }
    }
}