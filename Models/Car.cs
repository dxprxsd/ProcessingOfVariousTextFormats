public class Car
{
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Horsepower { get; set; }
    public int MaxSpeed { get; set; }

    public override string ToString()
    {
        return $"Manufacturer: {Manufacturer};\n" +
            $"Model: {Model}\n" +
            $"Year: {Year}\n" +
            $"Horsepower: {Horsepower}\n" +
            $"MaxSpeed: {MaxSpeed}.";
    }
}