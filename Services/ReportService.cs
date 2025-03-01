using System.Text;
using APBD_Cw1_s30673.Models;
using APBD_Cw1_s30673.Repositories;

namespace APBD_Cw1_s30673.Services;

public class ReportService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRentalRepository _rentalRepository;

    public ReportService(IEquipmentRepository equipmentRepository, IUserRepository userRepository, IRentalRepository rentalRepository)
    {
        _equipmentRepository = equipmentRepository;
        _userRepository = userRepository;
        _rentalRepository = rentalRepository;
    }

    public string BuildSummary(DateTime date)
    {
        var equipmentList = _equipmentRepository.GetAll();
        var userList = _userRepository.GetAll();
        var rentalList = _rentalRepository.GetAll();
        var overdueList = _rentalRepository.GetOverdue(date);

        var builder = new StringBuilder();
        builder.AppendLine("RAPORT KOŃCOWY");
        builder.AppendLine("----------------------------");
        builder.AppendLine($"Użytkownicy: {userList.Count}");
        builder.AppendLine($"Sprzęt razem: {equipmentList.Count}");
        builder.AppendLine($"Dostępny sprzęt: {equipmentList.Count(x => x.Status == EquipmentStatus.Available)}");
        builder.AppendLine($"Wypożyczony sprzęt: {equipmentList.Count(x => x.Status == EquipmentStatus.Rented)}");
        builder.AppendLine($"Niedostępny sprzęt: {equipmentList.Count(x => x.Status == EquipmentStatus.Unavailable)}");
        builder.AppendLine($"Aktywne wypożyczenia: {rentalList.Count(x => x.IsActive)}");
        builder.AppendLine($"Przeterminowane wypożyczenia: {overdueList.Count}");
        builder.AppendLine($"Suma naliczonych kar: {rentalList.Sum(x => x.Fine)} zł");
        builder.AppendLine();
        builder.AppendLine("Sprzęt wg typu:");
        builder.AppendLine($"Laptopy: {equipmentList.Count(x => x is Laptop)}");
        builder.AppendLine($"Projektory: {equipmentList.Count(x => x is Projector)}");
        builder.AppendLine($"Kamery: {equipmentList.Count(x => x is Camera)}");

        return builder.ToString();
    }
}
