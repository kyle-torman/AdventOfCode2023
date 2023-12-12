public class GalaxyParser
{
    private readonly string _rawMap;
    public GalaxyParser(string rawMap)
    {
        _rawMap = rawMap;
    }

    public List<Galaxy> ParseGalaxiesWithExpansion(int expansion)
    {
        var rowExpansionIndexes = GetRowExpansionIndexes().ToList();
        var columnExpansionIndexes = GetColumnExpansionIndexes().ToList();
        var galaxies = ParseGalaxiesWithExpansion(rowExpansionIndexes, columnExpansionIndexes, expansion).ToList();
        return galaxies;
    }

    public List<Galaxy> ParseGalaxies()
    {
        var expandedRawMap = ExpandRawMap();
        var galaxies = ParseGalaxies(expandedRawMap).ToList();
        return galaxies;
    }

    private IEnumerable<Galaxy> ParseGalaxies(List<string> expandRawMapRows)
    {
        var currentId = 1;
        for(int row = 0; row < expandRawMapRows.Count; row++)
        {
            var rawMapRow = expandRawMapRows[row];
            for(int column = 0; column < rawMapRow.Length; column++)
            {
                if(rawMapRow[column] == '#')
                {
                    yield return new Galaxy(currentId++, row, column);
                }
            }
        }
    }

    private IEnumerable<Galaxy> ParseGalaxiesWithExpansion(List<int> rowExpansionIndexes, List<int> columnExpansionIndexes, int expansionSize)
    {
        var rawMapRows = _rawMap.Split("\r\n").ToList();
        var currentId = 1;    
        for(int row = 0; row < rawMapRows.Count; row++)
        {
            var rawMapRow = rawMapRows[row];
            for(int column = 0; column < rawMapRow.Length; column++)
            {
                if(rawMapRow[column] == '#')
                {
                    var rowExpansionsBeforeGalaxy = rowExpansionIndexes.Count(r => r < row);
                    var columnExpansionsBeforeGalaxy = columnExpansionIndexes.Count(c => c < column);
                    var rowExpansion = rowExpansionsBeforeGalaxy * expansionSize;                    
                    var columnExpansion = columnExpansionsBeforeGalaxy * expansionSize;
                    //Need to remove this because the raw map counts for a single instance of each expansion
                    rowExpansion = rowExpansion == 0 ? 0 : rowExpansion - rowExpansionsBeforeGalaxy;
                    columnExpansion = columnExpansion == 0 ? 0 : columnExpansion - columnExpansionsBeforeGalaxy;
                    yield return new Galaxy(currentId++, row + rowExpansion, column + columnExpansion);
                }
            }
        }
    }

    private IEnumerable<int> GetRowExpansionIndexes()
    {
        var rawMapRows = _rawMap.Split("\r\n").ToList();
        for(int row = 0; row < rawMapRows.Count; row++)
        {
            if(rawMapRows[row].All(c => c == '.'))
            {
                yield return row;
            }
        }        
    }

    private IEnumerable<int> GetColumnExpansionIndexes()
    {
        var rawMapRows = _rawMap.Split("\r\n").ToList();
        for(int column = 0; column<rawMapRows[0].Length; column++)
        {
            var isExpandableColumn = rawMapRows.All(row => row[column] == '.');
            if(isExpandableColumn)
            {
                yield return column;
            }
        }
    }

    private List<string> ExpandRawMap()
    {
        var rawMapRows = _rawMap.Split("\r\n").ToList();
        var expandableColumns = new List<int>();
        for(int column = 0; column<rawMapRows[0].Length; column++)
        {
            var isExpandableColumn = rawMapRows.All(row => row[column] == '.');
            if(isExpandableColumn)
            {
                expandableColumns.Add(column);
            }
        }
        for(int columnsAdded = 0; columnsAdded < expandableColumns.Count; columnsAdded++)
        {
            var column = columnsAdded + expandableColumns[columnsAdded];
            for(int row = 0; row < rawMapRows.Count; row++)
            {
                rawMapRows[row] = rawMapRows[row].Insert(column, ".");
            }
        }

        var expandableRows = rawMapRows.Select((row, index) => new {row, index}).Where(x => x.row.All(c => c == '.')).ToArray();
        for(int rowsAdded = 0; rowsAdded < expandableRows.Length; rowsAdded++)
        {
            var row = rowsAdded + expandableRows[rowsAdded].index;
            rawMapRows.Insert(row, expandableRows[rowsAdded].row);
        }

        return rawMapRows;
    }
}