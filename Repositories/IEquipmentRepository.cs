using APBD_Cw1_s30673.Models;

namespace APBD_Cw1_s30673.Repositories;

public interface IEquipmentRepository
{
    void Add(Equipment equipment);
    List<Equipment> GetAll();
    Equipment? GetById(string id);
}
