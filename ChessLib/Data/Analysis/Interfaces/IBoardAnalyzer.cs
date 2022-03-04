namespace ChessLib.Data.Analysis.Interfaces;


public interface IBoardAnalyzer
{
    public void Analyze(AnalyzedMove analyzedMove, int deep = 6, bool main = true);
}