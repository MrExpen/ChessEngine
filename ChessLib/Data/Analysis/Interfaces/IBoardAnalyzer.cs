namespace ChessLib.Data.Analysis.Interfaces;


public interface IBoardAnalyzer
{
    public void Analyze(AnalyzedMove analyzedMove, int deep, bool main = true);
}