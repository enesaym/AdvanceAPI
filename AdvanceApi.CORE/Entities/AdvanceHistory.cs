using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AdvanceApi.CORE.Entities
{
   
    public partial class AdvanceHistory
    {
        public int ID { get; set; }

        public int? StatusID { get; set; }

        public int? AdvanceID { get; set; }

        public int? TransactorID { get; set; }

        public decimal? ApprovedAmount { get; set; }

        public DateTime? Date { get; set; }

        public virtual Advance Advance { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Status Status { get; set; }
    }
}
