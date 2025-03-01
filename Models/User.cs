namespace APBD_Cw1_s30673.Models;

public abstract class User
{
    protected User(string id, string firstName, string lastName, UserType userType)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
    }

    public string Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public UserType UserType { get; }

    public string FullName => $"{FirstName} {LastName}";

    public abstract string GetDetails();
}
