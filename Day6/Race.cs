public record Race
{
    public required long Time {get; init;}
    public required long Distance {get; init;}

    public void Print()
    {
        Console.WriteLine($"Time: {Time} - Distance: {Distance}");
    }

    public long NumberOfWinningScenarios => CalculateWinningScenarios().Count();

    public IEnumerable<long> CalculateWinningScenarios()
    {
        for(long timeHeld = 1; timeHeld < Time; timeHeld++)
        {
            var finalDistance = timeHeld * (Time - timeHeld);
            if(finalDistance > Distance)
            {
                yield return timeHeld;
            }
        }
    }
}