﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DTO.Advance
{
    public class AdvanceApproveDTO
    {
        public int EmployeeID { get; set; }

        public int AdvanceID { get; set; }

        public int StatusID { get; set; }

        public int TitleID { get; set; }

        public decimal ApprovedAmount { get; set; }
    }
}
