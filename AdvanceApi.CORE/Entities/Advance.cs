using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.CORE.Entities
{
    public partial class Advance
    {     
        public Advance()
        {
            AdvanceHistories = new HashSet<AdvanceHistory>();
            Receipts = new HashSet<Receipt>();
        }

        public int ID { get; set; }
   
        public decimal? AdvanceAmount { get; set; }

        public string AdvanceDescription { get; set; }

        public int? ProjectID { get; set; }

        public DateTime? DesiredDate { get; set; }

        public int? StatusID { get; set; }

        public int? PaymentID { get; set; }

        public DateTime? RequestDate { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<AdvanceHistory> AdvanceHistories { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
