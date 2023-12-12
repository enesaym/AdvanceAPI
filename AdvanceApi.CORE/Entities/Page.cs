using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.CORE.Entities
{
 
    public partial class Page
    {
   
        public Page()
        {
            TitleAuthorizations = new HashSet<TitleAuthorization>();
        }

        public int PageID { get; set; }

        public string PageName { get; set; }

        public string Path { get; set; }

        public virtual ICollection<TitleAuthorization> TitleAuthorizations { get; set; }
    }
}
