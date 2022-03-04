using ChessLib.Data.Structures;

namespace ChessLib.Data;

public class ChessGame
{
    public LinkedList<Board> Moves { get; } = new ();
    public Board Current => Moves.Last!.Value;

    public ChessGame(string startFen = Board.DefaultFen)
    {
        Moves.AddLast(new Board(startFen));
    }

    public void Move(ChessMove chessMove)
    {
        Moves.AddLast(Current.Move(chessMove));
    }
}