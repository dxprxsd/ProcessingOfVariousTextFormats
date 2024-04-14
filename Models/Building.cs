using System;

public class Building
{
    public string Owner { get; set; }
    public string Address { get; set; }
    public double PositionX { get; set; }
    public double PositionY { get; set; }
    public int Year { get; set; }

    public override string ToString()
    {
        return $"Owner: {Owner};\n" +
            $"Address: {Address};\n" +
            $"PositionX: {PositionX};\n" +
            $"PositionY: {PositionY};\n" +
            $"Year: {Year}.";
    }

    public static Building CreateBuildingFromConsoleInput()
    {
        Building building = new Building();

        Console.Write("Owner: ");
        building.Owner = Console.ReadLine();

        Console.Write("Address: ");
        building.Address = Console.ReadLine();

        while (true)
        {
            Console.Write("PositionX: ");
            try
            {
                building.PositionX = Int32.Parse(Console.ReadLine());
                break;
            }
            catch (FormatException) { }
        }

        while (true)
        {
            Console.Write("PositionY: ");
            try
            {
                building.PositionY = Int32.Parse(Console.ReadLine());
                break;
            }
            catch (FormatException) { }
        }

        while (true)
        {
            Console.Write("Year: ");
            try
            {
                building.Year = Int32.Parse(Console.ReadLine());
                break;
            }
            catch (FormatException) { }
        }

        return building;
    }
}