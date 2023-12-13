using AdvanceApi.CORE.Entities;
using AdvanceApi.DAL.Repositories.Concrete;
using AdvanceApi.DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
    

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork uow)
		{
			_logger = logger;
			_unitOfWork = uow;
		}

		[HttpGet]
		public async Task <IEnumerable<BusinessUnit>> Get()
		{	return await _unitOfWork.UnitDAL.GetAllUnits();

		
			
		}
	}
}
