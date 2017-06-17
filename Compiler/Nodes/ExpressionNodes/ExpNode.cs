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

        public override ExpressionCode GenerateCode()
        {
            var leftCode = leftOperand.GenerateCode();
            var rightCode = rightOperand.GenerateCode();
            
            var code = '(' + leftCode.Code + '^' + rightCode.Code + ')';
            return new ExpressionCode(code);
        }
    }
}