using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DTO.Rule
{
    public class RuleSelectDTO
    {
        public int ID { get; set; }

        public decimal? MaxAmount { get; set; }

        public decimal? MinAmount { get; set; }

        public DateTime? Date { get; set; }

        public int? TitleID { get; set; }
    }
}
