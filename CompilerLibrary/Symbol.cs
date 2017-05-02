namespace CompilerLibrary
{
    public class Symbol
    {
        public readonly int ColCount;
        public readonly int RowCount;
        public readonly char Character;

        public Symbol(char character, int rowCount, int colCount)
        {
            this.Character = character;
            this.RowCount = rowCount;
            this.ColCount = colCount;
        }
    }
}