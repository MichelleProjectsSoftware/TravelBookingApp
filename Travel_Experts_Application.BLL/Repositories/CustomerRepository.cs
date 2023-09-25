using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Experts_Application.BLL.Interfaces;
using Travel_Experts_Application.Lib.Models;

namespace Travel_Experts_Application.BLL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TravelExpertsDbContext _context;

        public CustomerRepository(TravelExpertsDbContext context) 
        {
             _context = context;
        }

        public async Task<int> GetCustomerIdByUserId(string userId)
        {
            var customer = await _context.Customers
            .Where(c => c.UserId == userId)
            .Select(c => c.CustomerId)
            .FirstOrDefaultAsync();

            return customer;
        }
    }
}
