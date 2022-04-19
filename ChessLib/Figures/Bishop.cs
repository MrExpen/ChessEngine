using ChessLib.Data;
using ChessLib.Data.Enums;
using ChessLib.Data.Structures;

namespace ChessLib.Figures;

public class Bishop : ChessFigure
{
    public Bishop(ChessColor color, ChessPosition position, Board board) : base(EnumFigure.Bishop, color, position, board)
    {
    }

    public override IEnumerable<ChessPosition> GetMoves()
    {
        for (var pos = Position + new ChessPosition(1, 1); pos.IsValid; pos += new ChessPosition(1, 1))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                if (!MakesCheck(pos))
                {
                    yield return pos;
                }
            }
            else 
            {
                if (figure.Color != Color)
                {
                    if (!MakesCheck(pos))
                    {
                        yield return pos;
                    }
                }
                
                break;
            }
        }
        for (var pos = Position + new ChessPosition(-1, -1); pos.IsValid; pos += new ChessPosition(-1, -1))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                if (!MakesCheck(pos))
                {
                    yield return pos;
                }
            }
            else 
            {
                if (figure.Color != Color)
                {
                    if (!MakesCheck(pos))
                    {
                        yield return pos;
                    }
                }
                
                break;
            }
        }
        for (var pos = Position + new ChessPosition(-1, 1); pos.IsValid; pos += new ChessPosition(-1, 1))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                if (!MakesCheck(pos))
                {
                    yield return pos;
                }
            }
            else 
            {
                if (figure.Color != Color)
                {
                    if (!MakesCheck(pos))
                    {
                        yield return pos;
                    }
                }
                
                break;
            }
        }
        for (var pos = Position + new ChessPosition(1, -1); pos.IsValid; pos += new ChessPosition(1, -1))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                if (!MakesCheck(pos))
                {
                    yield return pos;
                }
            }
            else 
            {
                if (figure.Color != Color)
                {
                    if (!MakesCheck(pos))
                    {
                        yield return pos;
                    }
                }
                
                break;
            }
        }
    }
}