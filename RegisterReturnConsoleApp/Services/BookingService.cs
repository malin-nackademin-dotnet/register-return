
using RegisterReturnConsoleApp.Interfaces;
using RegisterReturnConsoleApp.Models;

namespace RegisterReturnConsoleApp.Services;

public class BookingService(IBookingRepository repository) : IBookingService
{
    public Booking GetBooking(Guid bookingId)
    {
        var booking = repository.GetById(bookingId);

        if (booking is null)
            throw new ArgumentException($"No booking with id '{booking}' was found.");

        return booking;
    }

    //ta med i klassdiagram
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

    //ta med i klassdiagram
    public bool RegisterReturn(Booking booking, Guid employeeId) //var först bara (Booking booking), kan inte bara ta emot booking, måste ta emot ett id från ui lagret och inte skapa det här
    {
        if (booking.Status != Status.CheckedOut)
            throw new InvalidOperationException("The booking is not checked out and cannot be returned.");

        booking.SetStatus(Status.Returned);
        booking.SetReturnedByEmployeeId(employeeId);
        booking.SetReturndAt(DateOnly.FromDateTime(DateTime.Now));

        bool result = repository.Update(booking);

        return result;
    }
}
