using System;

namespace TokenCoding
{
    [Serializable]
    public class CoderException : Exception
    {
        public CoderException() { }
        public CoderException(string message) : base(message) { }
        public CoderException(string message, Exception inner) : base(message, inner) { }
        protected CoderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}