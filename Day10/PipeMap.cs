public class PipeMap 
{
    public Pipe[,] Grid {get;}
    private int GridRowCount => Grid.GetLength(0);
    private int GridColumnCount => Grid.GetLength(1);
    private Pipe _startingPosition;

    public PipeMap(List<Pipe> pipes)
    {        
        //Need to add one to these because the row and column in the items is 0 based.
        var numberOfRows = pipes.Max(pipe => pipe.RowNumber) + 1;
        var numberOfColumns = pipes.Max(pipe => pipe.ColumnNumber) + 1;

        Grid = new Pipe[numberOfRows, numberOfColumns];
        AddPipesToGrid(pipes);
    }

    public List<Pipe> GetPipeLoop()
    {
        var pipeLoop = new List<Pipe>();
        pipeLoop.Add(_startingPosition);
        var currentPipe = _startingPosition;
        var previousPipe = _startingPosition;
        var complete = false;
        while(!complete)
        {            
            var connectingPipe = GetConnectingPipes(currentPipe).First(p => p != previousPipe);
            if(pipeLoop.Contains(connectingPipe))
            {
                complete = true;
            }
            else 
            {
                pipeLoop.Add(connectingPipe);
                previousPipe = currentPipe;
                currentPipe = connectingPipe;                
            }
        }
        return pipeLoop;
    }

    private IEnumerable<Pipe> GetConnectingPipes(Pipe currentPipe)
    {
        var adjacentCoordinates = currentPipe.GetAdjacentCoordinates()
                                             .Where(c => c.Row < GridRowCount && c.Column < GridColumnCount && c.Row >= 0 && c.Column >= 0);
                                             
        foreach(var coordinate in adjacentCoordinates)
        {
            var adjacentPipe = Grid[coordinate.Row, coordinate.Column];
            if(currentPipe.IsConnectingPipe(adjacentPipe))
            {
                yield return adjacentPipe;
            }
        }
    }

    private void AddPipesToGrid(List<Pipe> pipes)
    {
        foreach(var pipe in pipes)
        {
            if(pipe.Type == PipeType.StartingPosition)
            {
                _startingPosition = pipe;
            }

            Grid[pipe.RowNumber, pipe.ColumnNumber] = pipe;
        }
    }

    public void Print()
    {
        for (int row = 0; row < GridRowCount; row++)
        {
            for (int column = 0; column < GridColumnCount; column++)
            {
                var pipe = Grid[row, column];
                Console.Write(pipe is null ? "." : pipe.ToString());
            }
            Console.WriteLine();
        }
    }
}