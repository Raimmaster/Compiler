using System;

namespace Compiler
{
    public class DivNode : ExpressionNode
    {
        private ExpressionNode leftOperand;
        private ExpressionNode rightOperand;

        public DivNode(ExpressionNode leftOperand, ExpressionNode rightOperand)
        {
            this.leftOperand = leftOperand;
            this.rightOperand = rightOperand;
        }

        public override dynamic Evaluate()
        {
            return leftOperand.Evaluate() / rightOperand.Evaluate();
        }
    }
}