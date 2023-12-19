using AdvanceApi.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.Repositories.Abstract
{
    public interface IPaymentDAL
    {
        Task<int> PaymentInsert(Payment payment);
    }
}
