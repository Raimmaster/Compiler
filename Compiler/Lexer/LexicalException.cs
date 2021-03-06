﻿using System;
namespace Compiler
{
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
    }
}