using CMS.DataEngine;
using Statiq.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Module = Statiq.Common.Module;

namespace StatiqGenerator
{
    /// <summary>
    /// A module used in <see cref="IPipeline.InputModules"> which accepts an <see cref="ObjectQuery"/>
    /// and executes the query. The output of the module is a list of <see cref="IDocument"/>
    /// objects containing the properties of the object.
    /// </summary>
    /// <typeparam name="TObjectType">An object type that extends <see cref="BaseInfo"></typeparam>
    public class XperienceObjectModule<TObjectType>
        : Module where TObjectType : BaseInfo, new()
    {

        private ObjectQuery<TObjectType> Query { get; set; }

        public XperienceObjectModule(ObjectQuery<TObjectType> query)
        {
            Query = query;
        }

        protected override async Task<IEnumerable<IDocument>> ExecuteContextAsync(IExecutionContext context)
        {
            if (Query == null)
            {
                throw new NullReferenceException($"{nameof(XperienceObjectModule<TObjectType>)} missing query, pass an ObjectQuery in the constructor to retrieve data from your Xperience project.");
            }

            var items = await Task.Run(() => Query.TypedResult);
            return items.Select(i => XperienceDocumentConverter.FromBaseInfo(context, i));
        }
    }
}