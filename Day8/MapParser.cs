using System.Text.RegularExpressions;

public class MapParser 
{
    private readonly string _rawMap;

    public MapParser(string rawMap)
    {
        _rawMap = rawMap;
    }

    public Map ParseMap()
    {
        var instructions = ParseInstructions().ToList();
        var nodes = ParseNodes();
        return new Map
        {
            Nodes = nodes,
            Instructions = instructions
        };
    }

    private IEnumerable<Direction> ParseInstructions()
    {
        var rawInstructions = _rawMap.Split("\r\n").First();
        foreach(var rawInstruction in rawInstructions)
        {
            yield return rawInstruction switch
            {
                'L' => Direction.Left,
                'R' => Direction.Right,
                _ => throw new NotImplementedException()
            };
        }
    }

    private List<Node> ParseNodes()
    {
        var rawNodes = _rawMap.Split("\r\n").Skip(2).ToArray();
        var nodes = new List<Node>();   

        var nodeRegex = new Regex("([0-9A-Z][0-9A-Z][0-9A-Z])\\s+=\\s+\\(([0-9A-Z][0-9A-Z][0-9A-Z]),\\s+([0-9A-Z][0-9A-Z][0-9A-Z])\\)");     
        foreach(var rawNode in rawNodes)
        {
            var match = nodeRegex.Match(rawNode);
            var node = nodes.FirstOrDefault(n => n.Id == match.Groups[1].Value) ?? new Node { Id = match.Groups[1].Value };
            nodes.Add(node);

            var leftNode = nodes.FirstOrDefault(n => n.Id == match.Groups[2].Value);
            if(leftNode is null)
            {
                leftNode = new Node { Id = match.Groups[2].Value };
                nodes.Add(leftNode);
            } 

            var rightNode = nodes.FirstOrDefault(n => n.Id == match.Groups[3].Value);
            if(rightNode is null)
            {
                rightNode = new Node { Id = match.Groups[3].Value };
                nodes.Add(rightNode);
            } 

            node.SetConnectingNodes(leftNode, rightNode);
        }

        return nodes;
    }
}