using System.Text.RegularExpressions;

public static class Extensions 
{
    public static bool IsNumeric(this Match match)
    {
        var isSuccessful = int.TryParse(match.Value, out var _);
        return isSuccessful;
    }
}