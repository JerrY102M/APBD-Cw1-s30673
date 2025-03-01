namespace APBD_Cw1_s30673.Models;

public class Rental
{
    public Rental(string id, User user, Equipment equipment, DateTime rentDate, int days)
    {
        Id = id;
        User = user;
        Equipment = equipment;
        RentDate = rentDate;
        DueDate = rentDate.AddDays(days);
    }

    public string Id { get; }
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public decimal Fine { get; private set; }

    public bool IsActive => ReturnDate == null;

    public bool IsOverdue(DateTime date)
    {
        return IsActive && date.Date > DueDate.Date;
    }

    public void Close(DateTime returnDate, decimal fine)
    {
        ReturnDate = returnDate;
        Fine = fine;
    }

    public string GetDetails()
    {
        var status = IsActive
            ? $"aktywne, termin zwrotu: {DueDate:yyyy-MM-dd}"
            : $"zwrócone: {ReturnDate!.Value:yyyy-MM-dd}, kara: {Fine} zł";

        return $"{Id} | {User.FullName} -> {Equipment.Name} ({Equipment.Id}) | {status}";
    }
}
