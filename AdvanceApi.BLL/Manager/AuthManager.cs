﻿using AdvanceApi.BLL.Mapper;
using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.UnitOfWork;
using AdvanceApi.DTO.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.BLL.Manager
{
	public class AuthManager
	{
        IUnitOfWork _unitOfWork;
        MyMapper _mapper;
		public AuthManager(IUnitOfWork unitOfWork,MyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
		public async Task<ApiResponse<EmployeeLoginDTO>> Login(EmployeeLoginDTO dto)
		{
			try
			{
				var employee = _mapper.Map<EmployeeLoginDTO, Employee>(dto);
				var mapped = await _unitOfWork.AuthDAL.Login(employee,dto.Password);
				return new ApiResponse<EmployeeLoginDTO>(dto);
			}
			catch (Exception ex)
			{
				return new ApiResponse<EmployeeLoginDTO>(ex.Message);
			}
		}

		public async Task<ApiResponse<EmployeeRegisterDTO>> Register(EmployeeRegisterDTO dto)
        {
            try
            {
                var employee= _mapper.Map<EmployeeRegisterDTO,Employee>(dto);
                var mapped = await _unitOfWork.AuthDAL.Register(employee,dto.Password); 
				return new ApiResponse<EmployeeRegisterDTO>(dto);
			}
            catch (Exception ex)
            {
                return new ApiResponse<EmployeeRegisterDTO>(ex.Message);
            }
        }

    }
}
