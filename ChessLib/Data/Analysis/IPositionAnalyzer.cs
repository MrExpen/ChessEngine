namespace ChessLib.Data.Analysis;

public interface IPositionAnalyzer
{
    /// <summary>
    /// Return a White position valuation - Black position valuation
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public double EvaluatePosition(Board board);
}