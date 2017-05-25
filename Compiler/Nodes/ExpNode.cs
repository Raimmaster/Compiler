using System;

namespace Compiler
{
    public class ExpNode : ExpressionNode
    {
        private ExpressionNode leftOperand;
        private ExpressionNode rightOperand;

        public ExpNode(ExpressionNode leftOperand, ExpressionNode rightOperand)
        {
            this.leftOperand = leftOperand;
            this.rightOperand = rightOperand;
        }

        public override dynamic evaluate()
        {
            var result = Math.Pow(leftOperand.evaluate(), rightOperand.evaluate());
            float resultado = float.Parse(result.ToString());

            return resultado;
        }
    }
}