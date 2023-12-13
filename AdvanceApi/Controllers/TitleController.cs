using AdvanceApi.BLL.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvanceApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TitleController : ControllerBase
	{
		TitleManager _titleManager;
        public TitleController(TitleManager titleManager)
        {
                _titleManager = titleManager;
        }
		[HttpGet("GetAllTitles")]
		public async Task<IActionResult> GetAllTitles()
		{
			var titles = await _titleManager.GetAllTitles();
			if (titles == null)
				return NotFound();
			return Ok(titles.Data);
		}
	}
}
