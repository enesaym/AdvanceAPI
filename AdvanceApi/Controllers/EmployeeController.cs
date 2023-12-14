using AdvanceApi.BLL.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        EmployeeManager _manager;
        public EmployeeController(EmployeeManager manager)
        {
                _manager=manager;
        }
        [HttpGet("GetEmployeeBase")]
        public async Task<IActionResult> GetEmployeeBase()
        {
            var employees = await _manager.GetEmployeeBase();
            if (employees == null)
                return NotFound();
            return Ok(employees.Data);
        }
    }
}
