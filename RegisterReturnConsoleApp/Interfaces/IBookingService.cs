using RegisterReturnConsoleApp.Models;
using RegisterReturnConsoleApp.Services;

namespace RegisterReturnConsoleApp.Interfaces;

public interface IBookingService
{
    Booking GetBooking(Guid bookingId);
    ReturnValidationResult CanRegisterReturn(Status bookingStatus);
    bool RegisterReturn(Booking booking);
}
