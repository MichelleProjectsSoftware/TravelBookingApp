using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Experts_Application.BLL.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> GetCustomerIdByUserId(string userId);
    }
}
