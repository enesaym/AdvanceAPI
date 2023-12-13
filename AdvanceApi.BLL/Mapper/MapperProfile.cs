using AdvanceApi.CORE.Entities;
using AdvanceApi.DTO.BusinessUnit;
using AdvanceApi.DTO.Employee;
using AdvanceApi.DTO.Title;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.BLL.Mapper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<BusinessUnit,BusinessUnitSelectDTO>().ReverseMap();
			CreateMap<EmployeeRegisterDTO, Employee>().ReverseMap();
			CreateMap<EmployeeLoginDTO, Employee>().ReverseMap();
			CreateMap<TitleSelectDTO,Title>().ReverseMap();

		}
	}
}