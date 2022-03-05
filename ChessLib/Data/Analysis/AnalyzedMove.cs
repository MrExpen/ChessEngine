namespace ChessLib.Data.Analysis;

public class AnalyzedMove
{
    private Board? _board;

    public Board Board 
    {
        get
        {
            _board ??= new Board(Fen);

            return _board;
        }
    }
    
    public string Fen { get; }
    public double? Score { get; set; }
    public HashSet<AnalyzedMove> DeepMoves { get; } = new();

    public AnalyzedMove(Board board)
    {
        _board = board;
        Fen = _board.Fen;
    }

    public AnalyzedMove(string fen)
    {
        Fen = fen;
    }

    public void ClearCache()
    {
        _board = null;
    }
    
    protected bool Equals(AnalyzedMove other)
    {
        return Board.Equals(other.Board);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AnalyzedMove)obj);
    }

    public override int GetHashCode()
    {
        return Fen.GetHashCode();
    }

    public static bool operator ==(AnalyzedMove? left, AnalyzedMove? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(AnalyzedMove? left, AnalyzedMove? right)
    {
        return !Equals(left, right);
    }
}