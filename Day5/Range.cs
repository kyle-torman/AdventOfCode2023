public record Range(long Start, long End)
{
    public Range Shift(long distance) => new Range(Start + distance, End + distance);
}