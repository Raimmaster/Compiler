namespace CompilerLibrary
{
    public class InputString : IInput
    {
        public string InitialInput { get; set; }

        public InputString(string input)
        {
            this.InitialInput = input;
            this.RowCount = 1;
            this.ColCount = 1;
            this.CurrentChar = 0;
        }

        public int CurrentChar { get; set; }

        public int ColCount { get; set; }

        public int RowCount { get; set; }

        public Symbol GetNextSymbol()
        {
            if (CurrentChar < InitialInput.Length)
            {
                if (InitialInput[CurrentChar] == '\n')
                {
                    ++RowCount;
                    ColCount = 1;
                }

                var returnSymbol = new Symbol(
                    InitialInput[CurrentChar++],
                    RowCount,
                    ColCount++);

                return returnSymbol;
            }

            return new Symbol('\0', RowCount, ColCount);
        }
    }
}