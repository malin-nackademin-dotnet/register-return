
using RegisterReturnConsoleApp.Interfaces;
using RegisterReturnConsoleApp.Models;

namespace RegisterReturnConsoleApp.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly List<Booking> _bookings =
    [
        new Booking(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateOnly(2026, 11, 3),
            new DateOnly(2026, 11, 3),
            Guid.NewGuid(),
            null,
            8000m
        )
    ];
    public Booking? GetById(Guid bookingId)
    {
        var booking = _bookings.FirstOrDefault(booking => booking.Id == bookingId);

        return booking;
    }

    public bool Update(Booking booking)
    {
        ArgumentNullException.ThrowIfNull(booking);

        int index = _bookings.FindIndex(b => b.Id == booking.Id);

        if (index == -1)
            throw new KeyNotFoundException($"Booking with id {booking.Id} was not found.");

        _bookings[index] = booking;

        return true;
    }
}
