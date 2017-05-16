using System;

namespace Compiler
{
    public class PrintNode : StatementNode
    {
        private ExpressionNode eValor;

        public PrintNode(ExpressionNode eValor)
        {
            this.eValor = eValor;
        }

        public override void Evaluate()
        {
            Console.Out.WriteLine("Valor: " + eValor.evaluate());
        }
    }
}