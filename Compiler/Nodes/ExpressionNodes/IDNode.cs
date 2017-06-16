using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class IDNode : ExpressionNode
    {
        public string idLexema;
        public List<AttributeNode> attributeList;

        public IDNode(string idLexema)
        {
            this.idLexema = idLexema;
            this.attributeList = new List<AttributeNode>();
        }

        public IDNode(string idLexema, List<AttributeNode> attributeList) : this(idLexema)
        {
            this.attributeList = attributeList;
        }

        public override dynamic Evaluate()
        {
            return VariablesSingleton.Variables[idLexema];
        }

        public override Types EvaluateType()
        {
            if(!SymbolsTable.vars.ContainsKey(idLexema))
            {
                throw new SemanticException("Variable does not exist!");
            }
            var type = SymbolsTable.vars[idLexema];
            foreach(var attribute in attributeList)
            {
                type = attribute.EvaluateType(type);
            }
            return type;
        }

        public override ExpressionCode GenerateCode()
        {
            var sb = new StringBuilder(idLexema);
            foreach(var attrib in attributeList)
            {
                sb.Append('.' + attrib.GenerateCode().Code);
            }
            
            return new ExpressionCode(sb.ToString());
        }

        public override string ToString()
        {
            return idLexema;
        }
    }
}