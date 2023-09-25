using Travel_Experts_Application.Lib.Models;

namespace Travel_Experts_Application.Web.ViewModel
{
    public class BookingSearchViewModel
    {
        public int BookingDetailId { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public DateTime? TripStart { get; set; }
        public DateTime? TripEnd { get; set; }
        public int ItineraryNo { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        // Add other customer properties as needed

        // Constructor to map data from BookingDetail and Customer entities
        public BookingSearchViewModel(BookingDetail bookingDetail, Customer customer)
        {
            BookingDetailId = bookingDetail.BookingDetailId;
            Description = bookingDetail.Description;
            Destination = bookingDetail.Destination;
            TripStart = bookingDetail.TripStart;
            TripEnd = bookingDetail.TripEnd;
            ItineraryNo = (int)bookingDetail.ItineraryNo;

            // Map customer properties
            if (customer != null)
            {
                CustomerId = customer.CustomerId;
                CustomerName = $"{customer.CustFirstName} {customer.CustLastName}";
                // Add other customer property mappings as needed
            }
        }
    }
}
