using System;

namespace Compiler
{
    public class AssignNode : StatementNode
    {
        private IDNode id;
        private ExpressionNode eValor;

        public AssignNode(IDNode idLexema, ExpressionNode eValor)
        {
            this.id = idLexema;
            this.eValor = eValor;
        }

        public override string GenerateCode()
        {
            var code = id.idLexema + " = " + eValor.GenerateCode().Code + ";\n";
            return code;
        }

        public override void Interpret()
        {
            VariablesSingleton.Variables[id.ToString()] = eValor.Evaluate();
        }

        public override void ValidateSemantic()
        {
            var valorType = eValor.EvaluateType();
            
            if(!SymbolsTable.vars.ContainsKey(id.ToString()))
            {
                SymbolsTable.vars[id.ToString()] = valorType;
                return;
            }
            var nodeType = id.EvaluateType();

            if(valorType.GetType() != nodeType.GetType())
            {
                throw new SemanticException("Incorrect assignation types.");
            }
            
        }
    }
}