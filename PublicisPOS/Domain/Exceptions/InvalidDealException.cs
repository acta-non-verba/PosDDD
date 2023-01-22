using System.Runtime.Serialization;

namespace PublicisPOS.Domain.Exceptions
{
    [Serializable]
    public class InvalidDealException : Exception
    {
        public InvalidDealException()
        {
        }

        public InvalidDealException(string? message) : base(message)
        {
        }

        public InvalidDealException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidDealException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}