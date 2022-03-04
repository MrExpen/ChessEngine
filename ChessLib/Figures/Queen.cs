using ChessLib.Data;
using ChessLib.Data.Enums;
using ChessLib.Data.Structures;

namespace ChessLib.Figures;

public class Queen : ChessFigure
{
    public Queen(ChessColor color, ChessPosition position, Board board) : base(EnumFigure.Queen, color, position, board)
    {
    }

    public override List<ChessPosition> GetMoves()
    {
        var positions = new List<ChessPosition>();
        
        for (var pos = Position + new ChessPosition(1, 1); pos.IsValid; pos += new ChessPosition(1, 1))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                positions.Add(pos);
            }
            else 
            {
                if (figure.Color != Color)
                {
                    positions.Add(pos);
                }
                
                break;
            }
        }
        for (var pos = Position + new ChessPosition(-1, -1); pos.IsValid; pos += new ChessPosition(-1, -1))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                positions.Add(pos);
            }
            else 
            {
                if (figure.Color != Color)
                {
                    positions.Add(pos);
                }
                
                break;
            }
        }
        for (var pos = Position + new ChessPosition(-1, 1); pos.IsValid; pos += new ChessPosition(-1, 1))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                positions.Add(pos);
            }
            else 
            {
                if (figure.Color != Color)
                {
                    positions.Add(pos);
                }
                
                break;
            }
        }
        for (var pos = Position + new ChessPosition(1, -1); pos.IsValid; pos += new ChessPosition(1, -1))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                positions.Add(pos);
            }
            else 
            {
                if (figure.Color != Color)
                {
                    positions.Add(pos);
                }
                
                break;
            }
        }
        
        for (var pos = Position + new ChessPosition(1, 0); pos.IsValid; pos += new ChessPosition(1, 0))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                positions.Add(pos);
            }
            else 
            {
                if (figure.Color != Color)
                {
                    positions.Add(pos);
                }
                
                break;
            }
        }
        for (var pos = Position + new ChessPosition(-1, 0); pos.IsValid; pos += new ChessPosition(-1, 0))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                positions.Add(pos);
            }
            else 
            {
                if (figure.Color != Color)
                {
                    positions.Add(pos);
                }
                
                break;
            }
        }
        for (var pos = Position + new ChessPosition(0, 1); pos.IsValid; pos += new ChessPosition(0, 1))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                positions.Add(pos);
            }
            else 
            {
                if (figure.Color != Color)
                {
                    positions.Add(pos);
                }
                
                break;
            }
        }
        for (var pos = Position + new ChessPosition(0, -1); pos.IsValid; pos += new ChessPosition(0, -1))
        {
            var figure = Board.GetFigure(pos);
            if (figure is null)
            {
                positions.Add(pos);
            }
            else 
            {
                if (figure.Color != Color)
                {
                    positions.Add(pos);
                }
                
                break;
            }
        }

        return positions.Where(x => !MakesCheck(x)).ToList();
    }
}