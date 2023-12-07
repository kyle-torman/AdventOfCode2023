public class Hand : IComparable<Hand>
{
    public Card[] Cards {get;}
    public HandType HandType {get; private set;}
    public int Bet {get;}

    public Hand(Card[] cards, int bet)
    {
        Cards = cards;
        Bet = bet;
        SetHandType();
    }

    public void Print()
    {
        Console.WriteLine($"Cards: {string.Join(",", Cards)} - Bet: {Bet} - Type: {HandType}");
    }

    public int CompareTo(Hand? other)
    {
        if(other is null)
        {
            return -1;
        }

        var handTypeComparison = HandType.CompareTo(other.HandType);
        if(handTypeComparison != 0)
        {
            return handTypeComparison;
        }

        for(int i = 0; i < Cards.Length; i++)
        {
            var cardComparison = Cards[i].CompareTo(other.Cards[i]);
            if(cardComparison != 0)
            {
                return cardComparison;
            }
        }

        return 0;
    }

    private void SetHandType()
    {
        //SetHandTypeWithoutWilds();
        SetHandTypeWithWilds();
    }   

    private void SetHandTypeWithWilds()
    {
        var cardCounts = Cards.CountCards();
        var wildCount = cardCounts.Where(count => count.Key == Card.Jack).FirstOrDefault().Value;
        if(wildCount == 4)
        {
            HandType = HandType.FiveOfAKind;
        }
        else if(wildCount == 3)
        {
            if(cardCounts.Count == 2)
            {
                HandType = HandType.FiveOfAKind;
            }
            else 
            {
                HandType = HandType.FourOfAKind;
            }
        }
        else if(wildCount == 2)
        {
            if(cardCounts.Count == 2)
            {
                HandType = HandType.FiveOfAKind;
            }
            else if(cardCounts.Count == 3)
            {
                HandType = HandType.FourOfAKind;
            }
            else
            {
                HandType = HandType.ThreeOfAKind;
            }
        }
        else if(wildCount == 1)
        {
            if(cardCounts.Count == 2)
            {
                HandType = HandType.FiveOfAKind;
            }
            else if(cardCounts.Count == 3)
            {
                if(cardCounts.ContainsValue(3))
                {
                    HandType = HandType.FourOfAKind;
                }
                else 
                {
                    HandType = HandType.FullHouse;
                }
            }
            else if(cardCounts.Count == 4)
            {
                HandType = HandType.ThreeOfAKind;
            }
            else
            {
                HandType = HandType.OnePair;
            }
        }
        else
        {
            SetHandTypeWithoutWilds();
        }
    }

    private void SetHandTypeWithoutWilds()
    {
        var cardCounts = Cards.CountCards();
        if(cardCounts.ContainsFiveOfAKind())
        {
            HandType = HandType.FiveOfAKind;
        }
        else if(cardCounts.ContainsFourOfAKind())
        {
            HandType = HandType.FourOfAKind;
        }
        else if(cardCounts.ContainsFullHouse())
        {
            HandType = HandType.FullHouse;
        }
        else if(cardCounts.ContainsThreeOfAKind())
        {
            HandType = HandType.ThreeOfAKind;
        }
        else if(cardCounts.ContainsTwoPair())
        {
            HandType = HandType.TwoPair;
        }
        else if(cardCounts.ContainsOnePair())
        {
            HandType = HandType.OnePair;
        }
        else if(cardCounts.ContainsHighCard())
        {
            HandType = HandType.HighCard;
        }
        else
        {
            throw new Exception("Failed to set HandType");
        }
    } 
}



public enum HandType
{
    FiveOfAKind,
    FourOfAKind,
    FullHouse,
    ThreeOfAKind,
    TwoPair,
    OnePair,
    HighCard
}

public enum Card
{
    Ace,
    King,
    Queen,
    //Not Wild
    //Jack,
    Ten,
    Nine,
    Eight,
    Seven,
    Six,
    Five,
    Four,
    Three,
    Two,
    One,
    //Wild
    Jack  
}