namespace Compiler
{
    public class IDNode : ExpressionNode
    {
        private string idLexema;

        public IDNode(string idLexema)
        {
            this.idLexema = idLexema;
        }

        public override dynamic evaluate()
        {
            return VariablesSingleton.Variables[idLexema];
        }

        public override string ToString()
        {
            return idLexema;
        }
    }
}