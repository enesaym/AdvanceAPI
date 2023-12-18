using AdvanceApi.BLL.Mapper;
using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.UnitOfWork;
using AdvanceApi.DTO.Rule;
using AdvanceApi.DTO.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.BLL.Manager
{
    public class RuleManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MyMapper _mapper;
        public RuleManager(IUnitOfWork unitOfWork, MyMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
    }
}
