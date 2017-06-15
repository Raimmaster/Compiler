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

        public override string GenerateCode()
        {
            var code = "console.log(" + eValor.GenerateCode().Code + ");\n";

            return code;
        }

        public override void Interpret()
        {
            Console.Out.WriteLine("Valor: " + eValor.Evaluate());
        }

        public override void ValidateSemantic()
        {
            var valorType = eValor.EvaluateType();
            if((!(valorType is IntType) && !(valorType is BoolType)))
            {
                throw new SemanticException("Invalid types.");
            }
        }
    }
}