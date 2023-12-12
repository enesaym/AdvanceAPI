using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.CORE.Entities
{
    public partial class Title
    {
        public Title()
        {
            Employees = new HashSet<Employee>();
            Rules = new HashSet<Rule>();
            TitleAuthorizations = new HashSet<TitleAuthorization>();
        }

        public int ID { get; set; }

        public string TitleName { get; set; }

        public string TitleDescription { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Rule> Rules { get; set; }

        public virtual ICollection<TitleAuthorization> TitleAuthorizations { get; set; }
    }
}
