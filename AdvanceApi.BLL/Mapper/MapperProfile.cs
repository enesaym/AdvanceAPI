using AdvanceApi.CORE.Entities;
using AdvanceApi.DTO.Advance;
using AdvanceApi.DTO.AdvanceHistory;
using AdvanceApi.DTO.BusinessUnit;
using AdvanceApi.DTO.Employee;
using AdvanceApi.DTO.Payment;
using AdvanceApi.DTO.Project;
using AdvanceApi.DTO.Receipt;
using AdvanceApi.DTO.Status;
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
            CreateMap<AdvanceHistorySelectDTO, AdvanceHistory>().ReverseMap();
            CreateMap<AdvanceInsertDTO, Advance>().ReverseMap();
			CreateMap<EmployeeSelectDTO, Employee>().ReverseMap();
            CreateMap<EmployeeLoginDTO, Employee>().ReverseMap();
			CreateMap<TitleSelectDTO,Title>().ReverseMap();
			CreateMap<Project,ProjectSelectDTO>().ReverseMap();
            CreateMap<StatusSelectDTO, Status>().ReverseMap();
            CreateMap<PaymentSelectDTO, Payment>().ReverseMap();
            CreateMap<ReceiptSelectDTO, Receipt>().ReverseMap();
            CreateMap<AdvanceSelectDTO, Advance>().ReverseMap();

        }
	}
}