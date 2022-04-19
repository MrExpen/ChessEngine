using ChessLib.Data;
using ChessLib.Data.Analysis;
using ChessLib.Data.Enums;
using ChessLib.Data.Structures;

var game = new ChessGame();
var analyzer = new AlphaBetaAnalyzer(new PositionAnalyzer());

while (!game.Current.Tie || !game.Current.Winner.HasValue)
{
    for (int y = 7; y >= 0; y--)
    {
        for (int x = 0; x < 8; x++)
        {
            Console.Write(game.Current.GetFigure(x, y)?.Char ?? '.');
        }
        Console.WriteLine();
    }
    if (game.Current.Turn == ChessColor.Black)
    {
        var time = DateTime.Now;
        var bestMove = analyzer.GetBestMove(game.Current, 4);
        Console.WriteLine(DateTime.Now - time);
        Console.WriteLine(bestMove.From.ToString() + bestMove.To.ToString());
        game.Move(bestMove);
    }
    else
    {
        var input = Console.ReadLine()!;
        game.Move(new ChessMove(new ChessPosition(input.Substring(0, 2)), new ChessPosition(input.Substring(2, 2))));
    }
}

Console.WriteLine("done!");