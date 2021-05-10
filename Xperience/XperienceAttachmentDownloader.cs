using CMS.Base;
using CMS.DocumentEngine;
using Statiq.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace StatiqGenerator
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
                DownloadAttachment(attachment);
            }

            return input.Yield();
        }
#pragma warning restore 1998

        private void DownloadAttachment(DocumentAttachment attachment)
        {
            var fileName = $"output{StatiqHelper.AttachmentPath}/{attachment.AttachmentName}";
            if (!File.Exists(fileName))
            {
                var thread = new CMSThread(() =>
                {
                    // Try set permissions
                    var permissionSet = new PermissionSet(PermissionState.None);
                    var writePermission = new FileIOPermission(FileIOPermissionAccess.Write, fileName);
                    try
                    {
                        writePermission.Demand();
                        var binary = AttachmentBinaryHelper.GetAttachmentBinary(attachment);
                        BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite));
                        writer.Write(binary);
                        writer.Flush();
                        writer.Close();
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