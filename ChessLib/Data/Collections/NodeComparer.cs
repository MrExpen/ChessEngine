using ChessLib.Data.Analysis;

namespace ChessLib.Data.Collections;

public class NodeComparer : IComparer<AlphaBettaNode>
{
    public int Compare(AlphaBettaNode? x, AlphaBettaNode? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        return x.Score.CompareTo(y.Score);
    }
}