using System.Text.RegularExpressions;

public interface ICalibrationValueParser
{
    List<int> GetCalibrationValues();
}

public class CalibrationValueParser : ICalibrationValueParser
{
    private readonly CalibrationDocument _calibrationDocument;
    public CalibrationValueParser(CalibrationDocument calibrationDocument)
    {
        _calibrationDocument = calibrationDocument;
    }
    
    public List<int> GetCalibrationValues()
    {
        var calibrationLineItems = GetCalibrationLineItems();
        var calibrationValues = ConvertLineItemsToActualValues(calibrationLineItems);
        return calibrationValues;
    }

    private List<string[]> GetCalibrationLineItems()
    {
        const string baseRegex = @"(?<digits>one|two|three|four|five|six|seven|eight|nine|1|2|3|4|5|6|7|8|9|0)";
        var calibrationValueParserRegexLeft = new Regex(baseRegex);
        var calibrationValueParserRegexRight = new Regex(baseRegex, RegexOptions.RightToLeft);
        //var calibrationValueParserRegex = new Regex(@"(?<digits>1|2|3|4|5|6|7|8|9|0)");
        var unparsedCalibrationValues = _calibrationDocument.Value.ToLower().Split("\r\n");
        var calibrationLineItems = new List<string[]>();
        foreach(var calibrationValue in unparsedCalibrationValues)
        {
            var leftMostMatches = calibrationValueParserRegexLeft.Matches(calibrationValue);
            var rightMostMatches = calibrationValueParserRegexRight.Matches(calibrationValue);
            var callibrationLineItemValuesLeft = leftMostMatches.Select(m => m.Value).NormalizeNumberStrings().ToArray();
            var callibrationLineItemValuesRight = rightMostMatches.Select(m => m.Value).NormalizeNumberStrings().ToArray();
            //This will catch overlaps such as oneight. When going left to right you will get one first, but going right to left will get eight first. 
            if(callibrationLineItemValuesLeft.Last() != callibrationLineItemValuesRight.First())
            {
                Console.WriteLine($"Setting last value for overlap to {callibrationLineItemValuesRight.First()} from {callibrationLineItemValuesLeft.Last()} - {calibrationValue} ");
                callibrationLineItemValuesLeft[callibrationLineItemValuesLeft.Length - 1] = callibrationLineItemValuesRight.First();
            }
            calibrationLineItems.Add(callibrationLineItemValuesLeft);
        }
        return calibrationLineItems;
    }

    private List<int> ConvertLineItemsToActualValues(List<string[]> calibrationLineItems)
    {
        var calibrationValues = new List<int>();
        foreach(var calibrationLineItem in calibrationLineItems)
        {
            var firstDigit = calibrationLineItem.First();
            var lastDigit = calibrationLineItem.Last();
            var combinedValue = $"{firstDigit}{lastDigit}";
            calibrationValues.Add(int.Parse(combinedValue));
        }
        return calibrationValues;
    }
    


    // private int[] ConvertLineItemValues(string[] lineItemValues) =>
    //     lineItemValues.Select(item => 
    //         item switch 
    //         {
    //             "one" or "1" => 1,
    //             "two" or "2" => 2,
    //             "three" or "3" => 3,
    //             "four" or "4" => 4,
    //             "five" or "5" => 5,
    //             "six" or "6" => 6,
    //             "seven" or "7" => 7,
    //             "eight" or "8" => 8,
    //             "nine" or "9" => 9,
    //             _ => throw new NotImplementedException()
    //         }).ToArray();
}