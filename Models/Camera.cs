namespace APBD_Cw1_s30673.Models;

public class Camera : Equipment
{
    public Camera(string id, string name, int megapixels, bool hasTripod)
        : base(id, name)
    {
        Megapixels = megapixels;
        HasTripod = hasTripod;
    }

    public int Megapixels { get; }
    public bool HasTripod { get; }

    public override string GetDetails()
    {
        return $"Camera | {Id} | {Name} | Mpix: {Megapixels} | Tripod: {HasTripod} | Status: {Status}";
    }
}
