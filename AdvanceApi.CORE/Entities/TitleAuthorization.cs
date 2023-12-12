using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.CORE.Entities
{
    public partial class TitleAuthorization
    {
        public int TitleID { get; set; }
       
        public int AuthorizationID { get; set; }

        public int PageID { get; set; }

        public virtual Authorization Authorization { get; set; }

        public virtual Page Page { get; set; }

        public virtual Title Title { get; set; }
    }
}
