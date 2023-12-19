using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DTO.Payment
{
    public class PaymentInsertDTO
    {
        public DateTime? DeterminedPaymentDate { get; set; }

        public int? FinanceManagerID { get; set; }

        public int? AdvanceId { get; set; }
    }
}
