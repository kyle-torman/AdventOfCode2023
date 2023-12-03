using System.Text.RegularExpressions;

public class GameParser
{
    private readonly GameData _gameData;
    public GameParser(GameData gameData)
    {
        _gameData = gameData;
    }

    public List<Game> ParseGamesPlayed() 
    {
        var gameLineItems = _gameData.GamesPlayed.ToLower().Split("\r\n");
        var gamesPlayed = new List<Game>();
        foreach(var gameLineItem in gameLineItems)
        {
            var game = ParseGameFromLineItem(gameLineItem);
            gamesPlayed.Add(game);
        }
        return gamesPlayed;
    }

    private Game ParseGameFromLineItem(string gameLineItem)
    {
        var gameDataSplit = gameLineItem.Split(":");
        var gameNumber = ParseGameId(gameDataSplit[0]);
        var cubeSets = ParseCubeSetsFromLineItem(gameDataSplit[1]);
        return new Game { Id = gameNumber, CubeSetsPulled = cubeSets };
    }

    private int ParseGameId(string gameNumberRaw)
    {
        var gameNumberRegex = new Regex("game ([0-9]+)");
        var match = gameNumberRegex.Match(gameNumberRaw);
        var gameNumber = int.Parse(match.Groups[1].Value);
        return gameNumber;
    }

    private List<CubeSet> ParseCubeSetsFromLineItem(string cubeSetData)
    {
        var cubeSetsRaw = cubeSetData.Split(";");
        var cubeSets = new List<CubeSet>();        
        foreach(var cubeSetRaw in cubeSetsRaw)
        {
            var cubeSet = ParseCubeSetFromRawData(cubeSetRaw);
            cubeSets.Add(cubeSet);
        }
        return cubeSets;
    }

    private CubeSet ParseCubeSetFromRawData(string cubeSetRaw)
    {
        var cubeSetParserRegex = new Regex("([0-9]+ red)|([0-9]+ blue)|([0-9]+ green)");
        var matches = cubeSetParserRegex.Matches(cubeSetRaw);
        var colors = new Dictionary<string,int>();
        foreach(Match match in matches)
        {
            var colorSplit = match.Value.Split(" ");
            var number = int.Parse(colorSplit[0]);
            var color = colorSplit[1];    
            colors.Add(color, number);            
        }
        var cubeSet = new CubeSet 
        {
            Red = colors.ContainsKey("red") ? colors["red"] : 0,
            Blue = colors.ContainsKey("blue") ? colors["blue"] : 0,
            Green = colors.ContainsKey("green") ? colors["green"] : 0
        };
        return cubeSet;
    }
}