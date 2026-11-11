using RegisterReturnConsoleApp.Models;
using RegisterReturnConsoleApp.Services;

namespace RegisterReturnConsoleApp.Interfaces;

public interface IBookingService
{
    Booking GetBooking(Guid bookingId);
    bool RegisterReturn(Booking booking);
}
