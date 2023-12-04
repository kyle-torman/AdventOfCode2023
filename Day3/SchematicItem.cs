public abstract class SchematicItem
{
    public required int RowNumber {get; init;}
    public required int ColumnNumber {get; init;}
    public (int Row, int Column)[] GetAdjacentCoordinates()
    {
        return new (int, int)[]
        {
            (RowNumber - 1, ColumnNumber - 1),
            (RowNumber - 1, ColumnNumber),
            (RowNumber - 1, ColumnNumber + 1),
            (RowNumber, ColumnNumber - 1),
            (RowNumber, ColumnNumber + 1),
            (RowNumber + 1, ColumnNumber - 1),
            (RowNumber + 1, ColumnNumber),
            (RowNumber + 1, ColumnNumber + 1),
        };
    }
}

public class SchematicSymbol : SchematicItem
{
    public string Value {get; init;} = string.Empty;
    public override string ToString()
    {
        return Value;
    }
}

public class SchematicNumber : SchematicItem, IEquatable<SchematicNumber>
{
    public int Value {get; init;}
    public int ColumnSpan {get; init;}
    public int MaxColumnNumber => ColumnNumber + ColumnSpan;
    public override string ToString()
    {
        return "N";
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SchematicNumber);
    }

    public bool Equals(SchematicNumber? other)
    {
        return other is not null &&
               Value == other.Value &&
               RowNumber == other.RowNumber &&
               ColumnNumber == other.ColumnNumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, RowNumber, ColumnNumber);
    }
}