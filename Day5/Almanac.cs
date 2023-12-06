public class Almanac
{
    public required long[] Seeds {get; init;}
    public required List<SeedRange> SeedRanges {get; init;}
    public required CategoryMapper SeedToSoilMapper {get; init;}
    public required CategoryMapper SoilToFertilizerMapper {get; init;}
    public required CategoryMapper FertilizerToWaterMapper {get; init;}
    public required CategoryMapper WaterToLightMapper {get; init;}
    public required CategoryMapper LightToTemperatureMapper {get; init;}
    public required CategoryMapper TemperatureToHumidityMapper {get; init;}
    public required CategoryMapper HumidityToLocationMapper {get; init;}

    public Dictionary<long,long> GetSeedToLocationMappings()
    {
        var seedToLocationMappings = new Dictionary<long,long>();
        foreach(var seed in Seeds)
        {
            var soilCategory = SeedToSoilMapper.MapSourceCategoryToDestinationCategory(seed);
            var fertilizerCategory = SoilToFertilizerMapper.MapSourceCategoryToDestinationCategory(soilCategory);
            var waterCategory = FertilizerToWaterMapper.MapSourceCategoryToDestinationCategory(fertilizerCategory);
            var lightCategory = WaterToLightMapper.MapSourceCategoryToDestinationCategory(waterCategory);
            var temperatureCategory = LightToTemperatureMapper.MapSourceCategoryToDestinationCategory(lightCategory);
            var humidityCategory = TemperatureToHumidityMapper.MapSourceCategoryToDestinationCategory(temperatureCategory);
            var locationCategory = HumidityToLocationMapper.MapSourceCategoryToDestinationCategory(humidityCategory);
            seedToLocationMappings.Add(seed, locationCategory);
        }
        return seedToLocationMappings;
    }

    public Dictionary<SeedRange, List<Range>> GetSeedRangeToLocationRangeMappings()
    {
        var seedRangeToLocationRangeMappings = new Dictionary<SeedRange, List<Range>>();
        foreach(var seedRange in SeedRanges)
        {
            var soilRangeCategories = SeedToSoilMapper.MapSourceCategoryRangeToDestinationCategoryRange(new List<Range>{seedRange.ToRange()});
            var fertilizerRangeCategories = SoilToFertilizerMapper.MapSourceCategoryRangeToDestinationCategoryRange(soilRangeCategories);
            var waterRangeCategories = FertilizerToWaterMapper.MapSourceCategoryRangeToDestinationCategoryRange(fertilizerRangeCategories);
            var lightRangeCategories = WaterToLightMapper.MapSourceCategoryRangeToDestinationCategoryRange(waterRangeCategories);
            var temperatureRangeCategories = LightToTemperatureMapper.MapSourceCategoryRangeToDestinationCategoryRange(lightRangeCategories);
            var humidityRangeCategories = TemperatureToHumidityMapper.MapSourceCategoryRangeToDestinationCategoryRange(temperatureRangeCategories);
            var locationRangeCategories = HumidityToLocationMapper.MapSourceCategoryRangeToDestinationCategoryRange(humidityRangeCategories);
            seedRangeToLocationRangeMappings.Add(seedRange, locationRangeCategories);
        }

        return seedRangeToLocationRangeMappings;
    }

    public void Print()
    {
        Console.WriteLine($"Seeds: {string.Join(",", Seeds)}");
        foreach(var seedRange in SeedRanges)
        {
            seedRange.Print();
        }
        SeedToSoilMapper.Print();
        SoilToFertilizerMapper.Print();
        FertilizerToWaterMapper.Print();
        WaterToLightMapper.Print();
        LightToTemperatureMapper.Print();
        TemperatureToHumidityMapper.Print();
        HumidityToLocationMapper.Print();
    }
}