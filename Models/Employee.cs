namespace APBD_Cw1_s30673.Models;

public class Employee : User
{
    public Employee(string id, string firstName, string lastName, string department, string position)
        : base(id, firstName, lastName, UserType.Employee)
    {
        Department = department;
        Position = position;
    }

    public string Department { get; }
    public string Position { get; }

    public override string GetDetails()
    {
        return $"Employee | {Id} | {FullName} | Dział: {Department} | Stanowisko: {Position}";
    }
}
