public static class Extensions
{
    public static PipeType ToPipeType(this char rawPipeType) =>
        rawPipeType switch
        {
            '|' => PipeType.NorthAndSouth,
            '-' => PipeType.EastAndWest,
            'L' => PipeType.SouthWest,
            'J' => PipeType.SouthEast,
            '7' => PipeType.NorthEast,
            'F' => PipeType.NorthWest,
            'S' => PipeType.StartingPosition,         
            _ => throw new NotImplementedException()
        };
}