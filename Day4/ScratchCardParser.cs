using System.Text.RegularExpressions;

public class ScratchCardParser
{
    private readonly string _rawScratchCards;

    public ScratchCardParser(string rawScratchCards)
    {
        _rawScratchCards = rawScratchCards;
    }

    public List<ScratchCard> ParseScratchCards()
    {
        var rawCardsSplit = _rawScratchCards.Split("\r\n");
        var scratchCards = new List<ScratchCard>();
        foreach(var rawCard in rawCardsSplit)
        {
            var card = ParseScratchCard(rawCard);
            scratchCards.Add(card);
        }

        return scratchCards;
    }

    private ScratchCard ParseScratchCard(string rawScratchCard)
    {
        var rawSplit = rawScratchCard.Split(":");
        var cardId = ParseCardId(rawSplit[0]);
        var numbers = ParseCardNumbers(rawSplit[1]);
        return new ScratchCard(cardId, numbers.WinningNumbers, numbers.PlayerNumbers);
    }

    private int ParseCardId(string rawCardId)
    {
        var cardIdRegex = new Regex("Card\\s+(\\d+)");
        var match = cardIdRegex.Match(rawCardId);
        return int.Parse(match.Groups[1].Value);
    }

    private (HashSet<int> WinningNumbers, HashSet<int> PlayerNumbers) ParseCardNumbers(string rawCardNumbers)
    {
        var rawNumbersSplit = rawCardNumbers.Split("|");
        var winningNumbers = ParseIndivitualNumberSet(rawNumbersSplit[0]);
        var playerNumbers = ParseIndivitualNumberSet(rawNumbersSplit[1]);
        return (winningNumbers, playerNumbers);
    }

    private HashSet<int> ParseIndivitualNumberSet(string rawNumberSet)
    {
        var numberRegex = new Regex("\\d+");
        var numberMatches = numberRegex.Matches(rawNumberSet);
        return numberMatches.Select(match => int.Parse(match.Value)).ToHashSet();
    }
}