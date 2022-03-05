using ChessLib.Data.Structures;

namespace ChessLib.Data.Analysis.Interfaces;

public interface IMoveAnalyzer
{
    public ChessMove GetBestMove(Board board, int deep = 6);
}