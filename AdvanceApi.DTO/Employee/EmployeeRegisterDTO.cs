using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DTO.Employee
{
	public class EmployeeRegisterDTO
	{

		public string Name { get; set; }

		public string Surname { get; set; }

		public string PhoneNumber { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }	

		public int? BusinessUnitID { get; set; }

		public int? TitleID { get; set; }

		public int? UpperEmployeeID { get; set; }

		

	}
}
