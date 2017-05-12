using System;
using System.Collections.Generic;
using System.Data;

namespace Compiler
{
    public class Parser
    {
        private Lexer lexer;
        private Token token;
        private Dictionary<string, float> vars;

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
            this.token = lexer.GetNextToken();
            vars = new Dictionary<string, float>();
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
            string idLexema = token.lexema;
            token = lexer.GetNextToken();
            if (token.type != TokenType.END_STATEMENT)
            {
                throw new SyntaxErrorException("; operand expected on row " +
                    token.row + " and column " + token.column);
            }
            token = lexer.GetNextToken();
            string inputText = Console.ReadLine();
            this.vars[idLexema] = float.Parse(inputText);
        }

        private void Imprimir()
        {
            if (token.type != TokenType.PRINT_CALL)
            {
                throw new SyntaxErrorException("PRINT token type expected on row " + 
                    token.row + " and column " + token.column);
            }
            token = lexer.GetNextToken();
            float eValor = E();
            if (token.type != TokenType.END_STATEMENT)
            {
                throw new SyntaxErrorException("; operand expected on row " +
                    token.row + " and column " + token.column);
            }
            token = lexer.GetNextToken();
            Console.Out.WriteLine(eValor);
        }

        private void Asignar()
        {   
            if (token.type != TokenType.ID)
            {
                throw new SyntaxErrorException("ID Token Type expected!");
            }
            string idLexema = token.lexema;
            token = lexer.GetNextToken();
            if (token.type != TokenType.OP_ASSIGN)
            {
                throw new SyntaxErrorException("= operand expected!");
            }
            token = lexer.GetNextToken();
            float eValor = E();
            
            if (token.type != TokenType.END_STATEMENT)
            {
                throw new SyntaxErrorException("; operand expected");
            }
            token = lexer.GetNextToken();

            this.vars[idLexema] = eValor;
        }

        private float E()
        {
            float tValor = T();
            float ePrimaValor = EPrime(tValor);

            return ePrimaValor;
        }

        private float EPrime(float param)
        {
            if (token.type == TokenType.OP_SUM)
            {
                token = lexer.GetNextToken();
                float tValor = T();
                float ePrima1Valor = EPrime(param + tValor);

                return ePrima1Valor;
            }
            else if (token.type == TokenType.OP_SUBSTRACT)
            {
                token = lexer.GetNextToken();
                float tValor = T();
                float ePrima1Valor = EPrime(param - tValor);

                return ePrima1Valor;
            }
            else
            {
                return param;
            }
        }

        private float T()
        {
            float fValor = F();
            float tValor = TPrime(fValor);
            return tValor;
        }

        private float F()
        {
            if (token.type == TokenType.PAREN_OPEN)
            {
                token = lexer.GetNextToken();
                float eValor = E();
                if (token.type != TokenType.PAREN_CLOSE)
                {
                    throw new ParserException("Expected closing parenthesis.");
                }
                token = lexer.GetNextToken();
                return eValor;
            }else if (token.type == TokenType.LIT_INT)
            {
                float valor = float.Parse(token.lexema);
                token = lexer.GetNextToken();
                return valor;
            }else if (token.type == TokenType.ID)
            {
                string idLexema = token.lexema;
                float valor = this.vars[idLexema];
                token = lexer.GetNextToken();

                return valor;
            }
            else
            {
                throw new ParserException("Expected a factor.");
            }
        }

        private float TPrime(float param)
        {
            if (token.type == TokenType.OP_MULTIPLICATION)
            {
                token = lexer.GetNextToken();
                float fValor = F();
                float tPrima1Valor = TPrime(param * fValor);

                return tPrima1Valor;
            }
            else if (token.type == TokenType.OP_DIVISION)
            {
                token = lexer.GetNextToken();
                float fValor = F();
                float tPrima1Valor = TPrime(param / fValor);

                return tPrima1Valor;
            }
            else
            {
                return param;
            }
        }
    }
}
