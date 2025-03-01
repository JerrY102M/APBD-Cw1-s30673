using APBD_Cw1_s30673.Models;

namespace APBD_Cw1_s30673.Repositories;

public interface IRentalRepository
{
    void Add(Rental rental);
    List<Rental> GetAll();
    Rental? GetById(string id);
    List<Rental> GetActiveForUser(string userId);
    Rental? GetActiveForEquipment(string equipmentId);
    List<Rental> GetOverdue(DateTime date);
}
