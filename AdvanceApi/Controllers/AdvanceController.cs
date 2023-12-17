using AdvanceApi.BLL.Manager;
using AdvanceApi.DTO.Advance;
using AdvanceApi.DTO.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvanceApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdvanceController : ControllerBase
	{
        private readonly AdvanceManager _advanceManager;
        public AdvanceController(AdvanceManager manager)
        {
                _advanceManager = manager;
        }
		[HttpPost("AddAdvance")]
		public async Task<IActionResult> AddAdvance([FromBody] AdvanceInsertDTO dto)
		{
			var advance = await _advanceManager.InsertAdvanceAndHistory(dto);
			if (advance.Success==true)
			{
                return Ok(advance);
            }
			else
			{
				return null;
			}
		}
        [HttpGet("GetAdvanceWithAll/{id}")]
        public async Task<IActionResult> GetAdvance(int id)
        {
            var advance = await _advanceManager.GetAdvanceAndHistory(id);
            if (advance.Success == true)
            {
                return Ok(advance.Data);
            }
            else
            {
                return null;
            }
        }
        [HttpGet("GetPendingApprovalAdvance/{id}")]
        public async Task<IActionResult> GetPendingApprovalAdvance(int id)
        {
            var advance = await _advanceManager.GetPendingApprovalAdvances(id);
            if (advance.Success == true)
            {
                return Ok(advance.Data);
            }
            else
            {
                return null;
            }
        }

    }
}
