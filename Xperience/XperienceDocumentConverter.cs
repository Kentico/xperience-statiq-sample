using CMS.DataEngine;
using CMS.DocumentEngine;
using Statiq.Common;
using System.Collections.Generic;
using System.Linq;

namespace StatiqGenerator
{
    public class XperienceDocumentConverter
    {
        public static BaseInfo ToBaseInfo(IDocument doc, string objectType)
        {
            var baseInfo = ModuleManager.GetObject(objectType);
            foreach(var prop in baseInfo.Properties)
            {
                baseInfo.SetValue(prop, doc.Get(prop));
            }

            return baseInfo;
        }

        public static IDocument FromBaseInfo(IExecutionContext context, BaseInfo info)
        {
            var metadata = new List<KeyValuePair<string, object>>();
            metadata.AddRange(info.Properties.Select(
                 key => new KeyValuePair<string, object>(key, info.GetValue(key))
             ));
            
            return context.CreateDocument(metadata);
        }

        public static TPageType ToTreeNode<TPageType>(IDocument doc) where TPageType : TreeNode, new()
        {
            var node = TreeNode.New<TPageType>();
            foreach (var prop in node.Properties)
            {
                node.SetValue(prop, doc.Get(prop));
            }

            return node;
        }
    }
}
