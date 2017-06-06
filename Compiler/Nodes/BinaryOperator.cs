using System;
using System.Collections.Generic;

namespace Compiler
{
    public abstract class BinaryOperator : ExpressionNode
    {
        protected ExpressionNode leftOperand;
        protected ExpressionNode rightOperand;

        public BinaryOperator(ExpressionNode leftOperand, ExpressionNode rightOperand)
        {
            this.leftOperand = leftOperand;
            this.rightOperand = rightOperand;
        }

        public Dictionary<string, Types> rules = new Dictionary<string, Types>();

        public override Types EvaluateType()
        {
            var leftType = leftOperand.EvaluateType();
            var rightType = rightOperand.EvaluateType();
            var rule = leftType.GetType().Name + "," + rightType.GetType().Name;
            
            if(rules.ContainsKey(rule))
            {
                return rules[rule];
            }

            throw new SemanticException("Rule not supported");
        }
    }
}