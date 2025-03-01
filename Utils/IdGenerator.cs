namespace APBD_Cw1_s30673.Utils;

public class IdGenerator
{
    private int _equipmentId = 1;
    private int _userId = 1;
    private int _rentalId = 1;

    public string NextEquipmentId()
    {
        return $"EQ{_equipmentId++.ToString("000")}";
    }

    public string NextUserId()
    {
        return $"USR{_userId++.ToString("000")}";
    }

    public string NextRentalId()
    {
        return $"RNT{_rentalId++.ToString("000")}";
    }
}
