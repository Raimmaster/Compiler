using System;

namespace Compiler
{
    public class SumNode : BinaryOperator
    {
        public SumNode(ExpressionNode leftOperand, ExpressionNode rightOperand) : base(leftOperand, rightOperand)
        {
            rules["IntType,IntType"] = new IntType();
            rules["BoolType,BoolType"] = new BoolType();
        }

        public override dynamic Evaluate()
        {
            return leftOperand.Evaluate() + rightOperand.Evaluate();
        }

        public override ExpressionCode GenerateCode()
        {
            var leftCode = leftOperand.GenerateCode();
            var rightCode = rightOperand.GenerateCode();
            
            var code = '(' + leftCode.Code + '+' + rightCode.Code + ')';
            return new ExpressionCode(code);
        }
    }
}