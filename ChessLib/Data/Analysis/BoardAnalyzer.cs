using ChessLib.Data.Analysis.Interfaces;
using ChessLib.Data.Structures;

namespace ChessLib.Data.Analysis;

public class BoardAnalyzer : IBoardAnalyzer
{
    private AnalyzedMove? _analyzedMove;
    private readonly IPositionAnalyzer _positionAnalyzer;

    public BoardAnalyzer(IPositionAnalyzer positionAnalyzer)
    {
        _positionAnalyzer = positionAnalyzer;
    }

    public void Analyze(AnalyzedMove analyzedMove, int deep = 6, bool main = true)
    {
        if (main)
        {
            _analyzedMove ??= analyzedMove;
            _analyzedMove = analyzedMove.DeepMoves.TryGetValue(analyzedMove, out var actualMove)
                ? actualMove
                : analyzedMove;
        }

        if (deep == 0)
        {
            return;
        }

        analyzedMove.Score = _positionAnalyzer.EvaluatePosition(analyzedMove.Board);
        
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
    }
}