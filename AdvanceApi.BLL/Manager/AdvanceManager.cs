using AdvanceApi.BLL.Mapper;
using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.UnitOfWork;
using AdvanceApi.DTO.Advance;
using AdvanceApi.DTO.AdvanceHistory;
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
        public async Task<ApiResponse<int>> InsertAdvanceAndHistory(AdvanceInsertDTO advance)
        {
			_unitOfWork.BeginTransaction();
			try
			{
				var mapped= _mapper.Map<AdvanceInsertDTO, Advance>(advance);
				int insertedAdvanceId = await _unitOfWork.AdvanceDAL.AdvanceInsert(mapped);

				if (insertedAdvanceId > 0)
				{
					// İkinci tabloya insert
					AdvanceHistoryInsertDTO advanceHistory = new AdvanceHistoryInsertDTO();
					advanceHistory.AdvanceID = insertedAdvanceId; 

					var advanceHistoryMapped=_mapper.Map<AdvanceHistoryInsertDTO, AdvanceHistory>(advanceHistory);

					var insertedAdvanceHistory=await _unitOfWork.AdvanceHistoryDAL.InsertAdvanceHistory(advanceHistoryMapped);

					_unitOfWork.Commit();

					//eklenen advance id döner
					return new ApiResponse<int>(insertedAdvanceId); 
				}
				else
				{
					
					return null; 
				}
			}
			catch (Exception ex)
			{
				_unitOfWork.RollBack();
				return new ApiResponse<int>(ex.Message);
			}
			finally 
			{
			_unitOfWork.TransactionDispose();

			}
		}

    }
}
