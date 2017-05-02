using System;
using System.Runtime.Serialization;

namespace CompilerLibrary
{
    [Serializable]
    internal class LexicalException : Exception
    {
        public LexicalException()
        {
        }

        public LexicalException(string message) : base(message)
        {
        }

        public LexicalException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LexicalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}