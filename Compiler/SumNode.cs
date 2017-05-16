namespace Compiler
{
    public class SumNode : ExpressionNode
    {
        private ExpressionNode param;
        private ExpressionNode tValor;

        public SumNode(ExpressionNode param, ExpressionNode tValor)
        {
            this.param = param;
            this.tValor = tValor;
        }
    }
}