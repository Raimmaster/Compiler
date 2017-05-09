using System.Data;

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
            Codigo();
            if (token.type != TokenType.EOF)
            {
                throw new ParserException("End of file expected.");
            }
        }

        private void Codigo()
        {
            ListaSentencias();
        }

        private void ListaSentencias()
        {
            if (
                token.type == TokenType.ID ||
                token.type == TokenType.PRINT_CALL ||
                token.type == TokenType.READ_CALL
            )
            {
                Sentencia();
                ListaSentencias();
            }
            else
            {
                //TODO: Epsilon case 
            }
        }

        private void Sentencia()
        {
            if (token.type == TokenType.ID)
            {
                Asignar();
            }
            else if (token.type == TokenType.PRINT_CALL)
            {
                Imprimir();
            }
            else if (token.type == TokenType.READ_CALL)
            {
                Leer();
            }
            else
            {
                throw new SyntaxErrorException("Sentencia expected on row " +
                    token.row + " and column " + token.column);
            }
        }

        private void Leer()
        {
            if (token.type != TokenType.READ_CALL)
            {
                throw new SyntaxErrorException("read token type expected on row " +
                    token.row + " and column " + token.column);
            }
            token = lexer.GetNextToken();
            if (token.type != TokenType.ID)
            {
                throw new SyntaxErrorException("ID token type expected on row " +
                    token.row + " and column " + token.column);
            }
            token = lexer.GetNextToken();
            if (token.type != TokenType.END_STATEMENT)
            {
                throw new SyntaxErrorException("; operand expected on row " +
                    token.row + " and column " + token.column);
            }
            token = lexer.GetNextToken();
        }

        private void Imprimir()
        {
            if (token.type != TokenType.PRINT_CALL)
            {
                throw new SyntaxErrorException("PRINT token type expected on row " + 
                    token.row + " and column " + token.column);
            }
            token = lexer.GetNextToken();
            E();
            if (token.type != TokenType.END_STATEMENT)
            {
                throw new SyntaxErrorException("; operand expected on row " +
                    token.row + " and column " + token.column);
            }
            token = lexer.GetNextToken();
        }

        private void Asignar()
        {
            if (token.type != TokenType.ID)
            {
                throw new SyntaxErrorException("ID Token Type expected!");
            }
            token = lexer.GetNextToken();
            if (token.type != TokenType.OP_ASSIGN)
            {
                throw new SyntaxErrorException("= operand expected!");
            }
            token = lexer.GetNextToken();
            E();
            
            if (token.type != TokenType.END_STATEMENT)
            {
                throw new SyntaxErrorException("; operand expected");
            }
            token = lexer.GetNextToken();
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
