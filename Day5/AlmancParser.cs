using System.Text.RegularExpressions;

public class AlmanacParser
{
    private readonly string _rawAlmanac;
    public AlmanacParser(string rawAlmanac)
    {
        _rawAlmanac = rawAlmanac;
    }

    public Almanac ParseAlmanc()
    {
        var seeds = ParseSeeds();
        var seedRanges = ParseSeedRanges(seeds);
        return new Almanac
        {
            Seeds = seeds,
            SeedRanges = seedRanges,
            SeedToSoilMapper = ParseCategoryMapper("seed-to-soil"),
            SoilToFertilizerMapper = ParseCategoryMapper("soil-to-fertilizer"),
            FertilizerToWaterMapper = ParseCategoryMapper("fertilizer-to-water"),
            WaterToLightMapper = ParseCategoryMapper("water-to-light"),
            LightToTemperatureMapper = ParseCategoryMapper("light-to-temperature"),
            TemperatureToHumidityMapper = ParseCategoryMapper("temperature-to-humidity"),
            HumidityToLocationMapper = ParseCategoryMapper("humidity-to-location")
        };
    }

    private long[] ParseSeeds()
    {
        var seedsRegex = new Regex(@"seeds: (\d+\s)+");
        var match = seedsRegex.Match(_rawAlmanac);
        var seeds = new List<long>();
        foreach(Capture capture in match.Groups[1].Captures)
        {
            var seed = long.Parse(capture.Value.Trim());
            seeds.Add(seed);
        }
        return seeds.ToArray();
    }

    private List<SeedRange> ParseSeedRanges(long[] seeds)
    {
        var seedRanges = new List<SeedRange>();
        for(int i = 0; i < seeds.Length; i++)
        {
            var seedRange = new SeedRange
            {
                Start = seeds[i],
                Length = seeds[i+1]
            };
            seedRanges.Add(seedRange);
            i++;
        }

        return seedRanges;
    }

    private CategoryMapper ParseCategoryMapper(string mapperName)
    {
        var mapperRegex = new Regex($"{mapperName}\\s+map:\\r\\n([\\d\\s]+)");
        var match = mapperRegex.Match(_rawAlmanac);
        var rawCategoryMappings = match.Groups[1].Value;
        var rawMappingsSplit = rawCategoryMappings.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        var categoryMapper = new CategoryMapper() {Name = mapperName};
        foreach(var rawCategoryMap in rawMappingsSplit)
        {
            var categoryMap = ParseCategoryMap(rawCategoryMap);
            categoryMapper.AddCategoryMap(categoryMap);
        }
        return categoryMapper;
    }

    private CategoryMap ParseCategoryMap(string rawCategoryMap)
    {
        var numberRegex = new Regex("\\d+");
        var numberMatches = numberRegex.Matches(rawCategoryMap);
        return new CategoryMap 
        {
            DestinationRangeStart = long.Parse(numberMatches[0].Value),
            SourceRangeStart = long.Parse(numberMatches[1].Value),
            RangeLength = long.Parse(numberMatches[2].Value)
        };
    }
}