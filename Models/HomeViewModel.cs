using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatiqGenerator
{
    public class HomeViewModel
    {
        public Task<IEnumerable<AuthorWithBooks>> Authors { get; set; }
    }
}
