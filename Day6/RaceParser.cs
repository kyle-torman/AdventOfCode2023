using System.Text.RegularExpressions;

public class RaceParser
{
    private readonly string _rawRaces;
    public RaceParser(string rawRaces)
    {
        _rawRaces = rawRaces;
    }

    public Race ParseSingleRace()
    {
        var time = ParseSingleNumber("Time");
        var distance = ParseSingleNumber("Distance");
        return new Race 
        {
            Time = time,
            Distance = distance
        };
    }

    public List<Race> ParseRaces()
    {
        var times = ParseNumbers("Time");
        var distances = ParseNumbers("Distance");
        var races = new List<Race>();
        for(int i = 0; i < times.Count; i++)
        {
            var race = new Race 
            {
                Time = times[i],
                Distance = distances[i]
            };
            races.Add(race);
        }
        return races;
    }

    private List<long> ParseNumbers(string nameOfNumbers)
    {
        var regex = new Regex($"{nameOfNumbers}:\\s+(\\d+\\s*)+");
        var match = regex.Match(_rawRaces);
        return match.Groups[1].Captures.Select(capture => long.Parse(capture.Value.Trim())).ToList();
    }

    private long ParseSingleNumber(string nameOfNumber)
    {
        var regex = new Regex($"{nameOfNumber}:\\s+(\\d+\\s*)+");
        var match = regex.Match(_rawRaces);
        var combinedNumbers = string.Join("", match.Groups[1].Captures.Select(capture => capture.Value.Trim()));
        return long.Parse(combinedNumbers);
    }
}