using System.Text;
using ChessLib.Data.Enums;
using ChessLib.Data.Exceptions;
using ChessLib.Data.Structures;
using ChessLib.Figures;

namespace ChessLib.Data;

public class Board
{
    public const string DefaultFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    
    private readonly ChessFigure?[,] _figures = new ChessFigure[8, 8];

    public IEnumerable<ChessFigure> AllFigures => _figures.Cast<ChessFigure>().Where(x => x is not null);

    public ChessFigure WhiteKing { get; private set; }
    public ChessFigure BlackKing { get; private set; }

    public ChessFigure GetKing(ChessColor color) => color switch
    {
        ChessColor.White => WhiteKing,
        ChessColor.Black => BlackKing,
        _ => throw new NotSupportedException()
    };
    
    public ChessColor Turn;

    public bool Tie { get; private set; }
    public ChessColor? Winner { get; private set; }
    public string Fen { get; private set; }

    public int HalfMoveClock { get; private set; }
    public int FullMoveNumber { get; private set; }
    
    public bool WhiteLongCastling { get; private set; }
    public bool WhiteShortCastling { get; private set; }
    public bool BlackLongCastling { get; private set; }
    public bool BlackShortCastling { get; private set; }
    
    public ChessPosition? LastPawnLongMove { get; private set; }

    public Board Move(ChessMove chessMove, bool validateMove = true)
    {
        if (Tie || Winner.HasValue)
        {
            throw new GameEndedException();
        }
        
        if (GetFigure(chessMove.From)?.Color != Turn)
        {
            throw new NotYourMoveException();
        }
        
        var board = new Board(this);
        if (validateMove)
        {
            if (!GetFigure(chessMove.From)!.CanMoveTo(chessMove.To))
            {
                throw new InvalidMoveException();
            }
        }

        board.MoveFigure(chessMove.From, chessMove.To);

        #region PawnEdgeCheck

        if (GetFigure(chessMove.From)!.EnumFigure == EnumFigure.Pawn && chessMove.To.Y % 7 == 0)
        {
            board._figures[chessMove.To.X, chessMove.To.Y] =
                ChessFigureFactory.Create(chessMove.Func(), Turn, chessMove.To, board);
        }

        #endregion

        #region PawnLongEatCheck

        if (GetFigure(chessMove.From)!.EnumFigure == EnumFigure.Pawn && chessMove.To == LastPawnLongMove)
        {
            switch (Turn)
            {
                case ChessColor.White:
                    board._figures[chessMove.To.X, chessMove.To.Y - 1] = null;
                    break;
                case ChessColor.Black:
                    board._figures[chessMove.To.X, chessMove.To.Y + 1] = null;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion

        #region CastlingCheck

        if (GetFigure(chessMove.From)!.EnumFigure == EnumFigure.King &&
            MathF.Abs(chessMove.From.X - chessMove.To.X) > 1.5f)
        {
            if (chessMove.From.X - chessMove.To.X > 0)
            {
                board.MoveFigure(new ChessPosition(0, chessMove.From.Y), new ChessPosition(3, chessMove.From.Y));
            }
            else
            {
                board.MoveFigure(new ChessPosition(7, chessMove.From.Y), new ChessPosition(5, chessMove.From.Y));
            }
        }

        #endregion
        
        #region PawnLongMoveCheck

        board.LastPawnLongMove = null;
        if (GetFigure(chessMove.From)!.EnumFigure == EnumFigure.Pawn &&
            MathF.Abs(chessMove.From.Y - chessMove.To.Y) > 1.5f)
        {
            board.LastPawnLongMove = new ChessPosition(chessMove.From.X, (chessMove.From.Y + chessMove.To.Y) / 2);
        }

        #endregion
        
        #region RooksMoveCheck
        
        if (chessMove.From == new ChessPosition(0, 0) || chessMove.To == new ChessPosition(0, 0))
        {
            board.WhiteLongCastling = false;
        }
        else if (chessMove.From == new ChessPosition(7, 0) || chessMove.To == new ChessPosition(7, 0))
        {
            board.WhiteShortCastling = false;
        }

        else if (chessMove.From == new ChessPosition(0, 7) || chessMove.To == new ChessPosition(0, 7))
        {
            board.BlackLongCastling = false;
        }
        else if (chessMove.From == new ChessPosition(7, 7) || chessMove.To == new ChessPosition(7, 7))
        {
            board.BlackShortCastling = false;
        }
        
        #endregion

        #region KingMoveCheck

        if (GetFigure(chessMove.From)!.EnumFigure == EnumFigure.King)
        {
            if (GetFigure(chessMove.From)!.Color == ChessColor.White)
            {
                board.WhiteLongCastling = false;
                board.WhiteShortCastling = false;
            }
            else
            {
                board.BlackLongCastling = false;
                board.BlackShortCastling = false;
            }
        }

        #endregion

        #region FenThings
        
        board.FullMoveNumber++;
        board.Turn = Turn.Flipped();
        
        if (GetFigure(chessMove.To) is null && GetFigure(chessMove.From)!.EnumFigure != EnumFigure.Pawn)
        {
            board.HalfMoveClock++;
        }
        else
        {
            board.HalfMoveClock = 0;
        }

        board.Fen = GenerateFen(board);
        
        #endregion

        #region ResultsCheck
        
        if (board.IsUnderAttack(board.GetKing(board.Turn).Position, board.Turn.Flipped()) &&
            board.AllFigures.Where(x => x.Color == board.Turn).All(x => !x.GetMoves().Any()))
        {
            board.Winner = board.Turn.Flipped();
        }
        else if (!board.IsUnderAttack(board.GetKing(board.Turn).Position, board.Turn.Flipped()) &&
                 board.AllFigures.Where(x => x.Color == board.Turn).All(x => !x.GetMoves().Any()))
        {
            board.Tie = true;
        }
        else if (board.HalfMoveClock == 100)
        {
            board.Tie = true;
        }
        
        #endregion

        return board;
    }

    public ChessFigure? GetFigure(int x, int y) => _figures[x, y];
    public ChessFigure? GetFigure(ChessPosition position) => GetFigure(position.X, position.Y);

    private void MoveFigure(ChessPosition from, ChessPosition to)
    {
        if (GetFigure(from) is null || GetFigure(to) is not null)
        {
            throw new InvalidMoveException();
        }

        _figures[to.X, to.Y] = _figures[from.X, from.Y];
        _figures[from.X, from.Y] = null;
        GetFigure(to)!.Position = to;
    }
    public bool IsUnderAttack(ChessPosition position, ChessColor color)
    {
        #region Pawn

        if (color == ChessColor.White)
        {
            if (   GetFigure(position + new ChessPosition(-1, -1))?.EnumFigure == EnumFigure.Pawn
                && GetFigure(position + new ChessPosition(-1, -1))?.Color == color
                || GetFigure(position + new ChessPosition(1, -1))?.EnumFigure == EnumFigure.Pawn
                && GetFigure(position + new ChessPosition(1, -1))?.Color == color)
            {
                return true;
            }
        }
        else if (color == ChessColor.Black)
        {
            if (   GetFigure(position + new ChessPosition(-1, 1))?.EnumFigure == EnumFigure.Pawn
                   && GetFigure(position + new ChessPosition(-1, 1))?.Color == color
                   || GetFigure(position + new ChessPosition(1, 1))?.EnumFigure == EnumFigure.Pawn
                   && GetFigure(position + new ChessPosition(1, 1))?.Color == color)
            {
                return true;
            }
        }

        #endregion

        #region Knight

        if (new [] {
                position + new ChessPosition(1, 2),
                position + new ChessPosition(1, -2),
                position + new ChessPosition(-1, 2),
                position + new ChessPosition(-1, -2),

                position + new ChessPosition(2, 1),
                position + new ChessPosition(2, -1),
                position + new ChessPosition(-2, 1),
                position + new ChessPosition(-2, -1)
            }.Where(x => x.IsValid).Any(pos => GetFigure(pos)?.EnumFigure == EnumFigure.Knight && GetFigure(pos)?.Color == color))
        {
            return true;
        }

        #endregion

        #region King

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                var pos = position + new ChessPosition(i, j);
                if (i == 0 && j == 0 || !pos.IsValid)
                {
                    continue;
                }

                if (GetFigure(pos)?.EnumFigure == EnumFigure.King && GetFigure(pos)?.Color == color)
                {
                    return true;
                }
            }
        }

        #endregion

        #region RookQueen

        for (var pos = position + new ChessPosition(1, 0); pos.IsValid; pos += new ChessPosition(1, 0))
        {
            var figure = GetFigure(pos);
            if (figure is null)
            {
                continue;
            }
            if (figure.EnumFigure is EnumFigure.Queen or EnumFigure.Rook && figure.Color == color)
            {
                return true;
            }
            break;
        }
        for (var pos = position + new ChessPosition(-1, 0); pos.IsValid; pos += new ChessPosition(-1, 0))
        {
            var figure = GetFigure(pos);
            if (figure is null)
            {
                continue;
            }
            if (figure.EnumFigure is EnumFigure.Queen or EnumFigure.Rook && figure.Color == color)
            {
                return true;
            }
            break;
        }
        for (var pos = position + new ChessPosition(0, 1); pos.IsValid; pos += new ChessPosition(0, 1))
        {
            var figure = GetFigure(pos);
            if (figure is null)
            {
                continue;
            }
            if (figure.EnumFigure is EnumFigure.Queen or EnumFigure.Rook && figure.Color == color)
            {
                return true;
            }
            break;
        }
        for (var pos = position + new ChessPosition(0, -1); pos.IsValid; pos += new ChessPosition(0, -1))
        {
            var figure = GetFigure(pos);
            if (figure is null)
            {
                continue;
            }
            if (figure.EnumFigure is EnumFigure.Queen or EnumFigure.Rook && figure.Color == color)
            {
                return true;
            }
            break;
        }

        #endregion

        #region BishopQueen

        for (var pos = position + new ChessPosition(1, 1); pos.IsValid; pos += new ChessPosition(1, 1))
        {
            var figure = GetFigure(pos);
            if (figure is null)
            {
                continue;
            }
            if (figure.EnumFigure is EnumFigure.Queen or EnumFigure.Bishop && figure.Color == color)
            {
                return true;
            }
            break;
        }
        for (var pos = position + new ChessPosition(-1, -1); pos.IsValid; pos += new ChessPosition(-1, -1))
        {
            var figure = GetFigure(pos);
            if (figure is null)
            {
                continue;
            }
            if (figure.EnumFigure is EnumFigure.Queen or EnumFigure.Bishop && figure.Color == color)
            {
                return true;
            }
            break;
        }
        for (var pos = position + new ChessPosition(-1, 1); pos.IsValid; pos += new ChessPosition(-1, 1))
        {
            var figure = GetFigure(pos);
            if (figure is null)
            {
                continue;
            }
            if (figure.EnumFigure is EnumFigure.Queen or EnumFigure.Bishop && figure.Color == color)
            {
                return true;
            }
            break;
        }
        for (var pos = position + new ChessPosition(1, -1); pos.IsValid; pos += new ChessPosition(1, -1))
        {
            var figure = GetFigure(pos);
            if (figure is null)
            {
                continue;
            }
            if (figure.EnumFigure is EnumFigure.Queen or EnumFigure.Bishop && figure.Color == color)
            {
                return true;
            }
            break;
        }
        
        #endregion

        return false;
    }
    private static string GenerateFen(Board board)
    {
        var stringBuilder = new StringBuilder();
        for (int y = 7; y >= 0; y--)
        {
            int count = 0;
            for (int x = 0; x < 8; x++)
            {
                if (board.GetFigure(x, y) is null)
                {
                    count++;
                }
                else
                {
                    if (count != 0)
                    {
                        stringBuilder.Append(count);
                        count = 0;
                    }
                    stringBuilder.Append(board.GetFigure(x, y)!.Char);
                }

            }
            if (count != 0)
            {
                stringBuilder.Append(count);
            }
            if (y != 0)
            {
                stringBuilder.Append('/');
            }
        }
        stringBuilder.Append(' ');
        stringBuilder.Append(board.Turn switch
        {
            ChessColor.White => 'w',
            ChessColor.Black => 'b',
            _ => throw new NotSupportedException()
        });
        stringBuilder.Append(' ');

        if (board.WhiteShortCastling)
        {
            stringBuilder.Append('K');
        }
        if (board.WhiteLongCastling)
        {
            stringBuilder.Append('Q');
        }
        if (board.BlackShortCastling)
        {
            stringBuilder.Append('k');
        }
        if (board.BlackLongCastling)
        {
            stringBuilder.Append('q');
        }

        if (!(board.WhiteLongCastling || board.WhiteShortCastling || board.BlackLongCastling || board.BlackShortCastling))
        {
            stringBuilder.Append('-');
        }

        stringBuilder.Append(' ');

        stringBuilder.Append(board.LastPawnLongMove?.ToString() ?? "-");
        
        stringBuilder.Append($" {board.HalfMoveClock} {board.FullMoveNumber}");

        return stringBuilder.ToString();
    }
    public Board(string fen = DefaultFen)
    {
        Fen = fen;
        
        int y = 7;
        int x = 0;
        var tmp = fen.Split();
        for (int i = 0; i < tmp[0].Length; i++)
        {
            if (tmp[0][i] == '/')
            {
                y--;
                x = 0;
                continue;
            }
            EnumFigure figure = Enum.GetValues<EnumFigure>().FirstOrDefault(f => (char)f == char.ToLower(tmp[0][i]));
            if (figure == default)
            {
                x += int.Parse(tmp[0][i].ToString());
            }
            else
            {
                _figures[x, y] = ChessFigureFactory.Create(figure,
                    char.IsLower(tmp[0][i]) ? ChessColor.Black : ChessColor.White, new ChessPosition(x, y), this);
                if (GetFigure(x, y)!.EnumFigure == EnumFigure.King)
                {
                    switch (GetFigure(x, y)!.Color)
                    {
                        case ChessColor.White:
                            WhiteKing = GetFigure(x, y)!;
                            break;
                        case ChessColor.Black:
                            BlackKing = GetFigure(x, y)!;
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                }
                x++;
            }
        }
        Turn = tmp[1][0] switch
        {
            'w' => ChessColor.White,
            'b' => ChessColor.Black,
            _ => throw new NotSupportedException()
        };

        if (tmp[2][0] != '-')
        {
            for (int i = 0; i < tmp[2].Length; i++)
            {
                switch (tmp[2][i])
                {
                    case 'Q':
                        WhiteLongCastling = true;
                        break;

                    case 'q':
                        BlackLongCastling = true;
                        break;

                    case 'K':
                        WhiteShortCastling = true;
                        break;

                    case 'k':
                        BlackShortCastling = true;
                        break;
                }
            }
        }

        if (tmp[3][0] != '-')
        {
            LastPawnLongMove = new ChessPosition(tmp[3]);
        }

        HalfMoveClock = int.Parse(tmp[4]);
        FullMoveNumber = int.Parse(tmp[5]);
    }
    public Board(Board board)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var figure = board.GetFigure(i, j);
                if (figure is not null)
                {
                    _figures[i, j] = ChessFigureFactory.Create(figure.EnumFigure, figure.Color, figure.Position, this);
                    if (figure.EnumFigure == EnumFigure.King)
                    {
                        switch (figure.Color)
                        {
                            case ChessColor.White:
                                WhiteKing = figure;
                                break;
                            case ChessColor.Black:
                                BlackKing = figure;
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                    }
                }
            }
        }

        Turn = board.Turn;
        Fen = board.Fen;

        HalfMoveClock = board.HalfMoveClock;
        FullMoveNumber = board.FullMoveNumber;

        WhiteLongCastling = board.WhiteLongCastling;
        WhiteShortCastling = board.WhiteShortCastling;
        BlackLongCastling = board.BlackLongCastling;
        BlackShortCastling = board.BlackShortCastling;

        LastPawnLongMove = board.LastPawnLongMove;
    }

    protected bool Equals(Board other)
    {
        return Fen == other.Fen;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Board)obj);
    }

    public override int GetHashCode()
    {
        return Fen.GetHashCode();
    }

    public static bool operator ==(Board? left, Board? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Board? left, Board? right)
    {
        return !Equals(left, right);
    }
}