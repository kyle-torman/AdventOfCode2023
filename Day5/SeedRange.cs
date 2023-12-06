public record SeedRange 
{
    public required long Start {get; init;}
    public required long Length {get; init;}
    public long End => Start + Length;

    public Range ToRange() => new Range(Start, End);

    public void Print()
    {
        Console.WriteLine($"Start: {Start} - Length: {Length}");
    }
}