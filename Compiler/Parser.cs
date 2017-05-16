using System;
using System.Collections.Generic;

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

        public List<StatementNode> Parse()
        {
            var code = Codigo();
            if (token.type != TokenType.EOF)
            {
                throw new ParserException("End of file expected.");
            }

            return code;
        }

        private List<StatementNode> Codigo()
        {
            return ListaSentencias();
        }

        private List<StatementNode> ListaSentencias()
        {
            var statementNodesArray = new List<StatementNode>();
            if (
                token.type == TokenType.ID ||
                token.type == TokenType.PRINT_CALL ||
                token.type == TokenType.READ_CALL
            )
            {
                var sentencia = Sentencia();
                var listaSentencias = ListaSentencias();

                listaSentencias.Insert(0, sentencia);

                return listaSentencias;
            }
            else
            {
                return statementNodesArray;
            }
        }

        private StatementNode Sentencia()
        {
            if (token.type == TokenType.ID)
            {
                return Asignar();
            }
            else if (token.type == TokenType.PRINT_CALL)
            {
                return Imprimir();
            }
            else if (token.type == TokenType.READ_CALL)
            {
                return Leer();
            }
            else
            {
                throw new SyntaxErrorException("Sentencia expected on row " +
                    token.row + " and column " + token.column);
            }
        }

        private StatementNode Leer()
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
            //string inputText = Console.ReadLine();
            string inputText = "7";
            return new ReadNode(new IDNode(idLexema), new NumNode(float.Parse(inputText)));
        }

        private StatementNode Imprimir()
        {
            if (token.type != TokenType.PRINT_CALL)
            {
                throw new SyntaxErrorException("PRINT token type expected on row " + 
                    token.row + " and column " + token.column);
            }
            token = lexer.GetNextToken();
            var eValor = E();
            if (token.type != TokenType.END_STATEMENT)
            {
                throw new SyntaxErrorException("; operand expected on row " +
                    token.row + " and column " + token.column);
            }
            token = lexer.GetNextToken();
            return new PrintNode(eValor);
        }

        private StatementNode Asignar()
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
            var eValor = E();
            
            if (token.type != TokenType.END_STATEMENT)
            {
                throw new SyntaxErrorException("; operand expected");
            }
            token = lexer.GetNextToken();
            return new AssignNode(new IDNode(idLexema), eValor);
            //this.vars[idLexema] = eValor;
        }

        private ExpressionNode E()
        {
            var tValor = T();
            var ePrimaValor = EPrime(tValor);

            return ePrimaValor;
        }

        private ExpressionNode EPrime(ExpressionNode param)
        {
            if (token.type == TokenType.OP_SUM)
            {
                token = lexer.GetNextToken();
                var tValor = T();
                var ePrima1Valor = EPrime(new SumNode(param, tValor));

                return ePrima1Valor;
            }
            else if (token.type == TokenType.OP_SUBSTRACT)
            {
                token = lexer.GetNextToken();
                var tValor = T();
                var ePrima1Valor = EPrime(new SubNode(param, tValor));

                return ePrima1Valor;
            }
            else
            {
                return param;
            }
        }

        private ExpressionNode T()
        {
            var fValor = G();
            var tValor = TPrime(fValor);
            return tValor;
        }

        private ExpressionNode G()
        {
            var fValor = F();
            var gValor = GPrime(fValor);

            return gValor;
        }

        private ExpressionNode GPrime(ExpressionNode param)
        {
            if(token.type == TokenType.OP_EXPONENT)
            {
                token = lexer.GetNextToken();
                var fValor = F();
                var gPrima1Valor = GPrime(new ExpNode(param, fValor));

                return gPrima1Valor;
            }
            else
            {
                return param;
            }
        }

        private ExpressionNode F()
        {
            if (token.type == TokenType.PAREN_OPEN)
            {
                token = lexer.GetNextToken();
                var eValor = E();
                if (token.type != TokenType.PAREN_CLOSE)
                {
                    throw new ParserException("Expected closing parenthesis.");
                }
                token = lexer.GetNextToken();
                return eValor;
            }else if (token.type == TokenType.LIT_INT)
            {
                var valor = new NumNode(float.Parse(token.lexema));
                token = lexer.GetNextToken();
                return valor;
            }else if (token.type == TokenType.ID)
            {
                string idLexema = token.lexema;
                var valor = new IDNode(idLexema);
                token = lexer.GetNextToken();

                return valor;
            }
            else
            {
                throw new ParserException("Expected a factor.");
            }
        }

        private ExpressionNode TPrime(ExpressionNode param)
        {
            if (token.type == TokenType.OP_MULTIPLICATION)
            {
                token = lexer.GetNextToken();
                var fValor = G();
                var tPrima1Valor = TPrime(new MulNode(param, fValor));

                return tPrima1Valor;
            }
            else if (token.type == TokenType.OP_DIVISION)
            {
                token = lexer.GetNextToken();
                var fValor = G();
                var tPrima1Valor = TPrime(new DivNode(param, fValor));

                return tPrima1Valor;
            }
            else
            {
                return param;
            }
        }
    }
}
