namespace APBD_Cw1_s30673.Models;

public class Laptop : Equipment
{
    public Laptop(string id, string name, int ramGb, string processor)
        : base(id, name)
    {
        RamGb = ramGb;
        Processor = processor;
    }

    public int RamGb { get; }
    public string Processor { get; }

    public override string GetDetails()
    {
        return $"Laptop | {Id} | {Name} | RAM: {RamGb} GB | CPU: {Processor} | Status: {Status}";
    }
}
