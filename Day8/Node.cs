public record Node
{
    public required string Id {get; init;}
    public Node? Left {get; private set;}
    public Node? Right {get; private set;}

    public void SetConnectingNodes(Node left, Node right)
    {
        Left = left;
        Right = right;
    }

    public Node NavigateNode(Direction direction)
    {
        return direction switch
        {
            Direction.Left => Left!,
            Direction.Right => Right!,
            _ => throw new NotImplementedException()
        };
    }

    public void Print()
    {
        Console.WriteLine($"{Id} - ({Left!.Id}, {Right!.Id})");
    }
}