using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Claims;
using Travel_Experts_Application.BLL.Interfaces;
using Travel_Experts_Application.BLL.Repositories;
using Travel_Experts_Application.Lib.Models;

namespace Travel_Experts_Application.Web.Controllers
{
    public class BookingDetailController : Controller
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPackageRepository _packageRepository;

        public BookingDetailController(IBookingDetailRepository bookingDetailRepository, IHttpContextAccessor httpContextAccessor, ICustomerRepository customerRepository, IBookingRepository bookingRepository, IPackageRepository packageRepository)
        {
            _bookingDetailRepository = bookingDetailRepository;
            _httpContextAccessor = httpContextAccessor;
            _customerRepository = customerRepository;
            _bookingRepository = bookingRepository;
            _packageRepository = packageRepository;
        }


        // GET: BookingDetailController
        public async Task<ActionResult> BookingById()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect unauthenticated users to the home index
                return RedirectToAction("Login", "Account");
            }

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Retrieve CustomerId associated with the current user
            var customerId = await _customerRepository.GetCustomerIdByUserId(userId);

            if (customerId != null)
            {
                // Retrieve BookingId(s) associated with the CustomerId
                var bookingIds = await _bookingRepository.GetBookingIdsByCustomerId(customerId);

                // Retrieve BookingDetail records associated with the BookingId(s)
                IList<BookingDetail> bookingDetails = await _bookingDetailRepository.GetBookingDetailsByBookingIds(bookingIds);

                return View(bookingDetails);
            }

            // Handle the case where the user is not associated with a customer
            return View(new List<BookingDetail>());
        }

        public async Task<ActionResult> BookNew()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect unauthenticated users to the home index
                return RedirectToAction("Login", "Account");
            }

            await LoadPackages();
            return View();
        }

        public async Task<ActionResult> ThankYou()
        {
            return View();
        }

        private async Task LoadPackages()
        {
            IList<Package> packages = await _packageRepository.GetAllPackage();

            ViewData["Packages"] = packages.Select(x => new SelectListItem { Text = x.PkgName, Value = x.PackageId.ToString() });
        }
    }
}
