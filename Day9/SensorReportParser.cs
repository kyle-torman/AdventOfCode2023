using System.Text.RegularExpressions;

public class SensorReportParser 
{
    private readonly string _rawReport;

    public SensorReportParser(string rawReport)
    {
        _rawReport = rawReport;
    }

    public SensorReport ParseSensorReport()
    {
        var rawHistoryItems = _rawReport.Split("\r\n");   
        var historyItems = rawHistoryItems.Select(ParseNumbers)
                                          .Select(numbers => new ReportHistory(numbers.ToList()))
                                          .ToList();

        return new SensorReport(historyItems);   
    }

    private IEnumerable<long> ParseNumbers(string numbers)
    {
        var numberRegex = new Regex("[\\-0-9]+");
        var matches = numberRegex.Matches(numbers);
        foreach(Match match in matches)
        {
            yield return long.Parse(match.Value);
        }
    }
}