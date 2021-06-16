using CMS.DocumentEngine;
using Statiq.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Module = Statiq.Common.Module;

namespace Kentico.Xperience.StatiqGenerator
{
    /// <summary>
    /// A module used in <see cref="IPipeline.InputModules"> which accepts a <see cref="DocumentQuery"/>
    /// and executes the query. The output of the module is a list of <see cref="IDocument"/>
    /// objects containing the properties of the nodes.
    /// </summary>
    /// <typeparam name="TPageType">A strongly-typed class which extends <see cref="TreeNode"/>, or just <see cref="TreeNode"/>
    /// for generic queries</typeparam>
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
            if (Query == null)
            {
                throw new NullReferenceException($"{nameof(XperienceContentModule<TPageType>)} missing query, pass a DocumentQuery in the constructor to retrieve pages from your Xperience project.");
            }

            var items = await Task.Run(() => Query.TypedResult);
            return items.Select(i => XperienceDocumentConverter.FromBaseInfo(context, i));
        }
    }
}