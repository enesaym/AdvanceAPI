using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.CORE.Entities
{
    
    public partial class Project
    {
       
        public Project()
        {
            Advances = new HashSet<Advance>();
            Employees = new HashSet<Employee>();
        }

        public int ID { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? StartingDate { get; set; }
  
        public virtual ICollection<Advance> Advances { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
