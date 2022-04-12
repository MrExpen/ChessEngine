using ChessLib.Data.Analysis.Interfaces;
using ChessLib.Data.Enums;
using ChessLib.Figures;

namespace ChessLib.Data.Analysis;

public class PositionAnalyzer : IPositionAnalyzer
{
    public double EvaluatePosition(Board board)
    {
        return board.AllFigures.Sum(x => GetFigureScore(x, board));
    }

    private double GetFigureScore(ChessFigure chessFigure, Board board)
    {
        double score = 0;
        switch (chessFigure.EnumFigure)
        {
            case EnumFigure.Pawn:
            {
                score += 1;
                switch (chessFigure.Color)
                {
                    case ChessColor.White:
                        score += chessFigure.Position.Y * 0.2;
                        break;
                    case ChessColor.Black:
                        score += (7 - chessFigure.Position.Y) * 0.2;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score -= 0.5;
                }
                break;
            }
            case EnumFigure.Bishop:
            case EnumFigure.Knight:
            {
                score += 3;
                score += chessFigure.GetMoves().Count() * 0.1;
                
                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score -= 1.5;
                }
                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score += 0.5;
                }
                break;
            }
            case EnumFigure.Rook:
            {
                score += 5;
                score += chessFigure.GetMoves().Count() * 0.1;
                
                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score -= 3;
                }
                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score += 0.5;
                }
                break;
            }
            case EnumFigure.Queen:
            {
                score += 9;
                score += chessFigure.GetMoves().Count() * 0.1;
                
                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score -= 7;
                }
                break;
            }
            case EnumFigure.King:
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        return score * chessFigure.Color switch 
        {
            ChessColor.White => 1,
            ChessColor.Black => -1,
            _ => throw new NotSupportedException()
        };
    }
}