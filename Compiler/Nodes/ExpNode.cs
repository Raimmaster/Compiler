using System;

namespace Compiler
{
    public class ExpNode : BinaryOperator
    {
        public ExpNode(ExpressionNode leftOperand, ExpressionNode rightOperand) : base(leftOperand, rightOperand)
        {
            rules["IntType,IntType"] = new IntType();
            rules["IntType,BoolType"] = new IntType();
            rules["BoolType,IntType"] = new IntType();
        }

        public override dynamic Evaluate()
        {
            var result = Math.Pow(leftOperand.Evaluate(), rightOperand.Evaluate());
            float resultado = float.Parse(result.ToString());

            return resultado;
        }
    }
}