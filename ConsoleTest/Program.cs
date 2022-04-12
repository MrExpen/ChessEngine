using ChessLib.Data;
using ChessLib.Data.Analysis;
using ChessLib.Data.Enums;
using ChessLib.Data.Structures;

var game = new ChessGame();
var analyzer = new BoardAnalyzer(new PositionAnalyzer());

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
    var time = DateTime.Now;
    var analyzed = new AnalyzedMove(game.Current);
    if (game.Current.Turn == ChessColor.White)
    {
        analyzer.Analyze(analyzed);
        game.Move(analyzed.DeepMoves.First().Move!.Value);
    }
    else
    {
        analyzer.Analyze(analyzed, 3);
        game.Move(analyzed.DeepMoves.MinBy(x => x.Score).Move.Value);
    }
    Console.WriteLine(DateTime.Now - time);
}

Console.WriteLine("done!");