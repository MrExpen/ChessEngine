namespace ChessLib.Data.Enums;

public enum ChessColor
{
    None,
    White,
    Black
}

public static class ColorExtensions
{
    public static ChessColor Flipped(this ChessColor color)
    {
        return color switch
        {
            ChessColor.White => ChessColor.Black,
            ChessColor.Black => ChessColor.White,
            ChessColor.None => throw new NotSupportedException(),
            
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    }
}