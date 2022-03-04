using ChessLib.Data;
using ChessLib.Data.Enums;
using ChessLib.Data.Structures;

namespace ChessLib.Figures;

public class Pawn : ChessFigure
{
    public Pawn(ChessColor color, ChessPosition position, Board board) : base(EnumFigure.Pawn, color, position, board)
    {
    }

    public override List<ChessPosition> GetMoves()
    {
        var positions = new List<ChessPosition>();

        if (Color == ChessColor.White)
        {
            if (Board.GetFigure(Position + new ChessPosition(0, 1)) is null)
            {
                positions.Add(Position + new ChessPosition(0, 1));
            }
            if (Position.Y == 1 && Board.GetFigure(Position + new ChessPosition(0, 1)) is null &&
                Board.GetFigure(Position + new ChessPosition(0, 2)) is null)
            {
                positions.Add(Position + new ChessPosition(0, 2));
            }

            if ((Position + new ChessPosition(1, 1)).IsValid &&
                Board.GetFigure(Position + new ChessPosition(1, 1))?.Color == Color.Flipped()
                || Board.LastPawnLongMove == Position + new ChessPosition(1, 1))
            {
                positions.Add(Position + new ChessPosition(1, 1));
            }
            if ((Position + new ChessPosition(-1, 1)).IsValid &&
                Board.GetFigure(Position + new ChessPosition(-1, 1))?.Color == Color.Flipped()
                || Board.LastPawnLongMove == Position + new ChessPosition(-1, 1))
            {
                positions.Add(Position + new ChessPosition(-1, 1));
            }
            
            
        }
        else if (Color == ChessColor.Black)
        {
            if (Board.GetFigure(Position + new ChessPosition(0, -1)) is null)
            {
                positions.Add(Position + new ChessPosition(0, -1));
            }
            if (Position.Y == 6 && Board.GetFigure(Position + new ChessPosition(0, -1)) is null &&
                Board.GetFigure(Position + new ChessPosition(0, -2)) is null)
            {
                positions.Add(Position + new ChessPosition(0, -2));
            }

            if ((Position + new ChessPosition(1, -1)).IsValid &&
                Board.GetFigure(Position + new ChessPosition(1, -1))?.Color == Color.Flipped()
                || Board.LastPawnLongMove == Position + new ChessPosition(1, -1))
            {
                positions.Add(Position + new ChessPosition(1, -1));
            }

            if ((Position + new ChessPosition(-1, -1)).IsValid &&
                Board.GetFigure(Position + new ChessPosition(-1, -1))?.Color == Color.Flipped()
                || Board.LastPawnLongMove == Position + new ChessPosition(-1, -1))
            {
                positions.Add(Position + new ChessPosition(-1, -1));
            }
        }

        return positions.Where(x => !MakesCheck(x)).ToList();
    }
}