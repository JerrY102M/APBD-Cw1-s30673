namespace APBD_Cw1_s30673.Models;

public class Student : User
{
    public Student(string id, string firstName, string lastName, string studentNumber, string faculty)
        : base(id, firstName, lastName, UserType.Student)
    {
        StudentNumber = studentNumber;
        Faculty = faculty;
    }

    public string StudentNumber { get; }
    public string Faculty { get; }

    public override string GetDetails()
    {
        return $"Student | {Id} | {FullName} | Nr albumu: {StudentNumber} | Kierunek: {Faculty}";
    }
}
