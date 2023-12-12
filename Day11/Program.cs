// See https://aka.ms/new-console-template for more information
var parser = new GalaxyParser(GalaxyFiles.PuzzleInput);
//var galaxies = parser.ParseGalaxies();
var galaxies = parser.ParseGalaxiesWithExpansion(1000000);
var galaxyMapPathDistances = new Dictionary<string, long>();

foreach(var firstGalaxy in galaxies)
{
    var galaxiesToMap = galaxies.Where(g => g.Id != firstGalaxy.Id && !galaxyMapPathDistances.ContainsKey($"{g.Id} - {firstGalaxy.Id}"));
    foreach(var secondGalaxy in galaxiesToMap)
    {
        var pathDistance = firstGalaxy.DistanceToOtherGalaxy(secondGalaxy);
        galaxyMapPathDistances.Add($"{firstGalaxy.Id} - {secondGalaxy.Id}", pathDistance);
    }
}

Console.WriteLine($"Sum of Paths: {galaxyMapPathDistances.Sum(d => d.Value)}");

