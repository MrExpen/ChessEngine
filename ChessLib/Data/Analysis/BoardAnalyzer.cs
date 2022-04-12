using ChessLib.Data.Analysis.Interfaces;
using ChessLib.Data.Structures;

namespace ChessLib.Data.Analysis;

public class BoardAnalyzer : IBoardAnalyzer
{
    private readonly IPositionAnalyzer _positionAnalyzer;

    public BoardAnalyzer(IPositionAnalyzer positionAnalyzer)
    {
        _positionAnalyzer = positionAnalyzer;
    }

    public void Analyze(AnalyzedMove analyzedMove, int deep = 4)
    {

        if (deep == 0)
        {
            analyzedMove.Score = _positionAnalyzer.EvaluatePosition(analyzedMove.Board);
            analyzedMove.ClearCache();
            return;
        }
        
        if (!analyzedMove.DeepMoves.Any())
        {
            foreach (var figure in analyzedMove.Board.AllFigures.Where(x => x.Color == analyzedMove.Board.Turn))
            {
                foreach (var position in figure.GetMoves())
                {
                    var move = new ChessMove(figure.Position, position);
                    analyzedMove.DeepMoves.Add(new AnalyzedMove(analyzedMove.Board.Move(move, false)){Move = move});
                }
            }
        }

        Parallel.ForEach(analyzedMove.DeepMoves, move => Analyze(move, deep - 1));

        if (deep % 2 != 0)
        {
            analyzedMove.Score = analyzedMove.DeepMoves.Min(x => x.Score);
        }
        else
        {
            var max = analyzedMove.DeepMoves.MaxBy(x => x.Score)!;
            analyzedMove.DeepMoves.Clear();
            analyzedMove.DeepMoves.Add(max);
            analyzedMove.Score = max.Score;
        }
        
        
        
        analyzedMove.ClearCache();
    }
}