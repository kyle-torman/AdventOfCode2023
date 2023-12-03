public static class Extensions 
{
    public static IEnumerable<string> NormalizeNumberStrings(this IEnumerable<string> lineItemValues) =>
        lineItemValues.Select(item => 
            item switch 
            {
                "one" => "1",
                "two" => "2",
                "three" => "3",
                "four" => "4",
                "five" => "5",
                "six" => "6",
                "seven" => "7",
                "eight" => "8",
                "nine" => "9",
                _ => item
            });

}