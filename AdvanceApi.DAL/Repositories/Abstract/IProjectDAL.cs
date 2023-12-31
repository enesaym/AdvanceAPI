﻿using AdvanceApi.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.Repositories.Abstract
{
    public interface IProjectDAL
    {
        Task<List<Project>> GetProjectsByEmployeeID(int id);
        Task<Project> GetProjectById(int Id);
    }
}
