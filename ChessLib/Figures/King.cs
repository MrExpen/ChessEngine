using System.Security.Cryptography;
using ChessLib.Data;
using ChessLib.Data.Enums;
using ChessLib.Data.Structures;

namespace ChessLib.Figures;

public class King : ChessFigure
{
    public King(ChessColor color, ChessPosition position, Board board) : base(EnumFigure.King, color, position, board)
    {
    }

    public override List<ChessPosition> GetMoves()
    {
        var positions = new List<ChessPosition>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                var pos = Position + new ChessPosition(i, j);
                
                if (i == 0 && j == 0 || !pos.IsValid)
                {
                    continue;
                }

                if (Board.GetFigure(pos) is null || Board.GetFigure(pos)?.Color != Color)
                {
                    positions.Add(pos);
                }
            }
        }

        if (Color == ChessColor.White)
        {
            if (Board.WhiteLongCastling 
                && Board.GetFigure(new ChessPosition(1, 0)) is null
                && Board.GetFigure(new ChessPosition(2, 0)) is null
                && Board.GetFigure(new ChessPosition(3, 0)) is null
                && !Board.IsUnderAttack(new ChessPosition(2, 0), Color.Flipped())
                && !Board.IsUnderAttack(new ChessPosition(3, 0), Color.Flipped())
                && !Board.IsUnderAttack(new ChessPosition(4, 0), Color.Flipped()))
            {
                positions.Add(new ChessPosition(2, 0));
            }

            if (Board.WhiteShortCastling
                && Board.GetFigure(new ChessPosition(5, 0)) is null
                && Board.GetFigure(new ChessPosition(6, 0)) is null
                && !Board.IsUnderAttack(new ChessPosition(4, 0), Color.Flipped())
                && !Board.IsUnderAttack(new ChessPosition(5, 0), Color.Flipped())
                && !Board.IsUnderAttack(new ChessPosition(6, 0), Color.Flipped()))
            {
                positions.Add(new ChessPosition(6, 0));
            }
        }
        else if (Color == ChessColor.Black)
        {
            if (Board.BlackLongCastling
                && Board.GetFigure(new ChessPosition(1, 7)) is null
                && Board.GetFigure(new ChessPosition(2, 7)) is null
                && Board.GetFigure(new ChessPosition(3, 7)) is null
                && !Board.IsUnderAttack(new ChessPosition(2, 7), Color.Flipped())
                && !Board.IsUnderAttack(new ChessPosition(3, 7), Color.Flipped())
                && !Board.IsUnderAttack(new ChessPosition(4, 7), Color.Flipped()))
            {
                positions.Add(new ChessPosition(2, 7));
            }

            if (Board.BlackShortCastling
                && Board.GetFigure(new ChessPosition(5, 7)) is null
                && Board.GetFigure(new ChessPosition(6, 7)) is null
                && !Board.IsUnderAttack(new ChessPosition(4, 7), Color.Flipped())
                && !Board.IsUnderAttack(new ChessPosition(5, 7), Color.Flipped())
                && !Board.IsUnderAttack(new ChessPosition(6, 7), Color.Flipped()))
            {
                positions.Add(new ChessPosition(6, 7));
            }
        }

        return positions.Where(x => !MakesCheck(x)).ToList();
    }
}