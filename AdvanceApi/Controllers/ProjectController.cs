using AdvanceApi.BLL.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        ProjectManager _projectManager;
        public ProjectController(ProjectManager projectManager)
        {
                _projectManager = projectManager;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProjectsByEmployeeID(int id)
        {
            var projects = await _projectManager.GetProjectsByEmployeeID(id);
            if (projects == null)
                return NotFound();
            return Ok(projects.Data);
        }

    }
}
