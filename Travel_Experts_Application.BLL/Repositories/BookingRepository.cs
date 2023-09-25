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
    public class BookingRepository : IBookingRepository
    {
        private readonly TravelExpertsDbContext _context;

        public BookingRepository(TravelExpertsDbContext context)
        {
            _context = context;
        }

        //public async Task<int?> GetBookingIdsByCustomerId(int customerId)
        //{
        //    var booking = await _context.Bookings
        //    .Where(c => c.CustomerId == customerId)
        //    .Select(c => c.BookingId)
        //    .FirstOrDefaultAsync();

        //    return booking;
        //}

        public async Task<List<int>> GetBookingIdsByCustomerId(int customerId)
        {
            var bookingIds = await _context.Bookings
                .Where(b => b.CustomerId == customerId)
                .Select(b => b.BookingId)
                .ToListAsync();

            return bookingIds;
        }
    }
}
