using AdvanceApi.CORE.Entities;
using AdvanceApi.DTO.Advance;
using AdvanceApi.DTO.AdvanceHistory;
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
			CreateMap<AdvanceHistoryInsertDTO, AdvanceHistory>().ReverseMap();
			CreateMap<AdvanceInsertDTO, Advance>().ReverseMap();
			CreateMap<EmployeeSelectDTO, Employee>().ReverseMap();
            CreateMap<EmployeeLoginDTO, Employee>().ReverseMap();
			CreateMap<TitleSelectDTO,Title>().ReverseMap();

		}
	}
}