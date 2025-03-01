using APBD_Cw1_s30673.Models;
using APBD_Cw1_s30673.Repositories;
using APBD_Cw1_s30673.Utils;

namespace APBD_Cw1_s30673.Services;

public class RentalService
{
    private readonly IUserRepository _userRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly IdGenerator _idGenerator;
    private readonly RulesService _rulesService;

    public RentalService(
        IUserRepository userRepository,
        IEquipmentRepository equipmentRepository,
        IRentalRepository rentalRepository,
        IdGenerator idGenerator,
        RulesService rulesService)
    {
        _userRepository = userRepository;
        _equipmentRepository = equipmentRepository;
        _rentalRepository = rentalRepository;
        _idGenerator = idGenerator;
        _rulesService = rulesService;
    }

    public ServiceResult<Rental> RentEquipment(string userId, string equipmentId, int days, DateTime rentDate)
    {
        if (days <= 0)
        {
            return ServiceResult<Rental>.Fail("Liczba dni musi być większa od 0.");
        }

        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return ServiceResult<Rental>.Fail("Nie ma takiego użytkownika.");
        }

        var equipment = _equipmentRepository.GetById(equipmentId);
        if (equipment == null)
        {
            return ServiceResult<Rental>.Fail("Nie ma takiego sprzętu.");
        }

        if (equipment.Status == EquipmentStatus.Unavailable)
        {
            return ServiceResult<Rental>.Fail("Tego sprzętu nie można wypożyczyć, bo jest niedostępny.");
        }

        if (_rentalRepository.GetActiveForEquipment(equipmentId) != null)
        {
            return ServiceResult<Rental>.Fail("Ten sprzęt jest już wypożyczony.");
        }

        var activeCount = _rentalRepository.GetActiveForUser(userId).Count;
        var limit = _rulesService.GetLimitForUser(user);
        if (activeCount >= limit)
        {
            return ServiceResult<Rental>.Fail($"Użytkownik ma już limit wypożyczeń. Limit = {limit}.");
        }

        var rental = new Rental(_idGenerator.NextRentalId(), user, equipment, rentDate, days);
        equipment.MarkAsRented();
        _rentalRepository.Add(rental);

        return ServiceResult<Rental>.Ok(rental, $"Wypożyczono sprzęt {equipment.Name} dla {user.FullName}.");
    }

    public ServiceResult ReturnEquipment(string rentalId, DateTime returnDate)
    {
        var rental = _rentalRepository.GetById(rentalId);
        if (rental == null)
        {
            return ServiceResult.Fail("Nie znaleziono wypożyczenia.");
        }

        if (!rental.IsActive)
        {
            return ServiceResult.Fail("To wypożyczenie jest już zamknięte.");
        }

        var fine = _rulesService.CalculateFine(rental.DueDate, returnDate);
        rental.Close(returnDate, fine);
        rental.Equipment.MarkAsAvailable();

        if (fine > 0)
        {
            return ServiceResult.Ok($"Zwrot po terminie. Kara = {fine} zł.");
        }

        return ServiceResult.Ok("Zwrot w terminie. Kara = 0 zł.");
    }

    public List<Rental> GetActiveRentalsForUser(string userId)
    {
        return _rentalRepository.GetActiveForUser(userId);
    }

    public List<Rental> GetOverdueRentals(DateTime date)
    {
        return _rentalRepository.GetOverdue(date);
    }

    public List<Rental> GetAllRentals()
    {
        return _rentalRepository.GetAll();
    }
}
