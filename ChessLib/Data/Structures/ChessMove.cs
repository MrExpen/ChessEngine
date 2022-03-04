using ChessLib.Data.Enums;

namespace ChessLib.Data.Structures;

public readonly struct ChessMove
{
    public readonly ChessPosition From;
    public readonly ChessPosition To;
    public readonly Func<EnumFigure> Func;

    public ChessMove(ChessPosition from, ChessPosition to, Func<EnumFigure>? func=null)
    {
        From = from;
        To = to;
        Func = func ?? (() => EnumFigure.Queen);
    }
}