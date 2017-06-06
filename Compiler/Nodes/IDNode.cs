using System;

namespace Compiler
{
    public class IDNode : ExpressionNode
    {
        private string idLexema;

        public IDNode(string idLexema)
        {
            this.idLexema = idLexema;
        }

        public override dynamic Evaluate()
        {
            return VariablesSingleton.Variables[idLexema];
        }

        public override Types EvaluateType()
        {
            if(SymbolsTable.vars.ContainsKey(idLexema))
            {
                return SymbolsTable.vars[idLexema];
            }

            throw new SemanticException("Variable does not exist!");
        }

        public override string ToString()
        {
            return idLexema;
        }
    }
}