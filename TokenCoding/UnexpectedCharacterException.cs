using System;

namespace TokenCoding
{
    public class UnexpectedCharacterException : Exception
    {
        public UnexpectedCharacterException(char character) : base($"Unexpected {character}.") { }
    }
}
