using CMS.DocumentEngine;
using Statiq.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Module = Statiq.Common.Module;

namespace StatiqGenerator
{
    public class XperienceContentModule<TPageType>
        : Module where TPageType : TreeNode, new()
    {

        private DocumentQuery<TPageType> Query { get; set; }

        public XperienceContentModule(DocumentQuery<TPageType> query)
        {
            Query = query;
        }

        protected override async Task<IEnumerable<IDocument>> ExecuteContextAsync(IExecutionContext context)
        {
            if(Query == null)
            {
                throw new NullReferenceException($"{nameof(XperienceContentModule<TPageType>)} missing query, pass a DocumentQuery in the constructor to retrieve pages from your Xperience project.");
            }

            var items = await Task.Run(() => Query.TypedResult);
            return items.Select(i => XperienceDocumentConverter.FromTreeNode(context, i));
        }
    }
}