using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Travel_Experts_Application.BLL.Interfaces;
using Travel_Experts_Application.Lib.Models;
using Travel_Experts_Application.Web.ViewModel;

namespace Travel_Experts_Application.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminSearchController : Controller
    {
        private readonly TravelExpertsDbContext _context;

        public AdminSearchController(TravelExpertsDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Search(int? customerId, DateTime? tripStart, DateTime? tripEnd)
        {
            var query = _context.BookingDetails.AsQueryable();

            // Join BookingDetails with Bookings and Customers
            var joinedQuery = query
                .Join(
                    _context.Bookings,
                    bd => bd.BookingId,
                    booking => booking.BookingId,
                    (bd, booking) => new { bd, booking }
                )
                .Join(
                    _context.Customers,
                    combined => combined.booking.CustomerId,
                    customer => customer.CustomerId,
                    (combined, customer) => new { BookingDetail = combined.bd, Customer = customer }
                );

            // Apply filtering based on customerId if provided
            if (customerId.HasValue)
            {
                joinedQuery = joinedQuery.Where(combined => combined.Customer.CustomerId == customerId);
            }

            // Additional filtering based on tripStart and tripEnd if provided
            if (tripStart.HasValue)
            {
                joinedQuery = joinedQuery.Where(combined => combined.BookingDetail.TripStart >= tripStart);
            }

            if (tripEnd.HasValue)
            {
                joinedQuery = joinedQuery.Where(combined => combined.BookingDetail.TripEnd <= tripEnd);
            }

            // Select and map the data to the ViewModel
            var viewModelList = await joinedQuery
                .Select(combined => new BookingSearchViewModel(combined.BookingDetail, combined.Customer)
                {
                    BookingDetailId = combined.BookingDetail.BookingDetailId,
                    Description = combined.BookingDetail.Description,
                    Destination = combined.BookingDetail.Destination,
                    TripStart = combined.BookingDetail.TripStart,
                    TripEnd = combined.BookingDetail.TripEnd,
                    ItineraryNo = (int)combined.BookingDetail.ItineraryNo,
                    CustomerId = combined.Customer.CustomerId,
                    CustomerName = $"{combined.Customer.CustFirstName} {combined.Customer.CustLastName}"
                })
                .ToListAsync();

            return View(viewModelList);
        }
    }
}
