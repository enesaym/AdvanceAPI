using AdvanceApi.DTO.Employee;
using AdvanceApi.DTO.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DTO.AdvanceHistory
{
    public class AdvanceHistorySelectDTO
    {
        public int ID { get; set; }

        public int? StatusID { get; set; }

        public int? AdvanceID { get; set; }

        public int? TransactorID { get; set; }

        public decimal? ApprovedAmount { get; set; }

        public DateTime? Date { get; set; }

        public virtual EmployeeSelectDTO Transactor { get; set; }
    }
}
