public class ScratchCard 
{
    public int Id {get; }
    public HashSet<int> WinningNumbers {get; }
    public HashSet<int> PlayerNumbers {get; }

    public int Copies {get; private set;} = 1;

    public HashSet<int> WinningPlayerNumbers {get;}

    public ScratchCard(int id, HashSet<int> winningNumbers, HashSet<int> playerNumbers)
    {
        Id = id;
        WinningNumbers = winningNumbers;
        PlayerNumbers = playerNumbers;

        WinningPlayerNumbers = WinningNumbers.Intersect(PlayerNumbers).ToHashSet();
    }

    public void AddCopies(int numberOfCopies)
    {
        Copies += numberOfCopies;
    }

    public int Points => WinningPlayerNumbers.Count == 0 ? 0 : (int) Math.Pow(2, WinningPlayerNumbers.Count - 1);

    public override string ToString()
    {
        return $"Card: {Id} - Winning: {string.Join(",", WinningNumbers)} - Player: {string.Join(",", PlayerNumbers)} - PlayerWinning {string.Join(",", WinningPlayerNumbers)} - Points: {Points} - Copies {Copies}";   
    }
}