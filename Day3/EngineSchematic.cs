public class EngineSchematic 
{
    public SchematicItem[,] Grid {get; }
    private int GridRowCount => Grid.GetLength(0);
    private int GridColumnCount => Grid.GetLength(1);

    private readonly List<SchematicItem> _schematicItems;

    public EngineSchematic(List<SchematicItem> schematicItems)
    {
        _schematicItems = schematicItems;

        //Need to add one to these because the row and column in the items is 0 based.
        var numberOfRows = schematicItems.Max(item => item.RowNumber) + 1;
        var numberOfColumns = schematicItems.Max(item => item is SchematicNumber number ? number.MaxColumnNumber : item.ColumnNumber) + 1;

        Grid = new SchematicItem[numberOfRows, numberOfColumns];
        AddItemsToGrid(schematicItems);
    }  

    private void AddItemsToGrid(List<SchematicItem> schematicItems)
    {
        foreach(var item in schematicItems)
        {
            Grid[item.RowNumber, item.ColumnNumber] = item;
            if(item is SchematicNumber number)
            {
                for(int currentColumn = number.ColumnNumber+1; currentColumn < number.MaxColumnNumber; currentColumn++)
                {
                    Grid[item.RowNumber, currentColumn] = item;
                }
            }
        }
    }  

    public List<SchematicGear> GetSchematicGears()
    {
        const int GearPartNumberCount = 2;
        var possibleGears = _schematicItems.Where(item => item is SchematicSymbol symbol && symbol.Value == "*").Cast<SchematicSymbol>();
        var schematicGears = new List<SchematicGear>();
        foreach(var possibleGear in possibleGears)
        {
            var adjecentPartNumbers = GetAdjacentPartNumbersForSymbol(possibleGear).ToArray();
            if(adjecentPartNumbers.Length == GearPartNumberCount)
            {
                schematicGears.Add(new SchematicGear(adjecentPartNumbers[0], adjecentPartNumbers[1]));
            }
        }
        return schematicGears;
    }

    public HashSet<SchematicNumber> GetSchematicPartNumbers()
    {
        var schematicSymbols = _schematicItems.Where(item => item is SchematicSymbol).Cast<SchematicSymbol>();
        var schematicPartNumbers = new HashSet<SchematicNumber>();
        foreach(var symbol in schematicSymbols)
        {
            var adjacentPartNumbers = GetAdjacentPartNumbersForSymbol(symbol);
            schematicPartNumbers.UnionWith(adjacentPartNumbers);
        }

        return schematicPartNumbers;
    }

    private HashSet<SchematicNumber> GetAdjacentPartNumbersForSymbol(SchematicSymbol symbol)
    {
        var schematicPartNumbers = new HashSet<SchematicNumber>();
        var validAdjacentCoordinates = symbol.GetAdjacentCoordinates().Where(c => c.Row < GridRowCount && c.Column < GridColumnCount);
        foreach(var adjacentCoordinate in validAdjacentCoordinates)
        {
            var adjacentItem = Grid[adjacentCoordinate.Row, adjacentCoordinate.Column];
            if(adjacentItem is SchematicNumber partNumber)
            {
                schematicPartNumbers.Add(partNumber);
            }
        }
        return schematicPartNumbers;
    }

    public void PrintGrid()
    {
        for (int row = 0; row < GridRowCount; row++)
        {
            for (int column = 0; column < GridColumnCount; column++)
            {
                var item = Grid[row, column];
                Console.Write(item is null ? "." : item.ToString());
            }
            Console.WriteLine();
        }
    }
}