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
            if (
                token.type == TokenType.ID ||
                token.type == TokenType.PRINT_CALL ||
                token.type == TokenType.READ_CALL ||
                token.type == TokenType.STRUCT_KW ||
                token.type == TokenType.DECL_KW
            )
            {
                var sentencia = Sentencia();
                var listaSentencias = ListaSentencias();

                listaSentencias.Insert(0, sentencia);

                return listaSentencias;
            }
            else
            {
                return new List<StatementNode>();
            }
        }

        //sentencia -> print id
        //          | 
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
            else if(token.type == TokenType.DECL_KW)
            {
                return Declarar();
            }else if (token.type == TokenType.STRUCT_KW)
            {
                return StructDeclaration();
            }
            else
            {
                throw new SyntaxErrorException("Sentencia expected on row " +
                    token.row + " and column " + token.column);
            }
        }

        private StatementNode StructDeclaration()
        {
            if(token.type != TokenType.STRUCT_KW)
            {
                throw new SyntaxErrorException("struct keyword expected on row " +
                    token.row + " and column " + token.column);
            }
            GetNextToken();
            if(token.type != TokenType.ID)
            {
                throw new SyntaxErrorException("id expected on row " +
                    token.row + " and column " + token.column);
            }
            var id = token;
            GetNextToken();
            var attributeList = StructAttributeList(new List<DeclarationStatement>());
            if(token.type != TokenType.END_KW)
            {
                throw new SyntaxErrorException("end keyword expected on row " +
                    token.row + " and column " + token.column);
            }
            GetNextToken();
            return new StructNode(id, attributeList);
        }

        private List<DeclarationStatement> StructAttributeList(List<DeclarationStatement> declarations)
        {
            if(token.type == TokenType.DECL_KW)
            {
                declarations.Add((DeclarationStatement)Declarar());
                return StructAttributeList(declarations);
            }else
            {
                return declarations;
            }
        }

        private StatementNode Declarar()
        {
            if(token.type != TokenType.DECL_KW)
            {
                throw new SyntaxErrorException("decl keyword expected on row " + token.row + 
                    " and column " + token.column);
            }
            GetNextToken();
            if(
                token.type != TokenType.ID && 
                token.type != TokenType.BOOL_KW &&
                token.type != TokenType.INT_KW
              )
            {
                throw new SyntaxErrorException("type identifier expected on row " + token.row +
                    " and column " + token.column);
            }
            var varType = new VarTypeNode(token.lexema);
            GetNextToken();
            if(token.type != TokenType.ID)
            {
                throw new SyntaxErrorException("identifier expected on row " + token.row +
                    " and column " + token.column);
            }
            var varID = new  IDNode(token.lexema);
            GetNextToken();
            var rankSpecifier = OptionalRankSpecifier(new List<int>());
            if(token.type != TokenType.END_STATEMENT)
            {
                throw new SyntaxErrorException("; expected on row " + token.row +
                    " and column " + token.column);
            }
            GetNextToken();
            return new DeclarationStatement(varType, varID, rankSpecifier);
        }

        private List<int> OptionalRankSpecifier(List<int> numberList)
        {
            if(token.type == TokenType.BRACKET_OPEN)
            {
                GetNextToken();
                if(token.type != TokenType.LIT_INT)
                {
                    throw new SyntaxErrorException("int size expected on row " + token.row +
                    " and column " + token.column);
                }
                numberList.Add(int.Parse(token.lexema));
                GetNextToken();
                if(token.type != TokenType.BRACKET_CLOSE)
                {
                    throw new SyntaxErrorException("] expected on row " + token.row +
                    " and column " + token.column);
                }
                GetNextToken();
                return OptionalRankSpecifier(numberList);
            }else
            {
                return numberList;
            }
        }

        private void GetNextToken()
        {
            token = lexer.GetNextToken();
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
            
            return new ReadNode(new IDNode(idLexema));
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
            var id = ID();
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
            return new AssignNode(id, eValor);
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
            }else if (token.type == TokenType.LIT_BOOL)
            {
                var valor = new BoolNode(bool.Parse(token.lexema));
                token = lexer.GetNextToken();
                return valor;
            }else if (token.type == TokenType.ID)
            {
                return ID();
            }
            else
            {
                throw new ParserException("Expected a factor.");
            }
        }

        private IDNode ID()
        {
            if(token.type != TokenType.ID)
            {
                throw new SyntaxErrorException("id expected on row " + token.row
                    + " and column " + token.column);
            }
            var id = token.lexema;
            GetNextToken();
            List<AttributeNode> attributeList = OptionalAttributeList();

            return new IDNode(id, attributeList);
        }

        private List<AttributeNode> OptionalAttributeList()
        {
            if(token.type == TokenType.DOT_OPERATOR)
            {
                GetNextToken();
                if(token.type != TokenType.ID)
                {
                    throw new SyntaxErrorException("id expected on row " + token.row
                        + " and column " + token.column);
                }
                var id = token.lexema;
                var attributeNode = new FieldNode(id);
                GetNextToken();
                var attributeList = OptionalAttributeList();
                attributeList.Insert(0, attributeNode);
                return attributeList;
            }else if (token.type == TokenType.BRACKET_OPEN)
            {
                GetNextToken();
                var value = E();
                if(token.type != TokenType.BRACKET_CLOSE)
                {
                    throw new SyntaxErrorException("] expected on row " + token.row
                        + " and column " + token.column);
                }
                GetNextToken();
                var attributeList = OptionalAttributeList();
                var indexArrayNode = new IndexArrayNode(value);
                attributeList.Insert(0, indexArrayNode);
                return attributeList;
            }
            else
            {
                return new List<AttributeNode>();
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
