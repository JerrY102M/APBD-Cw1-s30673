using APBD_Cw1_s30673.Models;
using APBD_Cw1_s30673.Repositories;
using APBD_Cw1_s30673.Services;
using APBD_Cw1_s30673.Utils;

namespace APBD_Cw1_s30673;

public class Program
{
    public static void Main()
    {
        var idGenerator = new IdGenerator();

        var equipmentRepository = new EquipmentRepository();
        var userRepository = new UserRepository();
        var rentalRepository = new RentalRepository();

        var equipmentService = new EquipmentService(equipmentRepository, idGenerator);
        var userService = new UserService(userRepository, idGenerator);
        var rulesService = new RulesService();
        var rentalService = new RentalService(userRepository, equipmentRepository, rentalRepository, idGenerator, rulesService);
        var reportService = new ReportService(equipmentRepository, userRepository, rentalRepository);

        Console.WriteLine("UCZELNIANA WYPOŻYCZALNIA SPRZĘTU");
        Console.WriteLine();

        Console.WriteLine("1. Dodawanie użytkowników");
        var student1 = userService.AddStudent("Anna", "Kowalska", "s12345", "Informatyka");
        var student2 = userService.AddStudent("Marek", "Nowak", "s23456", "Zarządzanie informacją");
        var employee1 = userService.AddEmployee("Jan", "Wiśniewski", "IT", "Pracownik uczelni");
        ShowUsers(userService.GetAll());

        Console.WriteLine("2. Dodawanie sprzętu");
        var laptop1 = equipmentService.AddLaptop("Dell Latitude", 16, "Intel i5");
        var laptop2 = equipmentService.AddLaptop("Lenovo ThinkPad", 32, "Intel i7");
        var projector1 = equipmentService.AddProjector("Epson X1", 3200, true);
        var projector2 = equipmentService.AddProjector("BenQ Basic", 2800, true);
        var camera1 = equipmentService.AddCamera("Canon EOS", 24, true);
        var camera2 = equipmentService.AddCamera("Sony Alpha", 26, false);
        ShowEquipment(equipmentService.GetAll());

        Console.WriteLine("3. Oznaczenie jednego sprzętu jako niedostępny");
        var unavailableResult = equipmentService.MarkUnavailable(camera2.Id, "sprzęt w serwisie");
        ShowResult(unavailableResult);
        Console.WriteLine();

        Console.WriteLine("4. Lista tylko dostępnego sprzętu");
        ShowEquipment(equipmentService.GetAvailable());

        Console.WriteLine("5. Poprawne wypożyczenia");
        var rent1 = rentalService.RentEquipment(student1.Id, laptop1.Id, 7, new DateTime(2026, 3, 1));
        var rent2 = rentalService.RentEquipment(student1.Id, projector1.Id, 3, new DateTime(2026, 3, 2));
        var rent3 = rentalService.RentEquipment(employee1.Id, laptop2.Id, 10, new DateTime(2026, 3, 3));
        var rent4 = rentalService.RentEquipment(employee1.Id, camera1.Id, 2, new DateTime(2026, 3, 4));
        ShowResult(rent1);
        ShowResult(rent2);
        ShowResult(rent3);
        ShowResult(rent4);
        Console.WriteLine();

        Console.WriteLine("6. Próby niepoprawnych operacji");
        var wrong1 = rentalService.RentEquipment(employee1.Id, camera2.Id, 2, new DateTime(2026, 3, 5));
        var wrong2 = rentalService.RentEquipment(student1.Id, projector2.Id, 2, new DateTime(2026, 3, 5));
        var wrong3 = rentalService.RentEquipment(student2.Id, camera1.Id, 2, new DateTime(2026, 3, 5));
        ShowResult(wrong1);
        ShowResult(wrong2);
        ShowResult(wrong3);
        Console.WriteLine();

        Console.WriteLine("7. Aktywne wypożyczenia wybranego użytkownika");
        ShowRentals(rentalService.GetActiveRentalsForUser(student1.Id));

        Console.WriteLine("8. Zwrot w terminie");
        if (rent2.Data != null)
        {
            var returnOnTime = rentalService.ReturnEquipment(rent2.Data.Id, new DateTime(2026, 3, 5));
            ShowResult(returnOnTime);
        }
        Console.WriteLine();

        Console.WriteLine("9. Zwrot po terminie z karą");
        if (rent1.Data != null)
        {
            var lateReturn = rentalService.ReturnEquipment(rent1.Data.Id, new DateTime(2026, 3, 12));
            ShowResult(lateReturn);
        }
        Console.WriteLine();

        Console.WriteLine("10. Lista przeterminowanych wypożyczeń");
        ShowRentals(rentalService.GetOverdueRentals(new DateTime(2026, 3, 20)));

        Console.WriteLine("11. Raport końcowy");
        Console.WriteLine(reportService.BuildSummary(new DateTime(2026, 3, 20)));
    }

    private static void ShowUsers(List<User> users)
    {
        foreach (var user in users)
        {
            Console.WriteLine(user.GetDetails());
        }

        Console.WriteLine();
    }

    private static void ShowEquipment(List<Equipment> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            Console.WriteLine(equipment.GetDetails());
        }

        Console.WriteLine();
    }

    private static void ShowRentals(List<Rental> rentals)
    {
        if (rentals.Count == 0)
        {
            Console.WriteLine("Brak pozycji.");
            Console.WriteLine();
            return;
        }

        foreach (var rental in rentals)
        {
            Console.WriteLine(rental.GetDetails());
        }

        Console.WriteLine();
    }

    private static void ShowResult(ServiceResult result)
    {
        if (result.Success)
        {
            Console.WriteLine("OK: " + result.Message);
        }
        else
        {
            Console.WriteLine("BŁĄD: " + result.Message);
        }
    }
}
