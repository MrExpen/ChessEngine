using ChessLib;
using ChessLib.Data;
using ChessLib.Data.Structures;

var game = new ChessGame();

while (!game.Current.Tie && !game.Current.Winner.HasValue)
{
    for (int j = 7; j >= 0; j--)
    {
        for (int i = 0; i < 8; i++)
        {
            Console.Write(game.Current.GetFigure(i, j)?.Char ?? '.');
        }
        Console.WriteLine();
    }

    game.Move(new ChessMove(new ChessPosition(Console.ReadLine()), new ChessPosition(Console.ReadLine())));
}

Console.WriteLine();