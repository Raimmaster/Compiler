using System;

namespace Compiler
{
    public class DivNode : BinaryOperator
    {
        
        public DivNode(ExpressionNode leftOperand, ExpressionNode rightOperand) : base(leftOperand, rightOperand)
        {
            rules["IntType,IntType"] = new IntType();
        }

        public override dynamic Evaluate()
        {
            return leftOperand.Evaluate() / rightOperand.Evaluate();
        }
    }
}