using APBD_Cw1_s30673.Models;

namespace APBD_Cw1_s30673.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly List<Rental> _rentalList = new();

    public void Add(Rental rental)
    {
        _rentalList.Add(rental);
    }

    public List<Rental> GetAll()
    {
        return _rentalList;
    }

    public Rental? GetById(string id)
    {
        return _rentalList.FirstOrDefault(x => x.Id == id);
    }

    public List<Rental> GetActiveForUser(string userId)
    {
        return _rentalList.Where(x => x.User.Id == userId && x.IsActive).ToList();
    }

    public Rental? GetActiveForEquipment(string equipmentId)
    {
        return _rentalList.FirstOrDefault(x => x.Equipment.Id == equipmentId && x.IsActive);
    }

    public List<Rental> GetOverdue(DateTime date)
    {
        return _rentalList.Where(x => x.IsOverdue(date)).ToList();
    }
}
