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
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly TravelExpertsDbContext _context;
        public BookingDetailRepository(TravelExpertsDbContext context)
        {
            _context = context;
        }

        public async Task<IList<BookingDetail>> GetAllBookingDetail()
        {
            return await _context.BookingDetails.ToListAsync();
        }

        public async Task<IList<BookingDetail>> GetBookingDetailsByBookingIds(IEnumerable<int> bookingIds)
        {
            var bookingIdsList = bookingIds.ToList();

            var bookingDetails = await _context.BookingDetails
                .Where(bd => bookingIdsList.Contains(bd.BookingId))
                .ToListAsync();

            return bookingDetails;

        }

    }
}
