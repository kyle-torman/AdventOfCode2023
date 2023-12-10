public class ReportHistory
{
    public List<long> Values {get;}

    public ReportHistory(List<long> values)
    {
        Values = values;        
    }

    public void Print()
    {
        Console.WriteLine(string.Join(",", Values));
    }

    public long ExtrapolateNextValue(bool isBackwards)
    {        
        var sequenceValues = new List<long>
        {
            isBackwards ? Values.First() : Values.Last()
        };
        
        var lastSequence = Values.GetDifferences().ToList();
        var sequenceValue = isBackwards ? lastSequence.First() : lastSequence.Last();
        sequenceValues.Add(sequenceValue);

        while(!lastSequence.All(v => v == 0))
        {
            lastSequence = lastSequence.GetDifferences().ToList();
            sequenceValue = isBackwards ? lastSequence.FirstOrDefault() : lastSequence.LastOrDefault();
            sequenceValues.Add(sequenceValue);
        }
        
        sequenceValues.Reverse();
        var extrapolatedValue = isBackwards ?
                                sequenceValues.Aggregate((x , y) => y - x) :
                                sequenceValues.Aggregate((x, y) => x + y);

        return extrapolatedValue;
    }
}