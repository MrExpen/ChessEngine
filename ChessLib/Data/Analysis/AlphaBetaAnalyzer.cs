using System.Diagnostics;
using ChessLib.Data.Analysis.Interfaces;
using ChessLib.Data.Enums;
using ChessLib.Data.Structures;

namespace ChessLib.Data.Analysis;

public class AlphaBetaAnalyzer : IMoveAnalyzer
{
    private readonly IPositionAnalyzer _positionAnalyzer;

    public AlphaBetaAnalyzer(IPositionAnalyzer positionAnalyzer)
    {
        _positionAnalyzer = positionAnalyzer;
    }

    public ChessMove GetBestMove(Board board, int deep = 6)
    {
        object _lock = new object();
        var bestMove = -300000;
        ChessMove bestMoveFound = new ChessMove();

        foreach (var move in board.GetAllMoves())
        {
            var newGame = board.Move(move, false);
            var value = Analyze(newGame, deep - 1, -300000, 300000, true);
            if (value >= bestMove)
            {
                bestMove = value;
                bestMoveFound = move;
            }
        }

        // Parallel.ForEach(board.GetAllMoves(), move =>
        // {
        //     var newGame = board.Move(move, false);
        //     var value = Analyze(newGame, deep - 1, -300000, 300000, true);
        //     lock (_lock)
        //     {
        //         if (value >= bestMove)
        //         {
        //             bestMove = value;
        //             bestMoveFound = move;
        //         }
        //     }
        // });
        return bestMoveFound;
    }

    private int Analyze(Board board, int deep, int alpha, int beta, bool isMaximisingPlayer)
    {
        if (deep == 0)
        {
            return -_positionAnalyzer.EvaluatePosition(board, isMaximisingPlayer ? board.Turn : board.Turn.Flipped());
        }

        int bestMove = isMaximisingPlayer ? -500000 : 500000;
        
        if (isMaximisingPlayer)
        {
            foreach (var move in board.GetAllMoves())
            {
                bestMove = Math.Max(bestMove, Analyze(board.Move(move, false), deep - 1, alpha, beta, !isMaximisingPlayer));
                alpha = Math.Max(alpha, bestMove);
                if (beta <= alpha)
                {
                    return bestMove;
                }
            }
        }
        else
        {
            foreach (var move in board.GetAllMoves())
            {
                bestMove = Math.Min(bestMove, Analyze(board.Move(move, false), deep - 1, alpha, beta, !isMaximisingPlayer));
                beta = Math.Max(beta, bestMove);
                if (beta <= alpha)
                {
                    return bestMove;
                }
            }
        }

        return Math.Abs(bestMove) == 500000 ? -bestMove : bestMove;
    }
}