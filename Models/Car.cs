public class Car
{
    public int Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Horsepower { get; set; }
    public int MaxSpeed { get; set; }

    public override string ToString()
    {
        return $"ID: {Id};\n" +
            $"Manufacturer: {Manufacturer};\n" +
            $"Model: {Model}\n" +
            $"Year: {Year}\n" +
            $"Horsepower: {Horsepower}\n" +
            $"MaxSpeed: {MaxSpeed}.";
    }
}