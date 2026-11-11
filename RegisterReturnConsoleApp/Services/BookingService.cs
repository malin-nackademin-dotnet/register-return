
using RegisterReturnConsoleApp.Interfaces;
using RegisterReturnConsoleApp.Models;

namespace RegisterReturnConsoleApp.Services;

public class BookingService(IBookingRepository repository) : IBookingService
{
    public Guid EmployeeId { get; } = Guid.NewGuid();
    public Booking GetBooking(Guid bookingId)
    {
        var booking = repository.GetById(bookingId);

        if (booking is null)
            throw new ArgumentException($"No booking with id '{bookingId}' was found.");

        return booking;
    }

    //ta med i sekvensdiagram
    public bool RegisterReturn(Booking booking)
    {
        if (booking.Status != Status.CheckedOut)
            throw new InvalidOperationException("Bokningen är inte utlämnad och kan inte returners.");

        booking.SetStatus(Status.Returned);
        booking.SetReturnedByEmployeeId(EmployeeId);
        booking.SetReturndAt(DateOnly.FromDateTime(DateTime.Now));

        bool result = repository.Update(booking);

        return result;
    }
}
