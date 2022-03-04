namespace ChessLib.Data;

public class AnalyzedMove
{
    public Board Board { get; }
    public double Score { get; set; }
    public HashSet<AnalyzedMove> DeepMoves { get; } = new();

    public AnalyzedMove(Board board)
    {
        Board = board;
    }
}