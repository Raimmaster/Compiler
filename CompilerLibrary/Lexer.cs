using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CompilerLibrary
{
    public class Lexer
    {
        private readonly InputString _inputString;
        private Symbol _currentSymbol;
        private Dictionary<string, TokenType> _reservedWordsDict;
        private List<string> _validEscapeCharactersList;

        public Lexer(InputString inputString)
        {
            this._inputString = inputString;
            this._currentSymbol = inputString.GetNextSymbol();
            InitReservedWordsDictionary();
            InitValidEscapeCharacters();
        }

        private void InitValidEscapeCharacters()
        {
            _validEscapeCharactersList = new List<string>();
            _validEscapeCharactersList.Add("\a");
            _validEscapeCharactersList.Add("\b");
            _validEscapeCharactersList.Add("\f");
            _validEscapeCharactersList.Add("\n");
            _validEscapeCharactersList.Add("\r");
            _validEscapeCharactersList.Add("\t");
            _validEscapeCharactersList.Add("\v");
            _validEscapeCharactersList.Add("\'");
            _validEscapeCharactersList.Add("\"");
            _validEscapeCharactersList.Add("\\");
            
        }

        private void InitReservedWordsDictionary()
        {
            _reservedWordsDict = new Dictionary<string, TokenType>();
            _reservedWordsDict["int"] = TokenType.INT_KEYWORD;
            _reservedWordsDict["float"] = TokenType.FLOAT_KEYWORD;
            _reservedWordsDict["char"] = TokenType.CHAR_KEYWORD;
            _reservedWordsDict["bool"] = TokenType.BOOL_KEYWORD;
            _reservedWordsDict["string"] = TokenType.STRING_KEYWORD;
            _reservedWordsDict["as"] = TokenType.AS_KEYWORD;
            _reservedWordsDict["is"] = TokenType.IS_KEYWORD;
            _reservedWordsDict["new"] = TokenType.NEW_INSTANCE_KEYWORD;
            _reservedWordsDict["if"] = TokenType.IF_CONDITIONAL_KEYWORD;
            _reservedWordsDict["for"] = TokenType.FOR_LOOP;
            _reservedWordsDict["while"] = TokenType.WHILE_LOOP;
            _reservedWordsDict["true"] = TokenType.TRUE_KEYWORD;
            _reservedWordsDict["false"] = TokenType.FALSE_KEYWORD;
        }

        public Token GetNextToken()
        {
            while (Char.IsWhiteSpace(_currentSymbol.Character))
            {
                _currentSymbol = _inputString.GetNextSymbol();
            }

            if (_currentSymbol.Character == '/')
            {
                string placeholderLexema = _currentSymbol.Character.ToString();

                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '/')
                {
                    do
                       {
                        _currentSymbol = _inputString.GetNextSymbol();
                    } while (_currentSymbol.Character != '\n' || _currentSymbol.Character == '\0');
                }else
                {
                    var tokenType = TokenType.OP_DIVISION;

                    var lexemaRow = _currentSymbol.RowCount;
                    var lexemaColumn = _currentSymbol.ColCount;

                    if (_currentSymbol.Character == '=')
                    {
                        placeholderLexema += _currentSymbol;
                        _currentSymbol = _inputString.GetNextSymbol();
                        tokenType = TokenType.OP_DIVISION_AND_ASSIGN;
                    }
                    return new Token(
                        TokenType.OP_DIVISION_AND_ASSIGN, 
                        placeholderLexema, 
                        lexemaRow,
                        lexemaColumn
                        );
                }
            }

            if (Char.IsLetter(_currentSymbol.Character))
            {
                var lexema = new StringBuilder();
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaCol = _currentSymbol.ColCount;
                do
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                } while (Char.IsLetter(_currentSymbol.Character));

                var tokenType = _reservedWordsDict.ContainsKey(lexema.ToString()) ? 
                    _reservedWordsDict[lexema.ToString()] : TokenType.ID;
                
                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaCol
                    );
            }
            else if (_currentSymbol.Character == '\'')
            {
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaCol = _currentSymbol.ColCount;

                var lexema = new StringBuilder(_currentSymbol.Character.ToString());
                do
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();

                    if (_currentSymbol.Character == '\\')
                    {
                        
                    }
                } while (_currentSymbol.Character != '\'');

                if (lexema.Length > 3)
                {
                    throw new LexicalException("A character cannot surpass the length of one.");
                }

                return new Token(
                    TokenType.LIT_CHAR, 
                    lexema.ToString(),
                    lexemaRow,
                    lexemaCol
                    );
            }
            else if (_currentSymbol.Character == '+')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.OP_SUM;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_SUM_AND_ASSIGN;
                }
                else if (_currentSymbol.Character == '+')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_INCREMENT;
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '?')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.OP_TERNARY;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '?')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_NULL_COALESCING;
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '-')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.OP_SUBSTRACT;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_SUBSTRACT_AND_ASSIGN;
                }
                else if (_currentSymbol.Character == '-')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_DECREMENT;
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '%')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.OP_SUM;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_MODULO_AND_ASSIGN;
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '*')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.OP_MULTIPLICATION;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_MULTIPLICATION_AND_ASSIGN;
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '=')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.OP_ASSIGN;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_EQUALITY;
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '(')
            {
                var lexema = _currentSymbol.Character.ToString();
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                _currentSymbol = _inputString.GetNextSymbol();

                return new Token(
                    TokenType.PAREN_OPEN,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == ')')
            {
                var lexema = _currentSymbol.Character.ToString();
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                _currentSymbol = _inputString.GetNextSymbol();

                return new Token(
                    TokenType.PAREN_CLOSE,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '[')
            {
                var lexema = _currentSymbol.Character.ToString();
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                _currentSymbol = _inputString.GetNextSymbol();

                return new Token(
                    TokenType.BRACKET_OPEN,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == ']')
            {
                var lexema = _currentSymbol.Character.ToString();
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                _currentSymbol = _inputString.GetNextSymbol();

                return new Token(
                    TokenType.BRACKET_CLOSE,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '{')
            {
                var lexema = _currentSymbol.Character.ToString();
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                _currentSymbol = _inputString.GetNextSymbol();

                return new Token(
                    TokenType.BRACES_OPEN,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '}')
            {
                var lexema = _currentSymbol.Character.ToString();
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                _currentSymbol = _inputString.GetNextSymbol();

                return new Token(
                    TokenType.BRACES_CLOSE,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == ':')
            {
                var lexema = _currentSymbol.Character.ToString();
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                _currentSymbol = _inputString.GetNextSymbol();

                return new Token(
                    TokenType.COLON_OPERATOR,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '.')
            {
                var lexema = _currentSymbol.Character.ToString();
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                _currentSymbol = _inputString.GetNextSymbol();

                return new Token(
                    TokenType.ATTRIBUTE_OPERATOR,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == ';')
            {
                var lexema = _currentSymbol.Character.ToString();
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                _currentSymbol = _inputString.GetNextSymbol();

                return new Token(
                    TokenType.END_STATEMENT,
                    lexema,
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '|')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.OP_ASSIGN;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_BITWISE_OR_AND_ASSIGN;
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '&')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.OP_ASSIGN;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_BITWISE_AND_AND_ASSIGN;
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '^')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.OP_ASSIGN;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_XOR_AND_ASSIGN;
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '<')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.LESS_THAN_OPERATOR;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.LESS_THAN_OR_EQUALS;
                }
                else if (_currentSymbol.Character == '<')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_BIT_LEFT_SHIFT;

                    if (_currentSymbol.Character == '=')
                    {
                        lexema.Append(_currentSymbol.Character);
                        _currentSymbol = _inputString.GetNextSymbol();
                        tokenType = TokenType.OP_BIT_LEFT_SHIFT_AND_ASSIGN;
                    }
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '>')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.GREATER_THAN_OPERATOR;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.GREATER_THAN_OR_EQUALS;
                }
                else if (_currentSymbol.Character == '>')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_BIT_RIGHT_SHIFT;

                    if (_currentSymbol.Character == '=')
                    {
                        lexema.Append(_currentSymbol.Character);
                        _currentSymbol = _inputString.GetNextSymbol();
                        tokenType = TokenType.OP_BIT_RIGHT_SHIFT_AND_ASSIGN;
                    }
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '!')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.NEGATION_OPERATOR;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_NEGATION_EQUALITY;
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '~')
            {
                var lexema = new StringBuilder(_currentSymbol.Character);
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;
                var tokenType = TokenType.UNARY_OPERATOR;
                _currentSymbol = _inputString.GetNextSymbol();

                if (_currentSymbol.Character == '=')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    tokenType = TokenType.OP_BITWISE_UNARY_AND_ASSIGN;
                }

                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (Char.IsDigit(_currentSymbol.Character))
            {
                var lexema = new StringBuilder();
                var lexemaRow = _currentSymbol.RowCount;
                var lexemaColumn = _currentSymbol.ColCount;

                if (_currentSymbol.Character == '0')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();

                    if (_currentSymbol.Character == 'x')
                    {
                        lexema.Append(_currentSymbol.Character);
                        _currentSymbol = _inputString.GetNextSymbol();
                        
                        //65-70 A-F; 97-102 a-f
                        //While we get A-F, case not matters, consume
                        while (
                            (_currentSymbol.Character >= 48
                            && _currentSymbol.Character <= 57) ||
                            (_currentSymbol.Character >= 65
                             && _currentSymbol.Character <= 70) ||
                            (_currentSymbol.Character >= 97
                             && _currentSymbol.Character <= 102)
                        )
                        {
                            lexema.Append(_currentSymbol.Character);
                            _currentSymbol = _inputString.GetNextSymbol();
                        }
                        
                        return new Token(
                        TokenType.LIT_HEX_INT,
                        lexema.ToString(),
                        lexemaRow,
                        lexemaColumn
                        );
                    }else if (_currentSymbol.Character == 'b')
                    {
                        lexema.Append(_currentSymbol.Character);
                        _currentSymbol = _inputString.GetNextSymbol();
                        while (_currentSymbol.Character == '0' || _currentSymbol.Character == '1')
                        {
                            lexema.Append(_currentSymbol.Character);
                            _currentSymbol = _inputString.GetNextSymbol();
                        }

                        return new Token(
                            TokenType.LIT_BIN_INT,
                            lexema.ToString(),
                            lexemaRow,
                            lexemaColumn
                            );
                    }
                }

                ConsumeDigits(lexema);

                var tokenType = TokenType.LIT_DECIMAL_INT;
                if (_currentSymbol.Character == '.')
                {
                    lexema.Append(_currentSymbol.Character);
                    _currentSymbol = _inputString.GetNextSymbol();
                    ConsumeDigits(lexema);
                    if (_currentSymbol.Character == 'f' || _currentSymbol.Character == 'F')
                    {
                        lexema.Append(_currentSymbol.Character);
                        _currentSymbol = _inputString.GetNextSymbol();
                        tokenType = TokenType.LIT_FLOAT;
                    }
                    else
                    {
                        throw new LexicalException("Float type must end with an f or F.");
                    }
                }
                
                return new Token(
                    tokenType,
                    lexema.ToString(),
                    lexemaRow,
                    lexemaColumn
                    );
            }
            else if (_currentSymbol.Character == '\0')
            {
                return new Token(
                    TokenType.EOF,
                    "",
                    _currentSymbol.RowCount,
                    _currentSymbol.ColCount
                    );
            }
            else
            {
                throw new LexicalException("Symbol not supported.");
            }
        }

        private void ConsumeDigits(StringBuilder lexema)
        {
            while (Char.IsDigit(_currentSymbol.Character))
            {
                lexema.Append(_currentSymbol.Character);
                _currentSymbol = _inputString.GetNextSymbol();
            }
        }
    }
}