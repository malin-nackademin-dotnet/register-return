using RegisterReturnConsoleApp.Models;
using RegisterReturnConsoleApp.Repositories;
using RegisterReturnConsoleApp.Services;

var bookingRepository = new BookingRepository();
var bookingService = new BookingService(bookingRepository);

var booking = bookingService.GetBooking(bookingRepository.BookingId);

Console.WriteLine("Registrera återlämning (välj enter för att fortsätta)");

while (Console.ReadKey(true).Key != ConsoleKey.Enter)
{
}

DateOnly todaysDate = DateOnly.FromDateTime(DateTime.Now);

switch (booking.Status)
{
    case Status.Returned:
        Console.WriteLine("Utrutningen har redan återlämnats.");
        break;
    case Status.Booked:
    case Status.Canceled:
        Console.WriteLine("Utrustningen har inte hämtats än eller bokningen har avbokats.");
        break;
    case Status.CheckedOut:
        var employeeId = bookingService.EmployeeId;

        Console.WriteLine();
        Console.WriteLine($"Personal som registrerar återlämning: {employeeId}");
        Console.WriteLine();
        Console.WriteLine($"Vill du registrera återlämning för utrustning med id {booking.EquipmentId}?");
        Console.WriteLine();
        Console.WriteLine($"Bokning {booking.Id} kommer avslutas {todaysDate} (Y/N)");

        ConsoleKey key;

        do
        {
            key = Console.ReadKey(true).Key;
        }
        while (key != ConsoleKey.Y && key != ConsoleKey.N);

        if (key == ConsoleKey.Y)
        {
            try
            {
                bool isRegistered = bookingService.RegisterReturn(booking);

                if (isRegistered)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Utrustning {booking.EquipmentId} har registrerats som återlämnad och bokning {booking.Id} har avslutas.");
                }
                else
                {     
                    Console.WriteLine();
                    Console.WriteLine("Bokningen kunde inte uppdateras i databasen.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        break;
}
