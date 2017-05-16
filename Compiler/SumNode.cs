using System;

namespace Compiler
{
    public class SumNode : ExpressionNode
    {
        private ExpressionNode leftOperand;
        private ExpressionNode rightOperand;

        public SumNode(ExpressionNode leftOperand, ExpressionNode rightOperand)
        {
            this.leftOperand = leftOperand;
            this.rightOperand = rightOperand;
        }

        public override float evaluate()
        {
            return leftOperand.evaluate() + rightOperand.evaluate();
        }
    }
}