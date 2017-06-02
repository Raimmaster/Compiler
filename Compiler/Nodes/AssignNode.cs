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
    }
}