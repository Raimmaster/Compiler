using System;
using System.Collections.Generic;

namespace Compiler
{
    public class IDNode : ExpressionNode
    {
        public string idLexema;
        public List<AttributeNode> attributeList;

        public IDNode(string idLexema)
        {
            this.idLexema = idLexema;
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

        public override string ToString()
        {
            return idLexema;
        }
    }
}