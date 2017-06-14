namespace Compiler
{
    public class VarTypeNode : Types
    {
        private string lexema;

        public VarTypeNode()
        {
            
        }

        public VarTypeNode(string lexema)
        {
            this.lexema = lexema;
        }

        public Types GetVarType()
        {
            if(SymbolsTable.types.ContainsKey(lexema))
            {
                return SymbolsTable.types[lexema];
            }else
            {
                throw new SemanticException("Type does not exist!");
            }
        }
    }
}