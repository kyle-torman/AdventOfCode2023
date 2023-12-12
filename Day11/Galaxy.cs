public class Galaxy
{
    public int Id {get;}
    public int ColumnNumber {get; }
    public int RowNumber {get; }

    public Galaxy(int id, int rowNumber, int columnNumber)
    {
        Id = id;
        RowNumber = rowNumber;
        ColumnNumber = columnNumber;   
    }

    public long DistanceToOtherGalaxy(Galaxy otherGalaxy)
    {
        return Math.Abs(RowNumber - otherGalaxy.RowNumber) + Math.Abs(ColumnNumber - otherGalaxy.ColumnNumber);
    }
}