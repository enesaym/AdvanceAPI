using AdvanceApi.DTO.BusinessUnit;
using AdvanceApi.DTO.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DTO.Employee
{
	public class EmployeeSelectDTO
	{
        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int? UpperEmployeeID { get; set; }

        public int? BusinessUnitID { get; set; }

        public TitleSelectDTO Title { get; set; }

        public BusinessUnitSelectDTO BusinessUnit { get; set; }

        public int TitleID { get; set; }

        public string TitleName { get; set; }   
    }
}
