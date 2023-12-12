public class PipeMapParser
{
    private readonly string _rawPipeMap;
    public PipeMapParser(string rawPipeMap)
    {
        _rawPipeMap = rawPipeMap;
    }

    public PipeMap ParsePipeMap()
    {
        var rawPipeRows = _rawPipeMap.Split("\r\n");

        var pipes = new List<Pipe>();
        for(int rowNumber = 0; rowNumber < rawPipeRows.Length; rowNumber++)
        {
            pipes.AddRange(ParsePipeRow(rawPipeRows[rowNumber], rowNumber).Where(pipe => pipe is not null));
        }

        return new PipeMap(pipes);
    }

    private IEnumerable<Pipe> ParsePipeRow(string rawPipeRow, int rowNumber)
    {
        for(var columnNumber = 0; columnNumber < rawPipeRow.Length; columnNumber++)
        {
            if(rawPipeRow[columnNumber] == '.')
            {
                continue;
            }

            yield return new Pipe(rawPipeRow[columnNumber])
            {
                RowNumber = rowNumber,
                ColumnNumber = columnNumber
            };
        }
    }
}