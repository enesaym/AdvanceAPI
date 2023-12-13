using AdvanceApi.BLL.Mapper;
using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.UnitOfWork;
using AdvanceApi.DTO.BusinessUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.BLL.Manager
{
	public class BusinessUnitManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly MyMapper _mapper;

		public BusinessUnitManager(IUnitOfWork unitOfWork,MyMapper mapper)
        {
             _unitOfWork=unitOfWork;
			_mapper=mapper;
        }

		public async Task<ApiResponse<List<BusinessUnitSelectDTO>>> GetAllBusinessUnit()
		{
			try
			{
				var units=await _unitOfWork.UnitDAL.GetAllUnits();
				var mapped=_mapper.Map<List<BusinessUnit>,List<BusinessUnitSelectDTO>>(units);
				return new ApiResponse<List<BusinessUnitSelectDTO>>(mapped);
			}
			catch (Exception ex)
			{
				return new ApiResponse<List<BusinessUnitSelectDTO>>(ex.Message);
			}
		}


    }
}
