using AdvanceApi.BLL.Mapper;
using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.UnitOfWork;
using AdvanceApi.DTO.Employee;
using AdvanceApi.LOG.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.BLL.Manager
{
	public class AuthManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly MyMapper _mapper;
		private readonly ILog _logger;
		public AuthManager(IUnitOfWork unitOfWork,MyMapper mapper, ILog logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
			_logger=logger;
        }
		public async Task<ApiResponse<EmployeeSelectDTO>> Login(EmployeeLoginDTO dto)
		{
			try
			{
				var employee = _mapper.Map<EmployeeLoginDTO, Employee>(dto);
				var mapped = await _unitOfWork.AuthDAL.Login(employee,dto.Password);
				if (mapped == null)
				{
					return null;
				}
				var mappedEmployeeSelect = _mapper.Map<Employee, EmployeeSelectDTO>(mapped);

				//title name doldurdum
				mappedEmployeeSelect.TitleName=mapped.Title.TitleName;
				return new ApiResponse<EmployeeSelectDTO>(mappedEmployeeSelect);
			}
			catch (Exception ex)
			{
				return new ApiResponse<EmployeeSelectDTO>(ex.Message);
			}
		}

		public async Task<ApiResponse<EmployeeRegisterDTO>> Register(EmployeeRegisterDTO dto)
        {
            try
            {
                var employee= _mapper.Map<EmployeeRegisterDTO,Employee>(dto);
                var mapped = await _unitOfWork.AuthDAL.Register(employee,dto.Password);
				_logger.TakeInfoLog(dto.Email + "mailine sahip kullanıcı eklendi");
				return new ApiResponse<EmployeeRegisterDTO>(dto);
			}
            catch (Exception ex)
            {
				_logger.TakeWarningLog("Kullanıcı eklenirken hata olustu");
				return new ApiResponse<EmployeeRegisterDTO>(ex.Message);
            }
        }

    }
}
