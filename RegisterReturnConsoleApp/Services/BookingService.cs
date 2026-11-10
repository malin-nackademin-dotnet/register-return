
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

    public ReturnValidationResult CanRegisterReturn(Status bookingStatus)
    {
        return bookingStatus switch
        {
            Status.Booked or Status.Canceled => ReturnValidationResult.NotCheckedOut,
            Status.Returned => ReturnValidationResult.AlreadyReturned,
            Status.CheckedOut => ReturnValidationResult.CanReturn,
            _ => throw new ArgumentOutOfRangeException(nameof(bookingStatus)),
        };
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
