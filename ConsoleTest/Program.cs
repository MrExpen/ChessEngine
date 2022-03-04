using ChessLib.Data;
using ChessLib.Data.Analysis;
using ChessLib.Data.Structures;

var game = new ChessGame();

BoardAnalyzer analyzer = new BoardAnalyzer(new PositionAnalyzer());

analyzer.Analyze(new AnalyzedMove(game.Current));



Console.WriteLine();