using tecnosor.cleanarchitecture.common.domain;
namespace stolenCars.publication.domain;

public class Vehicle : Entity
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Vin { get; set; }
    public string Plate { get; set; }

    public Vehicle(string id, string brand, string model, string vin, string plate) : base(id)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Vin = vin;
        Plate = plate;
    }
}