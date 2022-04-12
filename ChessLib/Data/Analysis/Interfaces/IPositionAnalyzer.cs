using ChessLib.Data.Enums;

namespace ChessLib.Data.Analysis.Interfaces;

public interface IPositionAnalyzer
{
    public int EvaluatePosition(Board board, ChessColor color = ChessColor.White);
}