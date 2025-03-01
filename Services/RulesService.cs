using APBD_Cw1_s30673.Models;

namespace APBD_Cw1_s30673.Services;

public class RulesService
{
    public int GetLimitForUser(User user)
    {
        if (user.UserType == UserType.Student)
        {
            return 2;
        }

        return 5;
    }

    public decimal CalculateFine(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate.Date <= dueDate.Date)
        {
            return 0;
        }

        var lateDays = (returnDate.Date - dueDate.Date).Days;
        return lateDays * 10;
    }
}
