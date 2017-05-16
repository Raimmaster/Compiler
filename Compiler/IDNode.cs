namespace Compiler
{
    public class IDNode : ExpressionNode
    {
        private string idLexema;

        public IDNode(string idLexema)
        {
            this.idLexema = idLexema;
        }
    }
}