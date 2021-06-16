using CMS.Base;
using CMS.DocumentEngine;
using Statiq.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Kentico.Xperience.StatiqGenerator
{
    /// <summary>
    /// When included in <see cref="IPipeline.PostProcessModules">, downloads all page attachments
    /// to the the input folder at the path indicated by <see cref="StatiqHelper.AttachmentPath">
    /// </summary>
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
                DownloadAttachment(attachment, context);
            }

            return input.Yield();
        }
#pragma warning restore 1998

        private void DownloadAttachment(DocumentAttachment attachment, IExecutionContext context)
        {
            var fileName = $"{StatiqHelper.AttachmentPath}/{attachment.AttachmentName}";
            var destination = context.FileSystem.GetOutputFile(fileName);
            if (!destination.Exists)
            {
                var thread = new CMSThread(() =>
                {
                    try
                    {
                        var binary = AttachmentBinaryHelper.GetAttachmentBinary(attachment);
                        using (Stream fileStream = destination.OpenWrite(true))
                        {
                            long initialPosition = fileStream.Position;
                            fileStream.Write(binary, 0, binary.Length);
                            long length = fileStream.Position - initialPosition;
                            fileStream.SetLength(length);
                        }
                    }
                    catch(Exception e) {
                        Console.WriteLine(e.Message);
                    }
                });
                thread.Start();
            }
        }
    }
}