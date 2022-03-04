using ChessLib.Data;
using ChessLib.Data.Enums;
using ChessLib.Data.Structures;

namespace ChessLib.Figures;

public static class ChessFigureFactory
{
    public static ChessFigure Create(EnumFigure enumFigure, ChessColor color, ChessPosition position, Board board)
    {
        return enumFigure switch
        {
            EnumFigure.Pawn => new Pawn(color, position, board),
            EnumFigure.Knight => new Knight(color, position, board),
            EnumFigure.Bishop => new Bishop(color, position, board),
            EnumFigure.Rook => new Rook(color, position, board),
            EnumFigure.Queen => new Queen(color, position, board),
            EnumFigure.King => new King(color, position, board),
            EnumFigure.None => throw new NotSupportedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(enumFigure), enumFigure, null)
        };
    }
}