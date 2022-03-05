using ChessLib.Data;
using ChessLib.Data.Enums;
using ChessLib.Data.Structures;

namespace ChessLib.Figures;

public abstract class ChessFigure
{
    protected readonly Board Board;
    
    public ChessPosition Position { get; set; }
    public ChessColor Color { get; }
    public EnumFigure EnumFigure { get; }

    public char Char => Color switch
    {
        ChessColor.None => throw new NotSupportedException(),
        ChessColor.White => char.ToUpper((char)EnumFigure),
        _ => (char)EnumFigure
    };

    public abstract List<ChessPosition> GetMoves();

    public virtual bool CanMoveTo(ChessPosition position)
    {
        //TODO optimize for classes
        return GetMoves().Contains(position);
    }

    protected bool MakesCheck(ChessPosition newPosition)
    {
        return Board.IsCheckedIf(new ChessMove(Position, newPosition), Color);
    }
    
    protected ChessFigure(EnumFigure enumFigure, ChessColor color, ChessPosition position, Board board)
    {
        Position = position;
        EnumFigure = enumFigure;
        Color = color;
        Board = board;
    }
}