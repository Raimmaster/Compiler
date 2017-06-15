namespace Compiler
{
    public class VarTypeNode : Types
    {
        public string typeName;

        public VarTypeNode()
        {
            
        }

        public VarTypeNode(string lexema)
        {
            this.typeName = lexema;
        }

        public Types GetVarType()
        {
            if(SymbolsTable.types.ContainsKey(typeName))
            {
                return SymbolsTable.types[typeName];
            }else
            {
                throw new SemanticException("Type does not exist!");
            }
        }
    }
}