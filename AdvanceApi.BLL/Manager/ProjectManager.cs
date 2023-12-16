using AdvanceApi.BLL.Mapper;
using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.Repositories.Abstract;
using AdvanceApi.DAL.UnitOfWork;
using AdvanceApi.DTO.BusinessUnit;
using AdvanceApi.DTO.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.BLL.Manager
{
    public class ProjectManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MyMapper _mapper;
        public ProjectManager(IUnitOfWork unitOfWork, MyMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// get projects by employee ıd
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>emplooyees projects</returns>
        public async Task<ApiResponse<List<ProjectSelectDTO>>> GetProjectsByEmployeeID(int employeeId)
        {
            try
            {
                var projects = await _unitOfWork.ProjectDAL.GetProjectsByEmployeeID(employeeId);
                var mapped = _mapper.Map<List<Project>, List<ProjectSelectDTO>>(projects);
                return new ApiResponse<List<ProjectSelectDTO>>(mapped);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ProjectSelectDTO>>(ex.Message);
            }
        }

    }
}
