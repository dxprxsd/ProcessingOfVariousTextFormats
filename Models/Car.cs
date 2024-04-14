using System;

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
            $"Model: {Model};\n" +
            $"Year: {Year};\n" +
            $"Horsepower: {Horsepower};\n" +
            $"MaxSpeed: {MaxSpeed}.";
    }

    public static Car CreateCarFromConsoleInput()
    {
        Car car = new Car();

        Console.Write("Name: ");
        car.Manufacturer = Console.ReadLine();

        Console.Write("Model: ");
        car.Model = Console.ReadLine();
    
        while (true)
        {
            Console.Write("Year: ");
            try
            {
                car.Year = Int32.Parse(Console.ReadLine());
                break;
            }
            catch (FormatException) { }
        }

        while (true)
        {
            Console.Write("Horsepower: ");
            try
            {
                car.Horsepower = Int32.Parse(Console.ReadLine());
                break;
            }
            catch (FormatException) { }
        }

        while (true)
        {
            Console.Write("MaxSpeed: ");
            try
            {
                car.MaxSpeed = Int32.Parse(Console.ReadLine());
                break;
            }
            catch (FormatException) { }
        }

        return car;
    }
}