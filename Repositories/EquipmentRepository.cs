using APBD_Cw1_s30673.Models;

namespace APBD_Cw1_s30673.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _equipmentList = new();

    public void Add(Equipment equipment)
    {
        _equipmentList.Add(equipment);
    }

    public List<Equipment> GetAll()
    {
        return _equipmentList;
    }

    public Equipment? GetById(string id)
    {
        return _equipmentList.FirstOrDefault(x => x.Id == id);
    }
}
