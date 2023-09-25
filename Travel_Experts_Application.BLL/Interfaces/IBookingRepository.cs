using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Experts_Application.BLL.Interfaces
{
    public interface IBookingRepository
    {
        //Task<int?> GetBookingIdsByCustomerId(int customerId);
        Task<List<int>> GetBookingIdsByCustomerId(int customerId);

    }
}
