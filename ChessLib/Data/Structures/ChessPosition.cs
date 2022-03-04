namespace ChessLib.Data.Structures;

public readonly struct ChessPosition
{
    public readonly int X;
    public readonly int Y;

    public ChessPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public ChessPosition(string cell)
    {
        if (cell.Length != 2)
        {
            throw new ArgumentException();
        }
        if (!(cell[0] >= 'a' && cell[0] <= 'h'))
        {
            throw new ArgumentException();
        }
        if (!(cell[1] >= '1' && cell[1] <= '8'))
        {
            throw new ArgumentException();
        }
        X = cell[0] - 'a';
        Y = cell[1] - '1';
    }

    public bool IsValid => X is >= 0 and < 8 && Y is >= 0 and < 8;


    public override string ToString()
    {
        return $"{(char)(X + 'a')}{(char)(Y + '1')}";
    }

    public bool Equals(ChessPosition other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is ChessPosition other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(ChessPosition left, ChessPosition right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ChessPosition left, ChessPosition right)
    {
        return !left.Equals(right);
    }

    public static ChessPosition operator +(ChessPosition left, ChessPosition right)
    {
        return new ChessPosition(left.X + right.X, left.Y + right.Y);
    }
    
    public static ChessPosition operator -(ChessPosition left, ChessPosition right)
    {
        return new ChessPosition(left.X - right.X, left.Y - right.Y);
    }
}