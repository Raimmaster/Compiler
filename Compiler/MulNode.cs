namespace Compiler
{
    public class MulNode : ExpressionNode
    {
        private ExpressionNode leftOperand;
        private ExpressionNode rightOperand;

        public MulNode(ExpressionNode leftOperand, ExpressionNode rightOperand)
        {
            this.leftOperand = leftOperand;
            this.rightOperand = rightOperand;
        }
    }
}