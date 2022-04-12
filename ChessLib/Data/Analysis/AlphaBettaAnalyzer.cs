using ChessLib.Data.Analysis.Interfaces;
using ChessLib.Data.Collections;
using ChessLib.Data.Structures;

namespace ChessLib.Data.Analysis;

public class AlphaBettaAnalyzer : IMoveAnalyzer
{
    private readonly IPositionAnalyzer _positionAnalyzer;

    public AlphaBettaAnalyzer(IPositionAnalyzer positionAnalyzer)
    {
        _positionAnalyzer = positionAnalyzer;
    }

    public ChessMove GetBestMove(Board board, int deep = 6)
    {
        var nodes = new MySortedCollection<AlphaBettaNode>(new NodeComparer());
        
        
    }
}