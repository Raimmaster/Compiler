using System;
using System.Collections.Generic;

namespace Compiler
{
    public class StructNode : StatementNode
    {
        public Token id;
        public List<DeclarationStatement> attributeList;

        public StructNode(Token id, List<DeclarationStatement> attributeList)
        {
            this.id = id;
            this.attributeList = attributeList;
        }

        public override void Interpret()
        {
            throw new NotImplementedException();
        }

        public override void ValidateSemantic()
        {
            var structType = new StructType();
            _fillAttributes(structType, attributeList);
            if(SymbolsTable.types.ContainsKey(id.lexema) || 
                SymbolsTable.types.ContainsKey(id.lexema))
            {
                throw new SemanticException("Struct id already exists!");
            }

            SymbolsTable.types[id.lexema] = structType;
        }

        private void _fillAttributes(StructType structType, List<DeclarationStatement> attributeList)
        {
            foreach(var attribute in attributeList)
            {
                var type = attribute.varType.GetVarType();
                structType.attributes[attribute.varID.idLexema] = type;
            }
        }
    }
}