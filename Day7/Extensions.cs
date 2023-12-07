public static class Extensions
{
    public static Dictionary<Card, int> CountCards(this Card[] cards)
    {
        var cardCounts = new Dictionary<Card, int>();
        foreach(var card in cards)
        {
            if(cardCounts.ContainsKey(card))
            {
                cardCounts[card] += 1;
            }
            else
            {
                cardCounts.Add(card, 1);
            }
        }

        return cardCounts;
    }

    public static bool ContainsFiveOfAKind(this Dictionary<Card, int> cardCounts) =>
        cardCounts.Count == 1;

    public static bool ContainsFourOfAKind(this Dictionary<Card, int> cardCounts) => 
        cardCounts.Count == 2 && cardCounts.ContainsValue(4);

    public static bool ContainsFullHouse(this Dictionary<Card, int> cardCounts) => 
        cardCounts.Count == 2 && cardCounts.ContainsValue(2) && cardCounts.ContainsValue(3);

    public static bool ContainsThreeOfAKind(this Dictionary<Card, int> cardCounts) =>
        cardCounts.Count == 3 && cardCounts.ContainsValue(3);

    public static bool ContainsTwoPair(this Dictionary<Card, int> cardCounts) =>
        cardCounts.Count == 3 && cardCounts.Count(count => count.Value == 2) == 2;

    public static bool ContainsOnePair(this Dictionary<Card, int> cardCounts) =>
        cardCounts.Count == 4 && cardCounts.ContainsValue(2);

    public static bool ContainsHighCard(this Dictionary<Card, int> cardCounts) =>
        cardCounts.Count == 5;
}