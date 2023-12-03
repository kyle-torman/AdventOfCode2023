public record Game 
{
    public int Id {get; init;}
    public List<CubeSet> CubeSetsPulled {get; init;} = new List<CubeSet>();

    public bool IsPossibleForBag(GameBag gameBag)
    {
        return !CubeSetsPulled.Any(set => set.Blue > gameBag.Blue || set.Red > gameBag.Red || set.Green > gameBag.Green);
    }

    public int MinimumCubeSetPower  
    {
        get 
        {
            var maxRed = CubeSetsPulled.Max(set => set.Red);
            var maxBlue = CubeSetsPulled.Max(set => set.Blue);
            var maxGreen = CubeSetsPulled.Max(set => set.Green);

            return maxRed * maxBlue * maxGreen;
        }
    }
}