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
            $"Address: {Address}\n" +
            $"PositionX: {PositionX}\n" +
            $"PositionY: {PositionY}\n" +
            $"Year: {Year}.";
    }
}