using System.Runtime.Serialization;

namespace GameServer
{
    [Serializable]
    internal class YourConflictException : Exception
    {
        public YourConflictException()
        {
        }

        public YourConflictException(string? message) : base(message)
        {
        }

        public YourConflictException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected YourConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}