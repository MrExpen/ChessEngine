using System.Runtime.Serialization;

namespace ChessLib.Data.Exceptions;

[Serializable]
public class NotYourMoveException : Exception
{
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //

    public NotYourMoveException()
    {
    }

    public NotYourMoveException(string message) : base(message)
    {
    }

    public NotYourMoveException(string message, Exception inner) : base(message, inner)
    {
    }

    protected NotYourMoveException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}