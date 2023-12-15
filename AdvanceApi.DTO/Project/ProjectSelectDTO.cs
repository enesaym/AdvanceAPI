using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DTO.Project
{
    public class ProjectSelectDTO
    {
        public int ID { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? StartingDate { get; set; }

    }
}
