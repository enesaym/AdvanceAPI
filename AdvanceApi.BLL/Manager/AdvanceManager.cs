﻿using AdvanceApi.BLL.Mapper;
using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.UnitOfWork;
using AdvanceApi.DTO.Advance;
using AdvanceApi.DTO.AdvanceHistory;
using AdvanceApi.DTO.Project;
using AdvanceApi.DTO.Status;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.BLL.Manager
{
	public class AdvanceManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly MyMapper _mapper;
		public AdvanceManager(IUnitOfWork unitOfWork,MyMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// for insert Advance and advanceHistory table
        /// </summary>
        /// <param name="advance"></param>
        /// <returns>Inserted Advance ID</returns>
        public async Task<ApiResponse<AdvanceInsertDTO>> InsertAdvanceAndHistory(AdvanceInsertDTO advance)
        {
			_unitOfWork.BeginTransaction();
			try
			{
				var mapped= _mapper.Map<AdvanceInsertDTO, Advance>(advance);
				mapped.StatusID = 101;
				mapped.RequestDate = DateTime.Now;
				int insertedAdvanceId = await _unitOfWork.AdvanceDAL.AdvanceInsert(mapped);

				if (insertedAdvanceId > 0)
				{
					// İkinci tabloya insert
					AdvanceHistoryInsertDTO advanceHistory = new AdvanceHistoryInsertDTO();
					advanceHistory.AdvanceID = insertedAdvanceId;
					advanceHistory.TransactorID = advance.EmployeeID;
					//talep ilk olusturuldugunda 201 = talep olusturuldu
					advanceHistory.StatusID = 201;
					advanceHistory.Date=DateTime.Now;
					advanceHistory.ApprovedAmount = advance.AdvanceAmount;
					var advanceHistoryMapped=_mapper.Map<AdvanceHistoryInsertDTO, AdvanceHistory>(advanceHistory);

					var insertedAdvanceHistory=await _unitOfWork.AdvanceHistoryDAL.InsertAdvanceHistory(advanceHistoryMapped);

					_unitOfWork.Commit();

					//eklenen advance id döner
					return new ApiResponse<AdvanceInsertDTO>(advance); 
				}
				else
				{
					
					return null; 
				}
			}
			catch (Exception ex)
			{
				_unitOfWork.RollBack();
				return new ApiResponse<AdvanceInsertDTO>(ex.Message);
			}
			finally 
			{
			_unitOfWork.TransactionDispose();

			}
		}


		//lazim olcak
		public async Task<ApiResponse<List<AdvanceSelectDTO>>> GetAdvanceAndHistory(int employeeID)
		{
			try
			{
                var result = await _unitOfWork.AdvanceDAL.GetEmployeeAdvances(employeeID);
                result.ForEach(async x =>
                {
                  
                    var status = await _unitOfWork.StatusDAL.GetStatusById(x.StatusID.Value);
                    x.Status = status;

                    var project = await _unitOfWork.ProjectDAL.GetProjectById(x.ProjectID.Value);

                    x.Project = project;
                });
				var mapped=_mapper.Map<List<Advance>,List<AdvanceSelectDTO>> (result);

				return new ApiResponse<List<AdvanceSelectDTO>>(mapped);

            }
			catch (Exception)
			{

				throw;
			}
		
        }
        






    }
}
