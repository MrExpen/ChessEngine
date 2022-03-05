using ChessLib.Data.Analysis.Interfaces;
using ChessLib.Data.Structures;

namespace ChessLib.Data.Analysis;

public class BoardAnalyzer : IBoardAnalyzer
{
    private AnalyzedMove? _cached;
    private readonly IPositionAnalyzer _positionAnalyzer;

    public BoardAnalyzer(IPositionAnalyzer positionAnalyzer)
    {
        _positionAnalyzer = positionAnalyzer;
    }

    public void Analyze(AnalyzedMove analyzedMove, int deep = 4, bool main = true)
    {
        if (main)
        {
            _cached ??= analyzedMove;
            if (analyzedMove != _cached)
            {
                analyzedMove = _cached.DeepMoves.TryGetValue(analyzedMove, out var actualMove)
                    ? actualMove
                    : analyzedMove;
                _cached = actualMove;
            }
        }

        if (deep == 0)
        {
            analyzedMove.ClearCache();
            return;
        }

        analyzedMove.Score ??= _positionAnalyzer.EvaluatePosition(analyzedMove.Board);
        
        if (!analyzedMove.DeepMoves.Any())
        {
            foreach (var figure in analyzedMove.Board.AllFigures.Where(x => x.Color == analyzedMove.Board.Turn))
            {
                foreach (var position in figure.GetMoves())
                {
                    analyzedMove.DeepMoves.Add(
                        new AnalyzedMove(analyzedMove.Board.Move(new ChessMove(figure.Position, position), false)));
                }
            }
        }

        foreach (var move in analyzedMove.DeepMoves)
        {
            Analyze(move, deep - 1, false);
        }

        analyzedMove.ClearCache();
    }
}