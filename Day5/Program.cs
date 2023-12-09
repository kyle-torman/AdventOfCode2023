// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var almanacParser = new AlmanacParser(AlmanacFiles.PuzzleInput);
var almanac = almanacParser.ParseAlmanc();
var stopWatch = new Stopwatch();
stopWatch.Start();
var seedRangeMappings = almanac.GetSeedRangeToLocationRangeMappings();
stopWatch.Stop();
Console.WriteLine($"Lowest Range Location: {seedRangeMappings.Min(r => r.Value.Min(r2 => r2.Start))} - Duration: {stopWatch.ElapsedMilliseconds} ms");
stopWatch.Reset();
stopWatch.Start();
var seedToLocationMappings = almanac.GetSeedToLocationMappings();
stopWatch.Stop();
Console.WriteLine($"Lowest Location: {seedToLocationMappings.Min(map => map.Value)} - Duration: {stopWatch.ElapsedMilliseconds} ms");
