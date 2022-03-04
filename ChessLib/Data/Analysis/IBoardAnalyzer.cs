using ChessLib.Data.Structures;

namespace ChessLib.Data.Analysis;


public interface IBoardAnalyzer
{
    public ChessMove GetBestMove(Board board, int deep = 3);
}