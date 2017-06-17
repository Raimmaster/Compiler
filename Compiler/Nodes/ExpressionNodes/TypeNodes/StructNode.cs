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

        public override string GenerateCode()
        {
            var structToClass = @"
            class " + id.lexema + @"{
                constructor(";
            var sb = new System.Text.StringBuilder(structToClass);

            foreach(var attrib in attributeList)
            {
                sb.Append(attrib.varID.idLexema + ',');
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append(@"){
                ");
            
            foreach(var attrib in attributeList)
            {
                var attribCode = attrib.varID.idLexema;
                sb.Append("this." + attribCode + '=' + attribCode + ";\n");
            }

            sb.Append(@"
                }
            }
            ");
            return sb.ToString();
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