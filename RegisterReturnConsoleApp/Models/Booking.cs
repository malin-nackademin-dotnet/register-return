
namespace RegisterReturnConsoleApp.Models;

public class Booking(Guid equipmentItemId, Guid customerId, DateOnly startDate, DateOnly endDate, Guid? checkedOutByEmployeeId, Guid? returnedByEmployeeId, DateOnly? returnedAt, decimal price)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid EquipmentItemId { get; private set; } = equipmentItemId;
    public Guid CustomerId { get; private set; } = customerId;
    public DateOnly StartDate { get; private set; } = startDate;
    public DateOnly EndDate { get; private set; } = endDate;
    public Guid? CheckedOutByEmployee { get; private set; } = checkedOutByEmployeeId;
    public Guid? ReturnedByEmployeeId { get; private set; } = returnedByEmployeeId;
    public DateOnly? ReturnedAt { get; private set; } = returnedAt; // ta med i klassdiagram
    public Status Status { get; private set; } = Status.Booked;
    public decimal TotalPrice { get; private set; } = price;

    //behövs tas med i sekvensdiagram?
    public void SetStatus(Status status)
    {
        Status = status;
    }

    public void SetReturnedByEmployeeId(Guid employeeId)
    {
        ReturnedByEmployeeId = employeeId;
    }

    public void SetReturndAt(DateOnly returnedAt)
    {
        ReturnedAt = returnedAt;
    }
}
