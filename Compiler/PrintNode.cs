namespace Compiler
{
    public class PrintNode : StatementNode
    {
        private ExpressionNode eValor;

        public PrintNode(ExpressionNode eValor)
        {
            this.eValor = eValor;
        }
    }
}