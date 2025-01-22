namespace ZootrBingoServer;

public class BingoBoard
{
    public List<string> ConnectedUsers;
    public string Password;
    public SquareItem[] SquareItems;
}

public class SquareItem
{
    public string Name;
    public string Owner;
}