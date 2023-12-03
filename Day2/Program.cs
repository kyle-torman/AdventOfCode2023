// See https://aka.ms/new-console-template for more information
var gameParser = new GameParser(new GameData(GameFiles.PuzzleInput));
var gamesPlayed = gameParser.ParseGamesPlayed();

var bagToCheck = new GameBag
{
    Red = 12,
    Green = 13,
    Blue = 14
};

var possibleGames = gamesPlayed.Where(game => game.IsPossibleForBag(bagToCheck));
Console.WriteLine($"Possible Games: {string.Join(",", possibleGames.Select(game => game.Id))}");
Console.WriteLine($"Sum of possible games: {possibleGames.Sum(game => game.Id)}");
Console.WriteLine($"Sum of minimum power: {gamesPlayed.Sum(game => game.MinimumCubeSetPower)}");
gamesPlayed.ForEach(game => 
{
    Console.WriteLine($"Game {game.Id} - {game.MinimumCubeSetPower}");
});