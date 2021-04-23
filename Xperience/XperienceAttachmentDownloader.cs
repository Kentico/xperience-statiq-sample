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

        protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            var node = XperienceDocumentConverter.ToTreeNode<TreeNode>(input);
            foreach (var attachment in node.AllAttachments)
            {
                await Task.Run(() => DownloadAttachment(attachment));
            }

            return input.Yield();
        }

        private void DownloadAttachment(DocumentAttachment attachment)
        {
            var fileName = $"input{StatiqHelper.AttachmentPath}/{attachment.AttachmentName}";
            if (!File.Exists(fileName))
            {
                var binary = AttachmentBinaryHelper.GetAttachmentBinary(attachment);
                BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.Create));
                writer.Write(binary);
                writer.Flush();
                writer.Close();
            }
        }
    }
}