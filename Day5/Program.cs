// See https://aka.ms/new-console-template for more information
var almanacParser = new AlmanacParser(AlmanacFiles.TestAlmanac);
var almanac = almanacParser.ParseAlmanc();
var seedRangeMappings = almanac.GetSeedRangeToLocationRangeMappings();
Console.WriteLine($"Lowest Range Location: {seedRangeMappings.Min(r => r.Value.Min(r2 => r2.Start))}");
var seedToLocationMappings = almanac.GetSeedToLocationMappings();
 Console.WriteLine($"Lowest Location: {seedToLocationMappings.Min(map => map.Value)}");
