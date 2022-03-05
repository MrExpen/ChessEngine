using ChessLib.Data;
using ChessLib.Data.Analysis;
using ChessLib.Data.Structures;




var game = new ChessGame();
BoardAnalyzer analyzer = new BoardAnalyzer(new PositionAnalyzer());

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
    analyzer.Analyze(new AnalyzedMove(game.Current));
    Console.WriteLine(DateTime.Now - time);
    while (true)
    {
        try
        {
            game.Move(new ChessMove(new ChessPosition(Console.ReadLine()!), new ChessPosition(Console.ReadLine()!)));
            break;
        }
        catch (Exception e)
        {
            Console.WriteLine("invalid move");
        }
    }
}

Console.WriteLine("done!");