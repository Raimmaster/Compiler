namespace CompilerLibrary
{
    public class Token
    {
        public TokenType type;
        private int _column;
        private int _row;
        private string lexema;
        
        public Token(TokenType type, string lexema, int row, int column)
        {
            this.type = type;
            this.lexema = lexema;
            this._row = row;
            this._column = column;
        }

        public override string ToString()
        {
            return lexema + " of type " + type;
        }
    }
}