using APBD_Cw1_s30673.Models;
using APBD_Cw1_s30673.Repositories;
using APBD_Cw1_s30673.Utils;

namespace APBD_Cw1_s30673.Services;

public class EquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IdGenerator _idGenerator;

    public EquipmentService(IEquipmentRepository equipmentRepository, IdGenerator idGenerator)
    {
        _equipmentRepository = equipmentRepository;
        _idGenerator = idGenerator;
    }

    public Laptop AddLaptop(string name, int ramGb, string processor)
    {
        var laptop = new Laptop(_idGenerator.NextEquipmentId(), name, ramGb, processor);
        _equipmentRepository.Add(laptop);
        return laptop;
    }

    public Projector AddProjector(string name, int lumens, bool hasHdmi)
    {
        var projector = new Projector(_idGenerator.NextEquipmentId(), name, lumens, hasHdmi);
        _equipmentRepository.Add(projector);
        return projector;
    }

    public Camera AddCamera(string name, int megapixels, bool hasTripod)
    {
        var camera = new Camera(_idGenerator.NextEquipmentId(), name, megapixels, hasTripod);
        _equipmentRepository.Add(camera);
        return camera;
    }

    public List<Equipment> GetAll()
    {
        return _equipmentRepository.GetAll();
    }

    public List<Equipment> GetAvailable()
    {
        return _equipmentRepository.GetAll().Where(x => x.Status == EquipmentStatus.Available).ToList();
    }

    public ServiceResult MarkUnavailable(string equipmentId, string reason)
    {
        var equipment = _equipmentRepository.GetById(equipmentId);
        if (equipment == null)
        {
            return ServiceResult.Fail("Nie znaleziono sprzętu.");
        }

        if (equipment.Status == EquipmentStatus.Rented)
        {
            return ServiceResult.Fail("Nie można oznaczyć jako niedostępny, bo sprzęt jest teraz wypożyczony.");
        }

        equipment.MarkAsUnavailable(reason);
        return ServiceResult.Ok($"Sprzęt {equipment.Name} został oznaczony jako niedostępny.");
    }
}
