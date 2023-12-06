public record CategoryMap 
{
    public required long DestinationRangeStart {get; init;}
    public long DestinationRangeEnd => DestinationRangeStart + RangeLength;
    public required long SourceRangeStart {get; init;}
    public long SourceRangeEnd => SourceRangeStart + RangeLength;
    public required long RangeLength {get; init;}

    public long DiffBetweenSourceAndDestination => DestinationRangeStart - SourceRangeStart;

    public long? MapSourceCategoryToDestinationCategory(long source)
    {
        if(!IsWithinSourceRange(source))
        {
            return null;
        }
        else
        {
            var diffBetweenSourceAndStart = source - SourceRangeStart;
            return DestinationRangeStart + diffBetweenSourceAndStart;
        }
    }

    public List<(Range SourceRange, Range? DestinationRange)> MapSourceCategoryRangeToDestinationCategoryRange(Range sourceRange)
    {
        if(!IsRangeWithinMap(sourceRange))
        {
            return new List<(Range sourceRange, Range? DestinationCategory)> {(sourceRange, null)};
        }
        else 
        {
            return GetMappedRanges(sourceRange);                        
        }
    }

    public List<(Range, Range?)> GetMappedRanges(Range sourceRange)
    {
        var overlapStart = Math.Max(sourceRange.Start, SourceRangeStart);
        var overlapEnd = Math.Min(sourceRange.End, SourceRangeEnd - 1);
        var mappedRange = new Range(overlapStart, overlapEnd);
        var mappedRanges = new List<(Range, Range?)> { (mappedRange, mappedRange.Shift(DiffBetweenSourceAndDestination)) };

        if(overlapStart > sourceRange.Start)
        {
            mappedRanges.Add((new Range(sourceRange.Start, overlapStart - 1), null));
        }

        if(overlapEnd < sourceRange.End)
        {
            mappedRanges.Add((new Range(overlapEnd + 1, sourceRange.End), null));
        }

        return mappedRanges;
    }

    private bool IsRangeWithinMap(Range range) => range.Start < SourceRangeEnd &&
                                                  range.End >= SourceRangeStart; 

    private bool IsWithinSourceRange(long source) => SourceRangeStart <= source &&
                                                     SourceRangeEnd > source;
}