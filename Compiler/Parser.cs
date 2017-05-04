namespace Compiler
{
    public class Parser
    {
        private Lexer lexer;
        private Token token;

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
            this.token = lexer.GetNextToken();
        }

        public void Parse()
        {
            E();
            if (token.type != TokenType.EOF)
            {
                throw new ParserException("End of file expected.");
            }

        }

        private void E()
        {
            T();
            EPrime();
        }

        private void EPrime()
        {
            if (token.type == TokenType.OP_SUM)
            {
                token = lexer.GetNextToken();
                T();
                EPrime();
            }else if (token.type == TokenType.OP_SUBSTRACT)
            {
                token = lexer.GetNextToken();
                T();
                EPrime();
            }
            else
            {
                //TODO: Epsilon case
            }
        }

        private void T()
        {
            F();
            TPrime();
        }

        private void F()
        {
            if (token.type == TokenType.PAREN_OPEN)
            {
                token = lexer.GetNextToken();
                E();
                if (token.type != TokenType.PAREN_CLOSE)
                {
                    throw new ParserException("Expected closing parenthesis.");
                }
                token = lexer.GetNextToken();
            }else if (token.type == TokenType.LIT_INT)
            {
                token = lexer.GetNextToken();
            }else if (token.type == TokenType.ID)
            {
                token = lexer.GetNextToken();
            }
            else
            {
                throw new ParserException("Expected a factor.");
            }
        }

        private void TPrime()
        {
            if (token.type == TokenType.OP_MULTIPLICATION)
            {
                token = lexer.GetNextToken();
                F();
                TPrime();
            }else if (token.type == TokenType.OP_DIVISION)
            {
                token = lexer.GetNextToken();
                F();
                TPrime();
            }
            else
            {
                //TODO: Epsilon Case
            }
        }
    }
}
