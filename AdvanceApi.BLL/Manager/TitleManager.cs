using AdvanceApi.BLL.Mapper;
using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.UnitOfWork;
using AdvanceApi.DTO.BusinessUnit;
using AdvanceApi.DTO.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.BLL.Manager
{
	public class TitleManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly MyMapper _mapper;
		public TitleManager(IUnitOfWork unitOfWork, MyMapper mapper)
        {
               _unitOfWork = unitOfWork;
				_mapper = mapper;
        }
		public async Task<ApiResponse<List<TitleSelectDTO>>> GetAllTitles()
		{
			try
			{
				var titles = await _unitOfWork.TitleDAL.GetAllTitles();
				var mapped = _mapper.Map<List<Title>, List<TitleSelectDTO>>(titles);
				return new ApiResponse<List<TitleSelectDTO>>(mapped);
			}
			catch (Exception ex)
			{
				return new ApiResponse<List<TitleSelectDTO>>(ex.Message);
			}
		}
	}
}
