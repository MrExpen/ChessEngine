using ChessLib.Data.Structures;

namespace ChessLib.Data.Analysis;

public class AlphaBettaNode
{
    public AlphaBettaNode(Board board, ChessMove move, int deep, int score)
    {
        Move = move;
        Board = board;
        Deep = deep;
        Score = score;
    }

    public ChessMove Move { get; }
    public Board Board { get; }
    public int Deep { get; }
    public int Score { get; }
    
}