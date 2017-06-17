using System.Collections.Generic;

namespace Compiler
{
    public class StructType : Types
    {
        public Dictionary<string, Types>  attributes;

        public StructType()
        {
            attributes = new Dictionary<string, Types>();
        }
    }
}