using ChessLib.Data;
using ChessLib.Data.Enums;
using ChessLib.Data.Structures;

namespace ChessLib.Figures;

public class Queen : ChessFigure
{
    public Queen(ChessColor color, ChessPosition position, Board board) : base(EnumFigure.Queen, color, position, board)
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
        
        for (var pos = Position + new ChessPosition(1, 0); pos.IsValid; pos += new ChessPosition(1, 0))
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
        for (var pos = Position + new ChessPosition(-1, 0); pos.IsValid; pos += new ChessPosition(-1, 0))
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
        for (var pos = Position + new ChessPosition(0, 1); pos.IsValid; pos += new ChessPosition(0, 1))
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
        for (var pos = Position + new ChessPosition(0, -1); pos.IsValid; pos += new ChessPosition(0, -1))
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