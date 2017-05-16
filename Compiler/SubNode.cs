using System;

namespace Compiler
{
    public class SubNode : ExpressionNode
    {
        private ExpressionNode leftOperand;
        private ExpressionNode rightOperand;

        public SubNode(ExpressionNode leftOperand, ExpressionNode rightOperand)
        {
            this.leftOperand = leftOperand;
            this.rightOperand = rightOperand;
        }

        public override float evaluate()
        {
            return leftOperand.evaluate() - rightOperand.evaluate();
        }
    }
}