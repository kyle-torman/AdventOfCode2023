using System.Text.RegularExpressions;

public class HandParser
{
    private readonly string _rawHands;
    public HandParser(string rawHands)
    {
        _rawHands = rawHands;
    }

    public IEnumerable<Hand> ParseHands()
    {
        var rawHandsSplit = _rawHands.Split("\r\n");
        var handRegex = new Regex("([a-zA-Z0-9]+)\\s+(\\d+)");
        foreach(var rawHand in rawHandsSplit)
        {
            var match = handRegex.Match(rawHand);
            var cards = ParseCards(match.Groups[1].Value).ToArray();
            var bet = int.Parse(match.Groups[2].Value);
            yield return new Hand(cards, bet);
        }
    }

    private IEnumerable<Card> ParseCards(string rawCards)
    {
        foreach(var card in rawCards)
        {
            yield return card switch
            {
                'A' => Card.Ace,
                'K' => Card.King,
                'Q' => Card.Queen,
                'J' => Card.Jack,
                'T' => Card.Ten,
                '9' => Card.Nine,
                '8' => Card.Eight,
                '7' => Card.Seven,
                '6' => Card.Six,
                '5' => Card.Five,
                '4' => Card.Four,
                '3' => Card.Three,
                '2' => Card.Two,
                '1' => Card.One,
                _ => throw new NotImplementedException()
            };
        }
    }
}