namespace APBD_Cw1_s30673.Models;

public class Projector : Equipment
{
    public Projector(string id, string name, int lumens, bool hasHdmi)
        : base(id, name)
    {
        Lumens = lumens;
        HasHdmi = hasHdmi;
    }

    public int Lumens { get; }
    public bool HasHdmi { get; }

    public override string GetDetails()
    {
        return $"Projector | {Id} | {Name} | Lumens: {Lumens} | HDMI: {HasHdmi} | Status: {Status}";
    }
}
