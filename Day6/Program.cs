// See https://aka.ms/new-console-template for more information
var raceParser = new RaceParser(RacesFiles.PuzzleInput);
// var races = raceParser.ParseRaces();
// Console.WriteLine($"Winning Scenarios Multiplied: {races.Select(race => race.NumberOfWinningScenarios).Aggregate((x,y) => x * y)}");

var combinedRace = raceParser.ParseSingleRace();
//combinedRace.Print();
Console.WriteLine($"Winning Scenarios Single: {combinedRace.NumberOfWinningScenarios}");
// foreach(var race in races)
// {
//     var winningScenarios = race.CalculateWinningScenarios();
//     Console.WriteLine($"Race ({race.Time} - Winning Scenarios: {string.Join(",", winningScenarios)})");
// }
