using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.CORE.Entities
{

    public partial class Authorization
    {
       
        public Authorization()
        {
            TitleAuthorizations = new HashSet<TitleAuthorization>();
        }

        public int AuthorizationID { get; set; }

        public string AuthroizationName { get; set; }

        
        public virtual ICollection<TitleAuthorization> TitleAuthorizations { get; set; }
    }
}
