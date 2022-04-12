using ChessLib.Data.Analysis.Interfaces;
using ChessLib.Data.Enums;
using ChessLib.Figures;

namespace ChessLib.Data.Analysis;

public class PositionAnalyzer : IPositionAnalyzer
{
    public int EvaluatePosition(Board board, ChessColor color = ChessColor.White)
    {
        return board.Winner switch
        {
            ChessColor.White => 300000,
            ChessColor.Black => -300000,
            _ => board.AllFigures.Sum(x => GetFigureScore(x, board))
        } * color switch
        {
            ChessColor.White => 1,
            ChessColor.Black => -1,
            _ => throw new NotSupportedException()
        };
    }

    private int GetFigureScore(ChessFigure chessFigure, Board board)
    {
        int score = 0;
        switch (chessFigure.EnumFigure)
        {
            case EnumFigure.Pawn:
            {
                score += 1000;
                switch (chessFigure.Color)
                {
                    case ChessColor.White:
                        score += chessFigure.Position.Y * 200;
                        break;
                    case ChessColor.Black:
                        score += (7 - chessFigure.Position.Y) * 200;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score -= 500;
                }
                break;
            }
            case EnumFigure.Bishop:
            case EnumFigure.Knight:
            {
                score += 3000;
                score += chessFigure.GetMoves().Count() * 100;
                
                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score -= 1500;
                }
                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score += 500;
                }
                break;
            }
            case EnumFigure.Rook:
            {
                score += 5000;
                score += chessFigure.GetMoves().Count() * 100;
                
                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score -= 3000;
                }
                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score += 500;
                }
                break;
            }
            case EnumFigure.Queen:
            {
                score += 9000;
                score += chessFigure.GetMoves().Count() * 100;
                
                if (board.IsUnderAttack(chessFigure.Position, chessFigure.Color.Flipped()))
                {
                    score -= 7000;
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