public class Map
{
    public required List<Direction> Instructions {get; init;}
    public required List<Node> Nodes {get; init;}

    public long CalculateNumberOfGhostStepsInMap()
    {
        var startingNodes = Nodes.Where(n => n.Id.EndsWith("A")).ToArray();
        var stepsForNodes = new List<long>();
        foreach(var node in startingNodes)
        {
            var stepsForNode = CalculateNumberOfStepsForNode(node.Id, true);
            stepsForNodes.Add(stepsForNode);
        }

        return stepsForNodes.LeastCommonMultiple();
    }

    private Node[] NavigateNodes(Node[] nodes, Direction instruction)
    {
        return nodes.Select(n => n.NavigateNode(instruction)).ToArray();
    }

    public long CalculateNumberOfStepsInMap()
    {
        return CalculateNumberOfStepsForNode("AAA", false);
    }

    public long CalculateNumberOfStepsForNode(string nodeId, bool isGhostStep)
    {
        long steps = 0;
        var completedMap = false;
        var currentNode = Nodes.First(n => n.Id == nodeId);
        while(!completedMap)
        {
            foreach(var instruction in Instructions)
            {
                currentNode = currentNode.NavigateNode(instruction);

                steps += 1;

                if(currentNode.Id == "ZZZ" || (isGhostStep && currentNode.Id.EndsWith("Z")))
                {
                    completedMap = true;
                    break;               
                }

            }
        }

        return steps;
    }

    public void Print()
    {
        Console.WriteLine(string.Join("=>", Instructions));
        foreach(var node in Nodes)
        {
            node.Print();
        }
    }
}