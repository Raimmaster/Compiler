namespace Compiler
{
    public class IndexArrayNode : AttributeNode
    {
        private ExpressionNode value;

        public IndexArrayNode(ExpressionNode value)
        {
            this.value = value;
        }
    }
}