using System.Text.RegularExpressions;

public class EngineSchematicParser 
{
    private readonly string _rawSchematic;

    public EngineSchematicParser(string rawSchematic)
    {
        _rawSchematic = rawSchematic;        
    }

    public EngineSchematic ParseEngineSchematic() 
    {
        var rawSchematicLines = _rawSchematic.Split("\r\n");    
        
        var schematicItems = new List<SchematicItem>();
        for(int rowNumber = 0; rowNumber < rawSchematicLines.Length; rowNumber++)
        {
            var itemsInRow = ParseSchematicItemsForRow(rawSchematicLines[rowNumber], rowNumber);
            schematicItems.AddRange(itemsInRow);
        }
        
        return new EngineSchematic(schematicItems);
    }

    private IEnumerable<SchematicItem> ParseSchematicItemsForRow(string rawSchematicRow, int rowNumber)
    {
        var schematicItemRegex = new Regex("[0-9]+|[^\\.]");
        var matches = schematicItemRegex.Matches(rawSchematicRow);
        foreach(Match match in matches)
        {
                SchematicItem schematicItem = match.IsNumeric() ? 
                    new SchematicNumber 
                    { 
                        Value = int.Parse(match.Value), 
                        RowNumber = rowNumber, 
                        ColumnNumber = match.Index, 
                        ColumnSpan = match.Length 
                    } :
                    new SchematicSymbol 
                    {
                        Value = match.Value,
                        RowNumber = rowNumber,
                        ColumnNumber = match.Index                        
                    };       
                yield return schematicItem;
        }
    }    
}