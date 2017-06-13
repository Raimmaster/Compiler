using System.Collections.Generic;

namespace Compiler
{
    public static class SymbolsTable
    {
        public static Dictionary<string, Types> vars = new Dictionary<string, Types>();                 
        public static Dictionary<string, Types> types = new Dictionary<string, Types>();

        public static void InitTypes()
        {
            types["int"] = new IntType();
            types["bool"] = new BoolType();
        }
    }
}