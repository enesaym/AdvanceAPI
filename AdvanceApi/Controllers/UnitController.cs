using AdvanceApi.BLL.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvanceApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UnitController : ControllerBase
	{
		BusinessUnitManager _businessUnitManager;
        public UnitController(BusinessUnitManager unitManager)
        {
				_businessUnitManager= unitManager;
        }

        [HttpGet("GetAllUnits")]
		public async Task<IActionResult> GetAllUnits()
		{
			var units =await _businessUnitManager.GetAllBusinessUnit();
			if (units == null)
				return NotFound();
			return Ok(units.Data);
		}

	}
}
