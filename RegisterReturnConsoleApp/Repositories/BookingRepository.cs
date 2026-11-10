
using RegisterReturnConsoleApp.Interfaces;
using RegisterReturnConsoleApp.Models;

namespace RegisterReturnConsoleApp.Repositories;

public class BookingRepository : IBookingRepository
{
    public Guid BookingId { get; } = Guid.NewGuid();
    private readonly List<Booking> _bookings;
    public BookingRepository()
    {
        _bookings =
        [
            new Booking(
                BookingId,
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DateOnly(2026, 11, 3),
                new DateOnly(2026, 11, 3),
                Guid.NewGuid(),
                null,
                null,
                8000m
            )
        ];
    }
    public Booking? GetById(Guid bookingId)
    {
        var booking = _bookings.FirstOrDefault(booking => booking.Id == bookingId);

        return booking;
    }

    public bool Update(Booking booking)
    {
        bool databaseError = true; //Ändra till false för att simulera databasfel

        if (databaseError)
            return false;

        int index = _bookings.FindIndex(b => b.Id == booking.Id);

        _bookings[index] = booking;

        return true;
    }
}
