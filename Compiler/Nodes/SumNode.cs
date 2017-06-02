using System;

namespace Compiler
{
    public class SumNode : BinaryOperator
    {
        public SumNode(ExpressionNode leftOperand, ExpressionNode rightOperand) : base(leftOperand, rightOperand)
        {
            rules["IntType,IntType"] = new IntType();
            rules["IntType,BoolType"] = new IntType();
            rules["BoolType,IntType"] = new IntType();
        }

        public override dynamic Evaluate()
        {
            return leftOperand.Evaluate() + rightOperand.Evaluate();
        }

        public override Types EvaluateType()
        {
            throw new NotImplementedException();
        }
    }
}