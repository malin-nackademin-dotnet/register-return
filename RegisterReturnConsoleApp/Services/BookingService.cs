
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

    public bool RegisterReturn(Booking booking)
    {
        //Används för att simulera om en bokning blivit returnerad innan man valt bekräfta
        //booking.SetStatus(Status.Returned);

        if (booking.Status != Status.CheckedOut)
            throw new InvalidOperationException("Bokningen har redan returnerats.");

        booking.SetStatus(Status.Returned);
        booking.SetReturnedByEmployeeId(EmployeeId);
        booking.SetReturndAt(DateOnly.FromDateTime(DateTime.Now));

        bool result = repository.Update(booking);

        return result;
    }
}
