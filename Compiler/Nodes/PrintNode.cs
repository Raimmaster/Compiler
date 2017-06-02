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

        public override void Interpret()
        {
            Console.Out.WriteLine("Valor: " + eValor.Evaluate());
        }
    }
}