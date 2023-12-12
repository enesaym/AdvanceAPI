using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.CORE.Entities
{  
    public partial class Payment
    {
        public Payment()
        {
            Advances = new HashSet<Advance>();
        }
        public int ID { get; set; }

        public DateTime? DeterminedPaymentDate { get; set; }

        public int? FinanceManagerID { get; set; }

        public virtual ICollection<Advance> Advances { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
