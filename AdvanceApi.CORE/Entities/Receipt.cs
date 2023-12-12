using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.CORE.Entities
{
    public partial class Receipt
    {
        public int ID { get; set; }

        public string ReceiptNo { get; set; }

        public bool? isRefundReceipt { get; set; }

        public int? AdvanceID { get; set; }

        public DateTime? Date { get; set; }

        public int? AccountantID { get; set; }

        public virtual Advance Advance { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
