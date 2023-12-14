using AdvanceApi.BLL.Mapper;
using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.UnitOfWork;
using AdvanceApi.DTO.Employee;
using AdvanceApi.DTO.Title;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.BLL.Manager
{
	public class EmployeeManager
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly MyMapper _mapper;
        public EmployeeManager(IUnitOfWork unitOfWork, MyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<EmployeeSelectDTO>>> GetEmployeeBase()
        {
            try
            {
                var employees = await _unitOfWork.EmployeeDAL.GetEmployeeBase();
                var mapped = _mapper.Map<List<Employee>, List<EmployeeSelectDTO>>(employees);
                return new ApiResponse<List<EmployeeSelectDTO>>(mapped);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<EmployeeSelectDTO>>(ex.Message);
            }
        }

    }
}
