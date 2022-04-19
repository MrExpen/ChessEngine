using ChessLib.Data;
using ChessLib.Data.Enums;
using ChessLib.Data.Structures;

namespace ChessLib.Figures;

public class Knight : ChessFigure
{
    public Knight(ChessColor color, ChessPosition position, Board board) : base(EnumFigure.Knight, color, position, board)
    {
    }

    public override IEnumerable<ChessPosition> GetMoves()
    {
        return new[]
        {
            Position + new ChessPosition(1, 2),
            Position + new ChessPosition(1, -2),
            Position + new ChessPosition(-1, 2),
            Position + new ChessPosition(-1, -2),

            Position + new ChessPosition(2, 1),
            Position + new ChessPosition(2, -1),
            Position + new ChessPosition(-2, 1),
            Position + new ChessPosition(-2, -1)
        }.Where(x => x.IsValid && Board.GetFigure(x)?.Color != Color && !MakesCheck(x));
    }
}