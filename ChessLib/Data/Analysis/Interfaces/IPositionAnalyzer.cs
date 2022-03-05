namespace ChessLib.Data.Analysis.Interfaces;

public interface IPositionAnalyzer
{
    /// <summary>
    /// Return a White position valuation - Black position valuation
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public double EvaluatePosition(Board board);
}