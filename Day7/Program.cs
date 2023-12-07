// See https://aka.ms/new-console-template for more information
var handParser = new HandParser(HandFiles.PuzzleInput);
var hands = handParser.ParseHands().ToList();
hands.Sort();
var totalWinnings = 0;
for(int i = 0; i < hands.Count; i++)
{
    var betMultiplier = hands.Count - i;
    totalWinnings += betMultiplier * hands[i].Bet;
}
Console.WriteLine($"Winnings: {totalWinnings}");
