namespace Compiler
{
    public class SubNode : ExpressionNode
    {
        private ExpressionNode param;
        private ExpressionNode tValor;

        public SubNode(ExpressionNode param, ExpressionNode tValor)
        {
            this.param = param;
            this.tValor = tValor;
        }
    }
}