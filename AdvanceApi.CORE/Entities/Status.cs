using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.CORE.Entities
{
    public partial class Status
    {
        public Status()
        {
            AdvanceHistories = new HashSet<AdvanceHistory>();
        }

        public int ID { get; set; }

        public string StatusName { get; set; }

        public virtual ICollection<AdvanceHistory> AdvanceHistories { get; set; }
    }
}
