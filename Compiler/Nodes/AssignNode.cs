using System;

namespace Compiler
{
    public class AssignNode : StatementNode
    {
        private IDNode idLexema;
        private ExpressionNode eValor;

        public AssignNode(IDNode idLexema, ExpressionNode eValor)
        {
            this.idLexema = idLexema;
            this.eValor = eValor;
        }

        public override void Interpret()
        {
            VariablesSingleton.Variables[idLexema.ToString()] = eValor.Evaluate();
        }

        public override void ValidateSemantic()
        {
            var valorType = eValor.EvaluateType();
            
            if(!SymbolsTable.vars.ContainsKey(idLexema.ToString()))
            {
                SymbolsTable.vars[idLexema.ToString()] = valorType;
                return;
            }
            var nodeType = idLexema.EvaluateType();

            if(!(valorType.GetType() == nodeType.GetType()))
            {
                throw new SemanticException("Incorrect assignation types.");
            }
            
        }
    }
}