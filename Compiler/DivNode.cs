namespace Compiler
{
    public class DivNode : ExpressionNode
    {
        private ExpressionNode param;
        private ExpressionNode fValor;

        public DivNode(ExpressionNode param, ExpressionNode fValor)
        {
            this.param = param;
            this.fValor = fValor;
        }
    }
}