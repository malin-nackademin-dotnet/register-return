using RegisterReturnConsoleApp.Models;

namespace RegisterReturnConsoleApp.Interfaces;

public interface IBookingRepository
{
    Booking? GetById(Guid bookingId);
    bool Update(Booking booking);
}
