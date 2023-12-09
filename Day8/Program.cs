// See https://aka.ms/new-console-template for more information
var parser = new MapParser(MapFiles.PuzzleInput);
var map = parser.ParseMap();
Console.WriteLine($"Steps Taken: {map.CalculateNumberOfStepsInMap()}");
Console.WriteLine($"Ghost Steps Taken: {map.CalculateNumberOfGhostStepsInMap()}");