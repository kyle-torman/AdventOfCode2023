// See https://aka.ms/new-console-template for more information
var cardParser = new ScratchCardParser(ScratchCardFiles.PuzzleInput);
var scratchCards = cardParser.ParseScratchCards();
for(int i = 0; i < scratchCards.Count; i++)
{
    var currentCard = scratchCards[i];
    var copiesToCreate = currentCard.WinningPlayerNumbers.Count;
    var firstCopyIndex = i + 1;    
    var lastCopyIndex = copiesToCreate + firstCopyIndex > scratchCards.Count ? scratchCards.Count : copiesToCreate + firstCopyIndex;
    for(int currentCopyIndex = firstCopyIndex; currentCopyIndex < lastCopyIndex; currentCopyIndex++)
    {
        scratchCards[currentCopyIndex].AddCopies(currentCard.Copies);
    }
}
Console.WriteLine($"Total Scratch Cards: {scratchCards.Sum(x => x.Copies)}");
Console.WriteLine($"Point Sum: {scratchCards.Sum(card => card.Points)}");
// scratchCards.ForEach(card => 
// {
//     Console.WriteLine(card.ToString());
// });
