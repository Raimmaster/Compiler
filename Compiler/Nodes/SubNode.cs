using System;

namespace Compiler
{
    public class SubNode : BinaryOperator
    {
        public SubNode(ExpressionNode leftOperand, ExpressionNode rightOperand) : base(leftOperand, rightOperand)
        {
            rules["IntType,IntType"] = new IntType();
        }

        public override dynamic Evaluate()
        {
            return leftOperand.Evaluate() - rightOperand.Evaluate();
        }
    }
}