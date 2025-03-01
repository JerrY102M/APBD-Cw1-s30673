namespace APBD_Cw1_s30673.Models;

public abstract class Equipment
{
    protected Equipment(string id, string name)
    {
        Id = id;
        Name = name;
        Status = EquipmentStatus.Available;
    }

    public string Id { get; }
    public string Name { get; }
    public EquipmentStatus Status { get; private set; }
    public string? UnavailableReason { get; private set; }

    public void MarkAsRented()
    {
        Status = EquipmentStatus.Rented;
        UnavailableReason = null;
    }

    public void MarkAsAvailable()
    {
        Status = EquipmentStatus.Available;
        UnavailableReason = null;
    }

    public void MarkAsUnavailable(string reason)
    {
        Status = EquipmentStatus.Unavailable;
        UnavailableReason = reason;
    }

    public abstract string GetDetails();
}
