using System;

namespace Compiler
{
    public class MulNode : BinaryOperator
    {
        public MulNode(ExpressionNode leftOperand, ExpressionNode rightOperand) : base(leftOperand, rightOperand)
        {
            rules["IntType,IntType"] = new IntType();
            rules["BoolType,BoolType"] = new BoolType();
        }

        public override dynamic Evaluate()
        {
            return leftOperand.Evaluate() * rightOperand.Evaluate();
        }
    }
}