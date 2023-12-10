public static class Extensions
{
    public static IEnumerable<long> GetDifferences(this List<long> source)
    {
        for(int i = 1; i < source.Count; i++)
        {
            yield return  source[i] - source[i - 1];
        }
    }
}