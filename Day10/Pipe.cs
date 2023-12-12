public class Pipe 
{
    public required int ColumnNumber {get; init;}
    public required int RowNumber {get; init;}
    public PipeType Type {get; }  
    private readonly char _rawType;

    public Pipe(char rawType)
    {
        _rawType = rawType;
        Type = rawType.ToPipeType();        
    }      

    public (int Row, int Column)[] GetAdjacentCoordinates()
    {
        return new (int, int)[]
        {
            (RowNumber - 1, ColumnNumber),
            (RowNumber, ColumnNumber - 1),
            (RowNumber, ColumnNumber + 1),
            (RowNumber + 1, ColumnNumber),
        };
    }   

    public bool IsConnectingPipe(Pipe? otherPipe)
    {
        if(otherPipe is null)
        {
            return false;
        }

        if(otherPipe.ColumnNumber < ColumnNumber)
        {
            return CanConnectWest(otherPipe);
        }
        else if(otherPipe.ColumnNumber > ColumnNumber)
        {
            return CanConnectEast(otherPipe);
        }
        else if(otherPipe.RowNumber < RowNumber)
        {
            return CanConnectNorth(otherPipe);
        }
        else if(otherPipe.RowNumber > RowNumber)
        {
            return CanConnectSouth(otherPipe);
        }

        throw new NotImplementedException();
    }

    private bool CanConnectNorth(Pipe otherPipe)
    {
        var thisPipeCanConnectAbove = Type == PipeType.NorthAndSouth ||
                                      Type == PipeType.SouthWest ||
                                      Type == PipeType.SouthEast ||
                                      Type == PipeType.StartingPosition;

        var otherPipeCanConnectBelow = otherPipe.Type == PipeType.NorthAndSouth ||
                                       otherPipe.Type == PipeType.NorthEast ||
                                       otherPipe.Type == PipeType.NorthWest ||
                                       otherPipe.Type == PipeType.StartingPosition;

        return thisPipeCanConnectAbove && otherPipeCanConnectBelow;                                  
    }

    private bool CanConnectSouth(Pipe otherPipe)
    {
        var thisPipeCanConnectBelow = Type == PipeType.NorthAndSouth ||
                                      Type == PipeType.NorthEast ||
                                      Type == PipeType.NorthWest ||
                                      Type == PipeType.StartingPosition;

        var otherPipeCanConnectAbove = otherPipe.Type == PipeType.NorthAndSouth ||
                                       otherPipe.Type == PipeType.SouthWest ||
                                       otherPipe.Type == PipeType.SouthEast ||
                                       otherPipe.Type == PipeType.StartingPosition;

        return thisPipeCanConnectBelow && otherPipeCanConnectAbove; 
    }

    private bool CanConnectEast(Pipe otherPipe)
    {
        var thisPipeCanConnectEast = Type == PipeType.EastAndWest ||
                                     Type == PipeType.NorthWest ||
                                     Type == PipeType.SouthWest ||
                                     Type == PipeType.StartingPosition;

        var otherPipeCanConnectWest = otherPipe.Type == PipeType.EastAndWest ||
                                      otherPipe.Type == PipeType.NorthEast ||
                                      otherPipe.Type == PipeType.SouthEast ||
                                      otherPipe.Type == PipeType.StartingPosition;

        return thisPipeCanConnectEast && otherPipeCanConnectWest;
    }

    private bool CanConnectWest(Pipe otherPipe)
    {
        var thisPipeCanConnectWest = Type == PipeType.EastAndWest ||
                                      Type == PipeType.NorthEast ||
                                      Type == PipeType.SouthEast ||
                                      Type == PipeType.StartingPosition;

        var otherPipeCanConnectEast = otherPipe.Type == PipeType.EastAndWest ||
                                     otherPipe.Type == PipeType.NorthWest ||
                                     otherPipe.Type == PipeType.SouthWest ||
                                     otherPipe.Type == PipeType.StartingPosition;

        return thisPipeCanConnectWest && otherPipeCanConnectEast;
    }

    public override string ToString() => $"{_rawType}";
}

public enum PipeType
{
    NorthAndSouth,
    EastAndWest,
    NorthEast,
    NorthWest,
    SouthEast,
    SouthWest,
    StartingPosition
}